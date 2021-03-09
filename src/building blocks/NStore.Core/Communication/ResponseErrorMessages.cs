using System.Collections.Generic;
namespace NStore.Core.Communication
{
    public class ResponseErrorMessages
    {
        public List<string> Mensagens { get; set; }

        public ResponseErrorMessages()
        {
            Mensagens = new List<string>();
        }
    }
}
