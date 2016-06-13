using BingoWallpaper.Models;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace BingoWallpaper.Uwp.Models
{
    public class ArchiveRepository
    {
        public string Area
        {
            get;
            set;
        }

        public string Date
        {
            get;
            set;
        }

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

        public static explicit operator Archive(ArchiveRepository archiveRepository)
        {
            return JsonConvert.DeserializeObject<Archive>(archiveRepository.Json);
        }

        public static explicit operator ArchiveRepository(Archive archive)
        {
            return new ArchiveRepository()
            {
                Id = archive.ObjectId,
                Json = JsonConvert.SerializeObject(archive),
                Area = archive.Market,
                Date = archive.Date
            };
        }
    }
}