using System;
using Microsoft.Maui;
#if __IOS__
using UIKit;
#endif

namespace Digitavox.Helpers
{
    public static class DVVoiceOverHelper
    {
        public static bool IsVoiceOverEnabled()
        {
            #if __IOS__
                return UIKit.UIAccessibility.IsVoiceOverRunning;
            #else
                return false;
            #endif
        }

        public static bool HasNotch()
        {
            #if __IOS__
                var keyWindow = UIApplication.SharedApplication.Windows[0];
                var bottomSafeArea = keyWindow.SafeAreaInsets.Bottom;
                return bottomSafeArea > 0;
            #else
                return false;
            #endif
        }
    }

}

