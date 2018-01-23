using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Blog.WebMobile.Models;
using Microsoft.Extensions.Options;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.Entities;
using Blog.Common;

namespace Blog.WebMobile.Controllers
{
    public class WechatController : Controller
    {
        [HttpPost]
        public IActionResult Post([FromServices]IOptions<AppSetting> appSettings, WechatPostRequestModel model)
        {
            if (!CheckSignature.Check(model.signature, model.timestamp, model.nonce, appSettings.Value.Token))
            {
                return Content("参数错误！");
            }
            var doc = XDocument.Load(Request.Body);
            var requestMessage = RequestMessageFactory.GetRequestEntity(doc);
            try
            {
                var baseMessage = Senparc.Weixin.MP.Entities.ResponseMessageBase.CreateFromRequestMessage<ResponseMessageTransfer_Customer_Service>(requestMessage);
                var responseDoc = Senparc.Weixin.MP.Helpers.EntityHelper.ConvertEntityToXml(baseMessage);
                var responseStr = responseDoc.ToString();
                switch (requestMessage.MsgType)
                {
                    case RequestMsgType.Text:
                        break;
                    case RequestMsgType.Location:
                        break;
                    case RequestMsgType.Image:
                        break;
                    case RequestMsgType.Voice:
                        break;
                    case RequestMsgType.Video:
                        break;
                    case RequestMsgType.Link:
                        break;
                    case RequestMsgType.ShortVideo:
                        break;
                    case RequestMsgType.Event:
                        //responseStr = await wechatEvent.HandleAsync(doc);
                        break;
                    default:
                        break;
                }
                return Content(responseStr);
            }
            catch (Exception ex)
            {
                //LogManager.GetCurrentClassLogger().Error($"Exception:{ex}\n,Doc:{doc}");
                return Content(model.echostr);
            }
        }

        public IActionResult Get([FromServices]IOptions<AppSetting> appSettings, WechatGetRequestModel model)
        {
            if (CheckSignature.Check(model.signature, model.timestamp, model.nonce, appSettings.Value.Token))
            {
                return Content(model.echostr);
            }
            return Content("failed:" + model.signature + "," + Senparc.Weixin.MP.CheckSignature.GetSignature(model.timestamp, model.nonce, appSettings.Value.Token) + "。" +
                           "如果你在浏览器中看到这句话，说明此地址可以被作为微信公众账号后台的Url，请注意保持Token一致。");
        }
    }
}
