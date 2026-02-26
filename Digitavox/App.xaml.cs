using CommunityToolkit.Mvvm.Messaging;
using Digitavox.Helpers;
using Digitavox.ViewModels;
using Digitavox.Views;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace Digitavox;

public partial class App : Application
{
	public App(DVViewModelSpeak dVViewModelSpeak, DVViewModelFunctions dVViewModelFunctions)
	{
		InitializeComponent();
		MainPage = new AppShell();

        WeakReferenceMessenger.Default.Register<DVMessage>(this, (r, m) => 
        {
            if (m.Value == "WindowStopped")
            {
            #if __ANDROID__
                dVViewModelSpeak.Skip();
            #endif
            //Para iOS, é tratado no AppDelegate

            }
            else if (m.Value == "WindowResumed")
            {
                string currentPageMessage = $"Você está {dVViewModelFunctions.CurrentPageIdentifier()}";
                dVViewModelSpeak.Skip();
                dVViewModelSpeak.Speak(currentPageMessage, () => { });
            }
            if (m.Value == "DisplayAlertDialog")
            {
                dVViewModelSpeak.Skip();
                //DisplayAlert();
                dVViewModelFunctions.DisplayAlert();
            }
            else if (m.Value == "DismissAlertDialog")
            {
                dVViewModelFunctions.DismissAlert();
            }
        });

        // Application.Current.RequestedThemeChanged += OnRequestedThemeChanged;

    }
    //private void DisplayAlert()
    //{
    //    //await Shell.Current.DisplayAlert("Desligue o talkback", "Vá para a as configurações e desabilite o talkback ou utilize seu atalho configurado para uma melhor experiência no Digitavox USP. Para sair dessa caixa de diálogo aperte enter e lembre-se de pressionar espaço para repetir o texto da página, exceto se estiver dentro da lição.", "OK");
    //    WeakReferenceMessenger.Default.Send(new DVMessage("CheckForScreenReader"));
    //}
    protected override Window CreateWindow(IActivationState activationState)
    {
        Window window = base.CreateWindow(activationState);
        window.Created += (s, e) =>
        {
            WeakReferenceMessenger.Default.Send(new DVMessage("WindowCreated"));
        };
        window.Activated += (s, e) =>
        {
            WeakReferenceMessenger.Default.Send(new DVMessage("WindowActivated"));
        };
        window.Deactivated += (s, e) =>
        {
            WeakReferenceMessenger.Default.Send(new DVMessage("WindowDeactivated"));
        };
        window.Stopped += (s, e) =>
        {
            WeakReferenceMessenger.Default.Send(new DVMessage("WindowStopped"));
        };
        window.Resumed += (s, e) =>
        {
            WeakReferenceMessenger.Default.Send(new DVMessage("WindowResumed"));
        };
        window.Destroying += (s, e) =>
        {
            WeakReferenceMessenger.Default.Send(new DVMessage("WindowDestroying"));
        };
        return window;
    }

    private void OnRequestedThemeChanged(object sender, AppThemeChangedEventArgs e)
    {
        Dispatcher.Dispatch(() =>
        {
            var theme = e.RequestedTheme;
            if (theme == AppTheme.Dark)
            {
                Resources["DynamicTextColor"] = Resources["TextColorDark"];
            }
            else
            {
                Resources["DynamicTextColor"] = Resources["TextColorLight"];
            }
        });
    }

    protected override void OnStart()
    {
        base.OnStart();
    }

    // Desassina o evento quando a aplicação entrar em background
    protected override void OnSleep()
    {
        base.OnSleep();
        //Application.Current.RequestedThemeChanged -= OnRequestedThemeChanged;
    }

    // Assina o evento quando a aplicação voltar para foreground
    protected override void OnResume()
    {
        base.OnResume();
        //Application.Current.RequestedThemeChanged += OnRequestedThemeChanged;
    }

}
