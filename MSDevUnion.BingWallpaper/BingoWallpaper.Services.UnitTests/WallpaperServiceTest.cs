using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.Threading.Tasks;

namespace BingoWallpaper.Services.UnitTests
{
    [TestClass]
    public class WallpaperServiceTest
    {
        private static WallpaperService _service;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            _service = new WallpaperService();
        }

        public Task TestGetArchivesAsync()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public async Task TestGetArchivesAsync2()
        {
            await Microsoft.VisualStudio.TestPlatform.UnitTestFramework.AppContainer.Assert.ThrowsException<ArgumentOutOfRangeException>(async () =>
            {
                await _service.GetArchivesAsync(2014, 12, "zh-CN");
            });
            await Microsoft.VisualStudio.TestPlatform.UnitTestFramework.AppContainer.Assert.ThrowsException<ArgumentNullException>(async () =>
            {
                await _service.GetArchivesAsync(2015, 1, null);
            });
            await Microsoft.VisualStudio.TestPlatform.UnitTestFramework.AppContainer.Assert.ThrowsException<ArgumentException>(async () =>
            {
                await _service.GetArchivesAsync(2015, 1, string.Empty);
            });
        }

        public async Task TestGetImageAsync()
        {
            await Microsoft.VisualStudio.TestPlatform.UnitTestFramework.AppContainer.Assert.ThrowsException<ArgumentNullException>(async () =>
            {
                await _service.GetImageAsync(null);
            });
        }
    }
}