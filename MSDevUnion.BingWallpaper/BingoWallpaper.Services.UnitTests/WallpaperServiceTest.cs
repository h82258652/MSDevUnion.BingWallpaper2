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

        [TestMethod]
        public async Task TestGetImageAsync2()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(async () =>
            {
                await _service.GetArchivesAsync(2014, 12, "zh-CN");
            });
        }

        public void TestGetImageAsync3()
        {
            Assert.ThrowsException<ArgumentNullException>(async () =>
            {
                await _service.GetArchivesAsync(2015, 1, null);
            });
        }
    }
}