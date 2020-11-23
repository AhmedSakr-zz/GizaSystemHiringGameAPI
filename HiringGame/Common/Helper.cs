using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HiringGame.Common
{
    public class Helper
    {
        public static string PrepareClientMessage(string message, DataEnum.ClientMessageType type)
        {
            var msg = new MessageModel
            {
                Title = type.ToString(),
                Message = message,
                MessageType = type
            };

           return JsonConvert.SerializeObject(msg);
        }
    }
}
