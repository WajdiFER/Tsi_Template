using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Tsi.Template.Abstraction.Common;
using Tsi.Template.Core.Abstractions;
using Tsi.Template.Core.Attributes;
using Tsi.Template.Core.Configuration;
using Tsi.Template.Core.Enums;
using Tsi.Template.Core.Helpers;
using Tsi.Template.Domain.Common;
using Tsi.Template.ViewModels.Common;

namespace Tsi.Template.Services.Common
{
    [Injectable(typeof(IUserRegistrationService))]
    public class UserRegistrationService : IUserRegistrationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;
        private readonly ISettingService _settingService;
        private readonly IPasswordHasher _passwordHasher;

        public UserRegistrationService(IHttpContextAccessor httpContextAccessor
            , IUserService userService
            , ISettingService settingService
            , IPasswordHasher passwordHasher)
        {
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
            _settingService = settingService;
            _passwordHasher = passwordHasher;
        }

        public async Task<RegisterResult> RegisterUser(UserRegisterModel model)
        {
            User user;

            var userSettings = await _settingService.LoadSettingAsync<UserSettings>();

            if (userSettings.UserNameEnabled)
            {
                if (string.IsNullOrWhiteSpace(model.Username))
                {
                    return RegisterResult.UsernameNotProvided;
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(model.Email))
                {
                    return RegisterResult.EmailNotProvided;
                }
            }

            user = await _userService.GetUserByUsernameAsync(model.Username);

            if (user is not null)
            {
                return RegisterResult.UsernameUsed;
            }

            user = await _userService.GetUserByEmailAsync(model.Email);

            if (user is not null)
            {
                return RegisterResult.EmailUsed;
            }

            if (!string.IsNullOrWhiteSpace(model.PhoneNumber))
            {
                user = await _userService.GetUserByPhoneNumberAsync(model.PhoneNumber);

                if (user is not null)
                {
                    return RegisterResult.EmailUsed;
                }
            }

            user = new User
            {
                Email = model.Email,
                Username = model.Username,
                NormalizedEmail = CommonHelper.Normalize(model.Email),
                NormalizedUsername = CommonHelper.Normalize(model.Username),
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = string.IsNullOrWhiteSpace(model.PhoneNumber) ? string.Empty : model.PhoneNumber
                
            };

            if (!userSettings.AdministratorActivationRequired)
            {
                user.Active = true;
            }

            var salt = _passwordHasher.GenerateSalt();

            var saltedPassword = _passwordHasher.GenerateHash(model.Password, salt);

            user.Salt = salt;
            user.Password = saltedPassword;

            await _userService.CreateUserAsync(user);

            return RegisterResult.Success;
        }

        public async Task SignInUserAsync(User user, bool isPersistent = false)
        {
            string authenticationScheme = CookieAuthenticationDefaults.AuthenticationScheme;

            var claims = GetUserClaims(user);

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, authenticationScheme);
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            var authProperties = new AuthenticationProperties
            {
                // AllowRefresh = <bool>,
                // Refreshing the authentication session should be allowed.
                // ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                // The time at which the authentication ticket expires. A 
                // value set here overrides the ExpireTimeSpan option of 
                // CookieAuthenticationOptions set with AddCookie.
                // IsPersistent = true,
                // Whether the authentication session is persisted across 
                // multiple requests. Required when setting the 
                // ExpireTimeSpan option of CookieAuthenticationOptions 
                // set with AddCookie. Also required when setting 
                // ExpiresUtc.
                // IssuedUtc = <DateTimeOffset>,
                // The time at which the authentication ticket was issued.
                // RedirectUri = "~/Account/Index"
                // The full path or absolute URI to be used as an http 
                // redirect response value.
            };

            await _httpContextAccessor.HttpContext.SignInAsync(authenticationScheme, claimsPrincipal, authProperties);
        }

        public async Task<LoginResult> ValidateUser(LoginModel model)
        {
            User user = await GetUserByUserNameOrEmail(model.Username, model.Email);

            if (user is null)
            {
                return LoginResult.NotFound;
            }

            if (user.LockedOut)
            {
                return LoginResult.LockedOut;
            }

            if (!user.Active)
            {
                return LoginResult.NotActive;
            }

            if (await ValidatePassword(user, model.Password))
            {
                return LoginResult.Success;
            }
            else
            {
                return LoginResult.WrongPassword;
            }
        }

        private async Task<User> GetUserByUserNameOrEmail(string username, string email)
        {
            var userSettings = await _settingService.LoadSettingAsync<UserSettings>();

            User user;

            if (userSettings.UserNameEnabled)
            {
                user = await _userService.GetUserByUsernameAsync(username);
            }
            else
            {
                user = await _userService.GetUserByEmailAsync(email);
            }

            return user;
        }

        public Task<bool> ValidatePassword(User user, string password)
        {
            var saltedPassword = _passwordHasher.GenerateHash(password, user.Salt);

            return Task.FromResult(saltedPassword.Equals(user.Password));
        }

        public async Task SignOutAsync()
        {
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
        private List<Claim> GetUserClaims(User user) => new()
        {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.NormalizedUsername),
                new Claim(ClaimTypes.Email, user.NormalizedEmail)
            };

        public async Task<User> GetCurrentUserAsync()
        {
            var claims = _httpContextAccessor.HttpContext?.User?.Claims?? null;

            if(claims is null || claims.Count() == 0)
            {
                return null;
            }

            User user;

            var settings = await _settingService.LoadSettingAsync<UserSettings>();

            if (settings.UserNameEnabled)
            {
                var usernameClaim = claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name);
                if (string.IsNullOrEmpty(usernameClaim.Value))
                {
                    return null;
                }

                user = await _userService.GetUserByUsernameAsync(usernameClaim.Value);
            }
            else
            {
                var emailClaim = claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email);
                if (string.IsNullOrEmpty(emailClaim.Value))
                {
                    return null;
                }

                user = await _userService.GetUserByEmailAsync(emailClaim.Value);
            }

            if(user is not null)
            {
                user.Salt = string.Empty;
                user.Password = string.Empty;
            }

            return user;
        }
    }
}
