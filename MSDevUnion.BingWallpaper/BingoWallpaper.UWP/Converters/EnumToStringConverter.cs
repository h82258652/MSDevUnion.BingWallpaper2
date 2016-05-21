using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Runtime.InteropServices;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml.Data;

namespace BingoWallpaper.Uwp.Converters
{
    public class EnumToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var enumeration = value as Enum;
            if (enumeration != null)
            {
                string resourceMapName = null;
                string resourceKey = null;
                var displayAttribute = value.GetType().GetField(enumeration.ToString()).GetCustomAttribute<DisplayAttribute>();
                if (displayAttribute != null)
                {
                    resourceMapName = displayAttribute.GroupName;
                    resourceKey = displayAttribute.Name;
                }
                ResourceLoader resourceMap = null;
                if (resourceMapName != null)
                {
                    try
                    {
                        resourceMap = ResourceLoader.GetForCurrentView(resourceMapName);
                    }
                    catch (COMException)
                    {
                        // 没找到该资源文件。
                    }
                }
                resourceMap = resourceMap ?? ResourceLoader.GetForCurrentView();
                resourceKey = resourceKey ?? enumeration.ToString();
                var localizationString = resourceMap.GetString(resourceKey);
                return string.IsNullOrEmpty(localizationString) ? enumeration.ToString() : resourceMap.GetString(resourceKey);
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}