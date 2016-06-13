using BingoWallpaper.Services;
using MicroMsg.sdk;
using System;
using System.Threading.Tasks;

namespace BingoWallpaper.Uwp.Services
{
    public class ShareService : IShareService
    {
        public Task ShareToSinaWeiboAsync()
        {
            throw new NotImplementedException();
        }

        public async Task ShareToWechatAsync()
        {
            const int scene = SendMessageToWX.Req.WXSceneChooseByUser;
            var message = new WXImageMessage()
            {
                // TODO
                Title = "TODO",
                ImageData = null
            };
            var req = new SendMessageToWX.Req(message, scene);
            var api = WXAPIFactory.CreateWXAPI(Constants.WechatAppId);
            var isValid = await api.SendReq(req);
        }
    }
}