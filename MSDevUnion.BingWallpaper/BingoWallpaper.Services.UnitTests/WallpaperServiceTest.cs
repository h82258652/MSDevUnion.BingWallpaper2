using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace BingoWallpaper.Services.UnitTests
{
    [TestFixture]
    public class WallpaperServiceTest
    {
        private IWallpaperService _service;

        [SetUp]
        public void SetUp()
        {
            _service = new WallpaperService();
        }

        [Test]
        public async Task TestGetArchivesAsync()
        {
            var result = await _service.GetArchivesAsync(2015, 1, "zh-CN");
            Assert.AreEqual(result.ErrorCode, 0);
            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
            {
                await _service.GetArchivesAsync(2014, 12, "zh-CN");
            });
            Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await _service.GetArchivesAsync(2015, 1, null);
            });
            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await _service.GetArchivesAsync(2015, 1, string.Empty);
            });
        }

        //public async Task TestGetImagesAsync()
        //{
        //    throw new NotImplementedException();
        //}
    }
}