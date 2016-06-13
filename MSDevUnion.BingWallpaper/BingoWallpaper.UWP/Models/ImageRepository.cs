using BingoWallpaper.Models;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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