
using System.Linq;

namespace NStore.Core.Communication
{
    public class ResponseResult
    {
        public string Title { get; set; }
        public int Status { get; set; }
        public ResponseErrorMessages Errors { get; set; }

        public ResponseResult()
        {
            Errors = new ResponseErrorMessages();
        }

        public bool AnyErrors()
        {
            return Errors.Mensagens.Any();
        }
    }
}
