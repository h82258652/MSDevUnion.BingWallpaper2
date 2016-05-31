using BingoWallpaper.Models;
using BingoWallpaper.Services;
using BingoWallpaper.Uwp.Repositories;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Connectivity;

namespace BingoWallpaper.Uwp.Services
{
    //public class WallpaperWithCacheService : WallpaperService
    //{
    //    public override async Task<LeanCloudResultCollection<Archive>> GetArchivesAsync(int year, int month, string area)
    //    {
    //        var viewMonth = new DateTime(year, month, 1);
    //        ValidateGetArchivesAsyncParameters(viewMonth, area);

    //        using (var db = new WallpaperContext())
    //        {
    //            var result = await db.Archives.Where(temp => temp.Market == area && temp.Date.StartsWith(viewMonth.ToString("yyyyMM"))).ToListAsync();
    //            if (result.Count >= DateTime.DaysInMonth(year, month))
    //            {
    //                // 该月数据已全部在缓存当中。
    //                return new LeanCloudResultCollection<Archive>()
    //                {
    //                    Results = result
    //                };
    //            }

    //            if (NetworkInterface.GetIsNetworkAvailable())
    //            {
    //                // 网络可用，下载数据并存储到数据库中。
    //                var collection = await base.GetArchivesAsync(viewMonth, area);
    //                foreach (var archive in collection)
    //                {
    //                    if (db.Archives.All(temp => temp.ObjectId != archive.ObjectId))
    //                    {
    //                        db.Archives.Add(archive);
    //                    }
    //                }
    //                await db.SaveChangesAsync();
    //            }
    //            return collection;
    //        }

    //        using (var db = new WallpaperContext())
    //        {
    //            if (year == now.Year && month >= now.Month)
    //            {
    //                // 直接加载网络数据。
    //            }
    //            else
    //            {
    //                // 加载缓存
    //            }

    //            var archives = db.Archives.Where(temp => temp.Market == area);
    //        }
    //    }
    //}
}