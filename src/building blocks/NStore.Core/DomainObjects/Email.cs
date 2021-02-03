using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NStore.Core.DomainObjects
{
    public class Email
    {
        public const int EnderecoMaxLength = 254;
        public const int EnderecoMinLength = 5;

        public string Endereco { get; private set; }

        public Email(string endereco)
        {
            if (!ValidarEmail(endereco)) throw new DomainException("E-mail inválido");
            Endereco = endereco;
        }

        protected Email() { }

        public static bool ValidarEmail(string endereco) 
        {
            var expressaoRegex = new Regex(@"\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}");
            return expressaoRegex.IsMatch(endereco);
        }

       
    }
}
