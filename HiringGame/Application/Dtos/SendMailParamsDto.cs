using System.Collections.Generic;

namespace HiringGame.Application.Dtos
{
    public class SendMailParamsDto
    {
        public SendMailParamsDto()
        {
            ToList = new List<string>();
            CcList = new List<string>();
        }

        public IList<string> ToList { get; set; }
        public IList<string> CcList { get; set; }
        public string MessageTitle { get; set; }
        public string MessageBody { get; set; }
        public bool IsHtml { get; set; }
    }
}
