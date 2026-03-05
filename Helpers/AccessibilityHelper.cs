namespace Digitavox.Helpers
{
    public static class AccessibilityHelper
    {
        public static bool IsHighContrastEnabled()
        {
#if WINDOWS
            var accessibilitySettings = new Windows.UI.ViewManagement.UISettings();
            var backgroundColor = accessibilitySettings.GetColorValue(Windows.UI.ViewManagement.UIColorType.Background);
            var isHighContrast = backgroundColor.ToString().Equals("#FF000000");
            return isHighContrast;
#else
            return false;
#endif
        }
    }
}
