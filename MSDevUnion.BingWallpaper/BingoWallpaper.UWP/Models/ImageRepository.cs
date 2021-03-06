﻿using BingoWallpaper.Models;
using BingoWallpaper.Models.LeanCloud;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace BingoWallpaper.Uwp.Models
{
    public class ImageRepository
    {
        [Key]
        public string Id
        {
            get;
            set;
        }

        public string Json
        {
            get;
            set;
        }

        public static explicit operator Image(ImageRepository imageRepository)
        {
            return JsonConvert.DeserializeObject<Image>(imageRepository.Json);
        }

        public static explicit operator ImageRepository(Image image)
        {
            return new ImageRepository()
            {
                Id = image.ObjectId,
                Json = JsonConvert.SerializeObject(image)
            };
        }
    }
}