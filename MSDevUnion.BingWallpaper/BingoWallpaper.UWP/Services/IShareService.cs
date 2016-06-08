using System.Threading.Tasks;

namespace BingoWallpaper.Uwp.Services
{
    public interface IShareService
    {
        Task ShareToSinaWeiboAsync();

        Task ShareToWechatAsync();
    }
}