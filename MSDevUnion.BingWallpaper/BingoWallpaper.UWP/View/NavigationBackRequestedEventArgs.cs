using System;
using Windows.Phone.UI.Input;
using Windows.UI.Core;

namespace BingoWallpaper.Uwp.View
{
    public class NavigationBackRequestedEventArgs : EventArgs
    {
        private readonly BackPressedEventArgs _backPressedEventArgs;

        private readonly BackRequestedEventArgs _backRequestedEventArgs;

        internal NavigationBackRequestedEventArgs(BackRequestedEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException(nameof(e));
            }

            _backRequestedEventArgs = e;
        }

        internal NavigationBackRequestedEventArgs(BackPressedEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException(nameof(e));
            }

            _backPressedEventArgs = e;
        }

        public bool Handled
        {
            get
            {
                if (_backRequestedEventArgs != null)
                {
                    return _backRequestedEventArgs.Handled;
                }

                if (_backPressedEventArgs != null)
                {
                    if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.BackPressedEventArgs"))
                    {
                        return _backPressedEventArgs.Handled;
                    }
                }
                throw new InvalidOperationException();
            }
            set
            {
                if (_backRequestedEventArgs != null)
                {
                    _backRequestedEventArgs.Handled = value;
                }
                if (_backPressedEventArgs != null)
                {
                    if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.BackPressedEventArgs"))
                    {
                        _backPressedEventArgs.Handled = value;
                    }
                }
            }
        }
    }
}