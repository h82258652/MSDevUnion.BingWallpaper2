using BingoWallpaper.Models;
using BingoWallpaper.Models.LeanCloud;
using BingoWallpaper.Services;
using BingoWallpaper.Uwp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BingoWallpaper.Uwp.Services
{
    public class LeanCloudWallpaperWithCacheService : LeanCloudWallpaperService
    {
        public override async Task<LeanCloudResultCollection<Archive>> GetArchivesAsync(int year, int month, string area)
        {
            var viewMonth = new DateTime(year, month, 1);
            ValidateGetArchivesAsyncParameters(viewMonth, area);

            using (var db = new WallpaperContext())
            {
                var result = await db.Archives.Where(temp => temp.Area == area && temp.Date.StartsWith(viewMonth.ToString("yyyyMM"))).OrderByDescending(temp => temp.Date).ToListAsync();
                if (result.Count >= DateTime.DaysInMonth(year, month))
                {
                    // 该月数据已全部在缓存当中。
                    return new LeanCloudResultCollection<Archive>()
                    {
                        Results = result.Select(temp => (Archive)temp).ToList()
                    };
                }

                // 下载数据并存储到数据库中。
                var collection = await base.GetArchivesAsync(viewMonth, area);
                var entityAdded = false;
                foreach (var archive in collection)
                {
                    if (await db.Archives.AnyAsync(temp => temp.Id == archive.ObjectId) == false)
                    {
                        db.Archives.Add((ArchiveRepository)archive);
                        entityAdded = true;
                    }
                }
                if (entityAdded)
                {
                    await db.SaveChangesAsync();
                }

                return collection;
            }
        }

        public override async Task<Image> GetImageAsync(string objectId)
        {
            if (objectId == null)
            {
                throw new ArgumentNullException(nameof(objectId));
            }
            if (objectId.Length <= 0)
            {
                throw new ArgumentException($"{nameof(objectId)} 不能为空字符串。");
            }

            using (var db = new WallpaperContext())
            {
                var imageRepository = await db.Images.FirstOrDefaultAsync(temp => temp.Id == objectId);
                if (imageRepository != null)
                {
                    return (Image)imageRepository;
                }

                var image = await base.GetImageAsync(objectId);
                if (await db.Images.AnyAsync(temp => temp.Id == objectId) == false)
                {
                    db.Images.Add((ImageRepository)image);
                    await db.SaveChangesAsync();
                }
                return image;
            }
        }

        public override async Task<LeanCloudResultCollection<Image>> GetImagesAsync(IEnumerable<string> objectIds)
        {
            if (objectIds == null)
            {
                throw new ArgumentNullException(nameof(objectIds));
            }

            var objectIdArray = objectIds as string[] ?? objectIds.ToArray();
            using (var db = new WallpaperContext())
            {
                var images = await db.Images.Where(temp => objectIdArray.Contains(temp.Id)).ToListAsync();
                if (images.Count >= objectIdArray.Length)
                {
                    // 请求的数据已全部缓存，直接返回。
                    return new LeanCloudResultCollection<Image>()
                    {
                        Results = images.Select(temp => (Image)temp).ToList()
                    };
                }

                // 筛选未缓存的 Id。
                var uncacheImageIds = objectIdArray.Where(objectId => images.Any(image => image.Id == objectId) == false);
                var uncacheImageCollection = await base.GetImagesAsync(uncacheImageIds);
                var entityAdded = false;
                foreach (var image in uncacheImageCollection)
                {
                    if (await db.Images.AnyAsync(temp => temp.Id == image.ObjectId) == false)
                    {
                        db.Images.Add((ImageRepository)image);
                        entityAdded = true;
                    }
                }
                if (entityAdded)
                {
                    await db.SaveChangesAsync();
                }

                var resuls = uncacheImageCollection.Results;
                uncacheImageCollection.Results = images.Select(temp => (Image)temp).Concat(resuls).ToList();
                return uncacheImageCollection;
            }
        }
    }
}