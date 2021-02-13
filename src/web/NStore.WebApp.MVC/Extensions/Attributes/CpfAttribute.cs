using NStore.Core.DomainObjects;
using System.ComponentModel.DataAnnotations;
namespace NStore.WebApp.MVC.Extensions
{
    public class CpfAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return Cpf.Validar(value.ToString()) ? ValidationResult.Success : new ValidationResult("CPF em formato inválido") ;
        }
    }
}
