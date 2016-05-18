using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.Storage;

namespace BingoWallpaper.Uwp.Configuration
{
    public class AppSettings : ISettings
    {
        private AppSettings()
        {
            ApplicationData.Current.DataChanged += RoamingApplicationDataChanged;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public static ISettings Current
        {
            get;
        } = new AppSettings();

        public bool Contains(string key, ApplicationDataLocality dataLocality = ApplicationDataLocality.Local)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            switch (dataLocality)
            {
                case ApplicationDataLocality.Local:
                    return ApplicationData.Current.LocalSettings.Values.ContainsKey(key);

                case ApplicationDataLocality.Roaming:
                    return ApplicationData.Current.RoamingSettings.Values.ContainsKey(key);

                case ApplicationDataLocality.Temporary:
                    throw new NotImplementedException();

                case ApplicationDataLocality.LocalCache:
                    throw new NotImplementedException();

                default:
                    throw new ArgumentOutOfRangeException(nameof(dataLocality));
            }
        }

        public T Get<T>(string key, Func<T> defaultValue = null, ApplicationDataLocality dataLocality = ApplicationDataLocality.Local)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            switch (dataLocality)
            {
                case ApplicationDataLocality.Local:
                    if (ApplicationData.Current.LocalSettings.Values.ContainsKey(key))
                    {
                        return (T)ApplicationData.Current.LocalSettings.Values[key];
                    }
                    return defaultValue != null ? defaultValue() : default(T);

                case ApplicationDataLocality.Roaming:
                    if (ApplicationData.Current.RoamingSettings.Values.ContainsKey(key))
                    {
                        return (T)ApplicationData.Current.RoamingSettings.Values[key];
                    }
                    return defaultValue != null ? defaultValue() : default(T);

                case ApplicationDataLocality.Temporary:
                    throw new NotImplementedException();

                case ApplicationDataLocality.LocalCache:
                    throw new NotImplementedException();

                default:
                    throw new ArgumentOutOfRangeException(nameof(dataLocality));
            }
        }

        public bool Remove(string key, ApplicationDataLocality dataLocality = ApplicationDataLocality.Local)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            bool result;
            switch (dataLocality)
            {
                case ApplicationDataLocality.Local:
                    result = ApplicationData.Current.LocalSettings.Values.Remove(key);
                    break;

                case ApplicationDataLocality.Roaming:
                    result = ApplicationData.Current.RoamingSettings.Values.Remove(key);
                    ApplicationData.Current.SignalDataChanged();
                    break;

                case ApplicationDataLocality.Temporary:
                    throw new NotImplementedException();

                case ApplicationDataLocality.LocalCache:
                    throw new NotImplementedException();

                default:
                    throw new ArgumentOutOfRangeException(nameof(dataLocality));
            }

            RaisePropertyChanged(key);
            return result;
        }

        public void Set<T>(string key, T value, ApplicationDataLocality dataLocality = ApplicationDataLocality.Local)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            switch (dataLocality)
            {
                case ApplicationDataLocality.Local:
                    ApplicationData.Current.LocalSettings.Values[key] = value;
                    break;

                case ApplicationDataLocality.Roaming:
                    ApplicationData.Current.RoamingSettings.Values[key] = value;
                    ApplicationData.Current.SignalDataChanged();
                    break;

                case ApplicationDataLocality.Temporary:
                    throw new NotImplementedException();

                case ApplicationDataLocality.LocalCache:
                    throw new NotImplementedException();

                default:
                    throw new ArgumentOutOfRangeException(nameof(dataLocality));
            }

            RaisePropertyChanged(key);
        }

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void RoamingApplicationDataChanged(ApplicationData sender, object args)
        {
            RaisePropertyChanged(string.Empty);
        }
    }
}