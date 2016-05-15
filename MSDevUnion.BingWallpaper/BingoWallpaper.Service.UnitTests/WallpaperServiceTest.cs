﻿using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace BingoWallpaper.Service.UnitTests
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

        [Test]
        public async Task TestGetImageAsync()
        {
            var image = await _service.GetImageAsync("559d0e88e4b03bd51879a0de");
            Assert.AreEqual(image.ErrorCode, 0);
            Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await _service.GetImageAsync(null);
            });
            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await _service.GetImageAsync(string.Empty);
            });
        }

        [Test]
        public async Task TestGetImagesAsync()
        {
            var images = await _service.GetImagesAsync("559d0e88e4b03bd51879a0de", "559d0e88e4b0a35bc506fbf8");
            Assert.AreEqual(images.ErrorCode, 0);
            Assert.AreEqual(images.Results.Count, 2);
            Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await _service.GetImagesAsync(null);
            });
        }
    }
}