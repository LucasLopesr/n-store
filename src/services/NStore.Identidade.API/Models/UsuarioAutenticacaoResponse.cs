﻿namespace NStore.Identidade.API.Models
{
    public class UsuarioAutenticacaoResponse
    {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public UsuarioToken UsuarioToken { get; set; }
    }
}
