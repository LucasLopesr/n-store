using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;

namespace NStore.WebApp.MVC.Extensions.Attributes
{
    public class CpfValidationAttributeAdapterProvider : IValidationAttributeAdapterProvider
    {
        private readonly IValidationAttributeAdapterProvider baseProvider = new ValidationAttributeAdapterProvider();
        public IAttributeAdapter GetAttributeAdapter(ValidationAttribute attribute, IStringLocalizer stringLocalizer)
        {
            if (attribute is CpfAttribute cpfAttribute) 
                return new CpfAttributeAdapter(cpfAttribute, stringLocalizer);
            return baseProvider.GetAttributeAdapter(attribute, stringLocalizer);
        }
    }
}
