using System;
using System.ComponentModel;
using Windows.Storage;

namespace BingoWallpaper.Configuration
{
    public interface IAppSettings : INotifyPropertyChanged
    {
        bool Contains(string key, ApplicationDataLocality dataLocality = ApplicationDataLocality.Local);

        T Get<T>(string key, Func<T> defaultValue = null, ApplicationDataLocality dataLocality = ApplicationDataLocality.Local);

        bool Remove(string key, ApplicationDataLocality dataLocality = ApplicationDataLocality.Local);

        void Set<T>(string key, T value, ApplicationDataLocality dataLocality = ApplicationDataLocality.Local);
    }
}