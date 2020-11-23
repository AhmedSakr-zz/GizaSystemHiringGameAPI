using System;

namespace HiringGame.Common
{
    [Serializable]
    public class MessageModel
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public DataEnum.ClientMessageType MessageType { get; set; }
    }
}
