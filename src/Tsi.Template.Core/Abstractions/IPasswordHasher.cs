namespace Tsi.Template.Core.Abstractions
{
    public interface IPasswordHasher
    {
        string GenerateHash(string input, string salt);
        string GenerateSalt(int size = 32); 
    } 
}
