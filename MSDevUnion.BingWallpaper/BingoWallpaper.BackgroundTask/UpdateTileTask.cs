using Windows.ApplicationModel.Background;

namespace BingoWallpaper.BackgroundTask
{
    public sealed class UpdateTileTask : IBackgroundTask
    {
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            var deferral = taskInstance.GetDeferral();
            try
            {
            }
            finally
            {
                deferral.Complete();
            }
        }
    }
}