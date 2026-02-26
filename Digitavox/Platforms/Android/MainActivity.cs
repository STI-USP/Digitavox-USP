using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Speech.Tts;
using Android.Views;
using Android.Views.Accessibility;
using CommunityToolkit.Mvvm.Messaging;
using Digitavox.Helpers;
using Digitavox.Models;
using Digitavox.PlatformsImplementations;
using Java.Util;
using Locale = Java.Util.Locale;
using TextToSpeech = Android.Speech.Tts.TextToSpeech;

namespace Digitavox;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]

public class MainActivity : MauiAppCompatActivity, TextToSpeech.IOnInitListener, TextToSpeech.IOnUtteranceCompletedListener
{
    private TextToSpeech textToSpeech;
    private AccessibilityManager am;
    private bool active;

    protected override void OnCreate(Bundle savedInstanceState) {
        base.OnCreate(savedInstanceState);
        active = true;
        Window.SetStatusBarColor(Android.Graphics.Color.Rgb(253, 180, 32)); //  [jo:231020] cor azul #347dc2 = (52, 125, 194) [jo:231109] cor laranja #FDB420 = (253, 180, 32s)
        am = (AccessibilityManager)GetSystemService(Context.AccessibilityService);
        WeakReferenceMessenger.Default.Register<DVMessage>(this, (r, m) => {
            if (m.Value == "WindowActivated")
            {
                //CheckTalkback();
            }
        });
       // am.TouchExplorationStateChange += CheckTalkbackHandler;
    }
    private void CheckTalkbackHandler(object sender, EventArgs e)
    {
        if (active) CheckTalkback();
    }
    private void CheckTalkback()
    {
        if (am.IsEnabled && am.IsTouchExplorationEnabled)
        {
            WeakReferenceMessenger.Default.Send(new DVMessage("DisplayAlertDialog"));
        }
        else
        {
            WeakReferenceMessenger.Default.Send(new DVMessage("DismissAlertDialog"));
        }
    }
    protected override void OnPause()
    {
        base.OnPause();
        active = false;
    }
    protected override void OnResume()
    {
        DVSpeak.GetInstance().Init(this);
        base.OnResume();
        active = true;
        //am = (AccessibilityManager)GetSystemService(Context.AccessibilityService);
        //CheckTalkback();
        FullScreen();
    }
    public TextToSpeech createTextToSpeech()
    {
        textToSpeech = new TextToSpeech(this, this);
        textToSpeech.SetSpeechRate(DVPersistence.Get<int>("speakRate"));
        return textToSpeech;
    }
    public void OnInit([GeneratedEnum] OperationResult status)
    {
        if (status == OperationResult.Success)
        {
            textToSpeech.SetLanguage(new Locale("pt", "BR"));
            textToSpeech.SetOnUtteranceCompletedListener(this);
            DVSpeak.GetInstance().InitCompleted();
        }
    }
    public void OnUtteranceCompleted(string utteranceId)
    {
        DVSpeak.GetInstance().Completed(utteranceId);
    }
    public override bool OnKeyDown([GeneratedEnum] Keycode keyCode, KeyEvent e)
    {
        Page p = Shell.Current.CurrentPage;
        if (p is IOnPageKeyPress)
        {
            bool handled = (p as IOnPageKeyPress).OnPageKeyDown((int)keyCode);
            if (handled) return true;
            else return base.OnKeyDown(keyCode, e);
        }
        else return base.OnKeyDown(keyCode, e);
    }
    public override bool OnKeyUp([GeneratedEnum] Keycode keyCode, KeyEvent e)
    {
        Page p = Shell.Current.CurrentPage;
        if (p is IOnPageKeyPress)
        {
          int keyModifiers = 0;
          if (e.IsCapsLockOn)
            DVKeyboard.SetModifier(Modifier.CapsLock, ref keyModifiers);
          if (e.IsCtrlPressed)
            DVKeyboard.SetModifier(Modifier.Ctrl, ref keyModifiers);
          if (e.IsAltPressed)
            DVKeyboard.SetModifier(Modifier.AltGr, ref keyModifiers);
          if (e.IsFunctionPressed)
            DVKeyboard.SetModifier(Modifier.Fn, ref keyModifiers);
          if (e.IsShiftPressed)
            DVKeyboard.SetModifier(Modifier.Shift, ref keyModifiers); // [jo:230705]
          if (e.IsNumLockOn)
            DVKeyboard.SetModifier(Modifier.NumLock, ref keyModifiers); // [jo:230705]
          //bool handled = (p as IOnPageKeyPress).OnPageKeyPress((int)keyCode, 
          //        32 * (e.IsNumLockOn ? 1 : 0) + 2 * (e.IsShiftPressed ? 1 : 0));
          bool handled = (p as IOnPageKeyPress).OnPageKeyPress((int)keyCode,
                  keyModifiers);
          if (handled) return true;
              else return base.OnKeyUp(keyCode, e);
          }
          else return base.OnKeyUp(keyCode, e);
    }
    private void FullScreen()
    {
        //this.Window.DecorView.SystemUiVisibility = (StatusBarVisibility)
        //    (SystemUiFlags.ImmersiveSticky | SystemUiFlags.HideNavigation |
        //     SystemUiFlags.Fullscreen | SystemUiFlags.Immersive);
        this.Window.DecorView.SystemUiVisibility = (StatusBarVisibility)
            (SystemUiFlags.HideNavigation | SystemUiFlags.Immersive);
    }
}