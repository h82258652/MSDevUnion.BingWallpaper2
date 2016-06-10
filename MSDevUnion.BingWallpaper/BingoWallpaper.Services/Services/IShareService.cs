using System.Threading.Tasks;

namespace BingoWallpaper.Services
{
    public interface IShareService
    {
        Task ShareToSinaWeiboAsync();

        Task ShareToWechatAsync();
    }
}