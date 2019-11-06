namespace Microliu.Utils
{
    public class ReturnResult
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }

        public ReturnResult()
        {
            Success = true;
            Message = "";
            Data = "";
        }
        public ReturnResult(bool success, string message)
        {
            Success = success;
            Message = message;
            Data = "";
        }

        public ReturnResult SetSuccess(bool success = true)
        {
            Success = success;
            return this;
        }

        public ReturnResult SetMessage(string message)
        {
            Message = message;
            return this;
        }

        public ReturnResult SetData(object data)
        {
            Data = data;
            return this;
        }

        public ReturnResult SetData(bool success, object data)
        {
            Success = success;
            Data = data;
            return this;
        }

        public static ReturnResult Set(bool success = false, string message = "", object data = null)
        {
            var dto = new ReturnResult();
            dto.Success = success;
            dto.Message = message;
            dto.Data = data;
            return dto;
        }
    }
}
