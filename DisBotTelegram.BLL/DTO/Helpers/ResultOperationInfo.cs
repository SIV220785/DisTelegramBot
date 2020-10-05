
namespace DisBotTelegram.BLL.Helpers
{
    public class ResultOperationInfo
    {
        public bool IsSuccess { get; }
        public string Message { get; set; }
        public ResultOperationInfo(bool isSuccess, string message = "")
        {
            IsSuccess = isSuccess;
            Message = message;
        }
    }

    public class ResultOperationInfo<T> : ResultOperationInfo where T : class
    {
        public T Value { get; }
        public ResultOperationInfo(T value, bool isSuccess, string message)
          : base(isSuccess, message)
        {
            Value = value;
        }
    }
}
