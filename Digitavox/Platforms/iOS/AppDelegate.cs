using Foundation;
using UIKit;
using Digitavox.Helpers;
using Digitavox.ViewModels;
using Digitavox.Platforms.iOS;


namespace Digitavox;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
    private bool initialVoiceOverStatus;
    public static DVViewModelSpeak ViewModelSpeak { get; private set; }

    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

    public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
    {
        base.FinishedLaunching(application, launchOptions);

        ViewModelSpeak = new DVViewModelSpeak();

        return true;
    }

    public override void OnActivated(UIApplication application)
    {
        base.OnActivated(application);
        AccessibilityHelper.PersistVoiceOverStatusAndNotify();
    }

    public override void OnResignActivation(UIApplication application)
    {
        base.OnResignActivation(application);
        AppDelegate.ViewModelSpeak.Skip();
        AccessibilityHelper.CheckVoiceOverStatusChangeAndNotify();
    }


}
