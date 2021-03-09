using System.ComponentModel.DataAnnotations;
using NStore.Core.DomainObjects;

namespace NStore.WebApp.MVC.Extensions.Attributes
{
    public class CpfAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return Cpf.Validar(value.ToString()) ? ValidationResult.Success : new ValidationResult("CPF em formato inválido") ;
        }
    }
}
