using System;

namespace BingoWallpaper.Uwp.Controls
{
    public class ExceptionEventArgs : EventArgs
    {
        private readonly string _errorMessage;

        internal ExceptionEventArgs(string errorMessage)
        {
            _errorMessage = errorMessage;
        }

        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
        }
    }
}