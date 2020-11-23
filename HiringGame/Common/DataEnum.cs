namespace HiringGame.Common
{
    public class DataEnum
    {
        public enum OperationStatus
        {
            Ok = 1,
            Error = 2,
            ValidationError = 3,
        }
        
        public enum ClientMessageType
        {
            Success = 1,
            Error = 2,
            Warning = 3,
            Info = 4

        }
    }
}
