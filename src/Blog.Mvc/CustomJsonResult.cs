using System;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Blog.Mvc
{
    public class CustomJsonResult<T> : ActionResult, IResult<T>
    {
        /// <summary>
        /// 结果状态默认为Unknown
        /// </summary>
        public ResultType Result { get; set; } = ResultType.Unknown;

        /// <summary>
        /// 信息默认返回空字符串
        /// </summary>
        public string Message { get; set; } = "";

        /// <summary>
        /// 信息默认返回空字符串
        /// </summary>
        public ResultCode Code { get; set; } = ResultCode.Unknown;

        /// <summary>
        /// 内容默认为null
        /// </summary>
        public T Data { get; set; } = default(T);

        public JsonConverter[] JsonConverter { get; set; }

        public JsonSerializerSettings JsonSerializerSettings { get; set; }

        public CustomJsonResult()
        {
            JsonSerializerSettings = new JsonSerializerSettings();
        }

        private void SetCustomJsonResult(string contenttype, ResultType type, ResultCode code, string message, T data,
            JsonSerializerSettings settings, params JsonConverter[] converters)
        {
            //this.ContentType = contenttype;
            Result = type;
            Code = code;
            Message = message;
            Data = data;
            JsonSerializerSettings = settings;
            JsonConverter = converters;
        }

        public CustomJsonResult(string contenttype, ResultType type, ResultCode code, string message, T content,
            JsonSerializerSettings settings, params JsonConverter[] converters)
        {
            SetCustomJsonResult(contenttype, type, code, message, content, settings, converters);
        }

        public CustomJsonResult(string contenttype, ResultType type, string message, T content,
            JsonSerializerSettings settings, params JsonConverter[] converters)
        {
            SetCustomJsonResult(contenttype, type, ResultCode.Unknown, message, content, settings, converters);
        }

        public CustomJsonResult(string contenttype, ResultType type, string message, T content,
            params JsonConverter[] converters)
        {
            SetCustomJsonResult(contenttype, type, ResultCode.Unknown, message, content, null, converters);
        }

        public CustomJsonResult(string contenttype, ResultType type, string message, T content,
            JsonSerializerSettings settings)
        {
            SetCustomJsonResult(contenttype, type, ResultCode.Unknown, message, content, settings);
        }

        public CustomJsonResult(ResultType type, ResultCode code, string message, T content,
            params JsonConverter[] converters)
        {
            SetCustomJsonResult(null, type, code, message, content, null, converters);
        }

        public override void ExecuteResult(ActionContext context)
        {
            if (context == null)
            {
                throw new ArgumentException("context");
            }
            var response = context.HttpContext.Response;
            response.ContentType = "application/json; charset=utf-8";
            //if (!String.IsNullOrEmpty(ContentType))
            //{
            //    response.ContentType = ContentType;
            //}
            //else
            //{
            //    response.ContentType = "application/json";
            //}

            //if (ContentEncoding != null)
            //{
            //    response.ContentEncoding = ContentEncoding;
            //}
            var bytes = Encoding.UTF8.GetBytes(GetResultJson());
            response.Body.Write(bytes, 0, bytes.Length);
            base.ExecuteResult(context);
        }

        public override string ToString()
        {
            return GetResultJson();
        }


        public string GetResultJson()
        {
            StringBuilder json = new StringBuilder();
            json.Append("{");

            try
            {
                if (Data != null)
                {
                    if (Data is string)
                    {
                        if (!string.IsNullOrWhiteSpace(Data.ToString()))
                        {
                            json.Append("\"data\":\"" + Data + "\",");
                        }
                    }
                    else
                    {

                        if (JsonSerializerSettings == null)
                        {
                            JsonSerializerSettings =
                                new JsonSerializerSettings() {ContractResolver = new DefaultContractResolver()};

                        }

                        JsonConvert.DefaultSettings = new Func<JsonSerializerSettings>(() =>
                        {
                            //日期类型默认格式化处理
                            JsonSerializerSettings.DateFormatHandling =
                                Newtonsoft.Json.DateFormatHandling.MicrosoftDateFormat;
                            JsonSerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                            return JsonSerializerSettings;
                        });
                        JsonSerializerSettings.Converters = this.JsonConverter;
                        JsonSerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                        json.Append("\"data\":" +
                                    JsonConvert.SerializeObject(Data, Formatting.None, JsonSerializerSettings) + ",");
                    }
                }
                json.Append("\"result\": " + (int) Result + ",");


                if (Code != ResultCode.Unknown)
                {
                    json.Append("\"code\": \"" + ((int) Code).ToString() + "\",");
                }


                json.Append("\"message\":" + JsonConvert.SerializeObject(Message) + "");


            }
            catch (Exception ex)
            {

                json.Append("\"result\": " + (int) ResultType.Exception + ",");
                json.Append("\"message\":\"" + string.Format("CustomJsonResult转换发生异常:{0}", ex.Message) + "\"");
                //转换失败记录日志
            }
            json.Append("}");

            string s = json.ToString();

            return s;
        }
    }

    public class CustomJsonResult : CustomJsonResult<object>
    {

        public CustomJsonResult()
        {

        }

        public CustomJsonResult(ResultType type, string message) : base(type, ResultCode.Unknown, message, null, null)
        {

        }

        public CustomJsonResult(ResultType type, ResultCode code, string message) : base(type, code, message, null,
            null)
        {

        }

        public CustomJsonResult(ResultType type, ResultCode code, string message, object content) : base(type, code,
            message, content, null)
        {

        }

        public CustomJsonResult(ResultType type, ResultCode code, string message, object content,
            params JsonConverter[] converters) : base(type, code, message, content, converters)
        {

        }

        public CustomJsonResult(string contenttype, ResultType type, ResultCode code, string message, object content,
            JsonSerializerSettings settings, params JsonConverter[] converters) : base(contenttype, type, code, message,
            content, settings, converters)
        {

        }

        public CustomJsonResult(string contenttype, ResultType type, string message, object content,
            JsonSerializerSettings settings, params JsonConverter[] converters) : base(contenttype, type, message,
            content, settings, converters)
        {

        }

        public CustomJsonResult(string contenttype, ResultType type, string message, object content,
            params JsonConverter[] converters) : base(contenttype, type, message, content, converters)
        {

        }

        public CustomJsonResult(string contenttype, ResultType type, string message, object content,
            JsonSerializerSettings settings) : base(contenttype, type, message, content, settings)
        {

        }

    }
}
