﻿using FluentValidation;
using System;
using System.Threading;
using System.Threading.Tasks;
using Tsi.Template.Abstraction.Catalog;
using Tsi.Template.ViewModels.Catalog;

namespace Tsi.Template.Validation.Catalog
{
    public class ProductViewModelValidator : TsiBaseValidator<ProductViewModel>
    {
        private readonly IProductService _productService;
        public ProductViewModelValidator(IProductService productService)
        {

            _productService = productService;

            RuleFor(p => p.Price).GreaterThan(0)
                .WithMessage("Price cannot be negative");

            RuleFor(p => p.Code).Must(code =>
             {
                 var productsWithCode = _productService.GetProductbyCode(code).GetAwaiter().GetResult();

                 return productsWithCode is null;
             }).WithMessage("Code already exist in the database");

            RuleFor(p => p).Must(ValidateSommeEcritureDetail)
                .WithMessage(GetMessage("TSI.CMPT.ERROR.ECRITURENONEQUILIBRE"));
        }

        private bool ValidateSommeEcritureDetail(ProductViewModel arg)
        {
            return false;
        } 
    }
}
