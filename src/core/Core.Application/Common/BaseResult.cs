namespace Core.Application.Common
{
    public class BaseResult<T> : BaseResult
    {
        public T Data { get; private set; }

        protected BaseResult(bool succeded, string responseCode, string responseMessage, T data) : base(succeded, responseCode, responseMessage)
        {
            Data = data;
        }

        public static BaseResult<T> Success(T data)
        {
            return new BaseResult<T>(true, "0000", "Succeeded", data);
        }

        public static BaseResult<T> Fail(string responseCode, string responseMessage, T data)
        {
            return new BaseResult<T>(false, responseCode, responseMessage, data);
        }
    }

    public class BaseResult
    {
        public bool Succeeded { get; private set; }
        public string ResponseCode { get; private set; }
        public string ResponseMessage { get; private set; }

        protected BaseResult(bool succeeded, string responseCode, string responseMessage)
        {
            Succeeded = succeeded;
            ResponseCode = responseCode;
            ResponseMessage = responseMessage;
        }

        public static BaseResult Success()
        {
            return new BaseResult(true, "0000", "Succeeded");
        }

        public static BaseResult Fail(string responseCode, string responseMessage)
        {
            return new BaseResult(false, responseCode, responseMessage);
        }
    }
}
