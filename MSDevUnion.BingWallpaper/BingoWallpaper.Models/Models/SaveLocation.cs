using System.ComponentModel.DataAnnotations;

namespace BingoWallpaper.Models
{
    /// <summary>
    /// 图片保存位置。
    /// </summary>
    public enum SaveLocation
    {
        /// <summary>
        /// 图片库。
        /// </summary>
        [Display(GroupName = "Enums")]
        PictureLibrary,

        /// <summary>
        /// 每次保存前选择。
        /// </summary>
        [Display(GroupName = "Enums")]
        ChooseEveryTime,

        /// <summary>
        /// 保存的图片。
        /// </summary>
        [Display(GroupName = "Enums")]
        SavedPictures
    }
}