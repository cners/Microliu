﻿namespace Microliu.Utils
{
    /// <summary>
    /// 返回响应结果
    /// </summary>
    public class ReturnResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 默认：true=200,false=400
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 返回附加数据
        /// </summary>
        public object Data { get; set; }

        public ReturnResult()
        {
            Success = false;
            Message = "";
            Data = "";
            Code = 400;
        }

        public ReturnResult(bool success, string message)
        {
            Success = success;
            Message = message;
            Data = "";
            Code = success ? 200 : 400;
        }

        public static ReturnResult New()
        {
            return new ReturnResult();
        }

        public ReturnResult SetSuccess(bool success = true)
        {
            Success = success;
            Code = success ? 200 : 400;
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
            Code = success ? 200 : 400;
            return this;
        }

        public static ReturnResult Set(bool success = false, string message = "", object data = null)
        {
            var dto = new ReturnResult();
            dto.Success = success;
            dto.Message = message;
            dto.Data = data;
            dto.Code = success ? 200 : 400;
            return dto;
        }
    }
}
