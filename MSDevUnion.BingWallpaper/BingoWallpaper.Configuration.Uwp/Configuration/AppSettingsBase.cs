﻿using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using Windows.Storage;

namespace BingoWallpaper.Configuration
{
    public abstract class AppSettingsBase : IAppSettings
    {
        protected AppSettingsBase()
        {
            ApplicationData.Current.DataChanged += RoamingApplicationDataChanged;
        }

        public event PropertyChangedEventHandler PropertyChanged;

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
                    throw new NotSupportedException();

                case ApplicationDataLocality.LocalCache:
                    throw new NotSupportedException();

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
                    {
                        if (ApplicationData.Current.LocalSettings.Values.ContainsKey(key))
                        {
                            return (T)ApplicationData.Current.LocalSettings.Values[key];
                        }
                        return defaultValue != null ? defaultValue() : default(T);
                    }

                case ApplicationDataLocality.Roaming:
                    {
                        if (ApplicationData.Current.RoamingSettings.Values.ContainsKey(key))
                        {
                            return (T)ApplicationData.Current.RoamingSettings.Values[key];
                        }
                        return defaultValue != null ? defaultValue() : default(T);
                    }

                case ApplicationDataLocality.Temporary:
                    throw new NotSupportedException();

                case ApplicationDataLocality.LocalCache:
                    throw new NotSupportedException();

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
                    var applicationData = ApplicationData.Current;
                    result = applicationData.RoamingSettings.Values.Remove(key);
                    applicationData.SignalDataChanged();
                    break;

                case ApplicationDataLocality.Temporary:
                    throw new NotSupportedException();

                case ApplicationDataLocality.LocalCache:
                    throw new NotSupportedException();

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
                    {
                        object set = value;
                        if (typeof(T).GetTypeInfo().IsEnum)
                        {
                            set = Convert.ChangeType(value, Enum.GetUnderlyingType(typeof(T)));
                        }
                        ApplicationData.Current.LocalSettings.Values[key] = set;
                        RaisePropertyChanged(key);
                        break;
                    }

                case ApplicationDataLocality.Roaming:
                    {
                        object set = value;
                        if (typeof(T).GetTypeInfo().IsEnum)
                        {
                            set = Convert.ChangeType(value, Enum.GetUnderlyingType(typeof(T)));
                        }
                        var applicationData = ApplicationData.Current;
                        applicationData.RoamingSettings.Values[key] = set;
                        RaisePropertyChanged(key);
                        applicationData.SignalDataChanged();
                        break;
                    }

                case ApplicationDataLocality.Temporary:
                    throw new NotSupportedException();

                case ApplicationDataLocality.LocalCache:
                    throw new NotSupportedException();

                default:
                    throw new ArgumentOutOfRangeException(nameof(dataLocality));
            }
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