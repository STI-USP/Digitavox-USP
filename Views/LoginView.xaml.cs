using Digitavox.Models;
using Digitavox.ViewModels;

namespace Digitavox.Views;

public partial class LoginView : ContentPage, IOnPageKeyPress
{
	public LoginView(LoginViewModel loginViewModel)
	{
		InitializeComponent();
		BindingContext = loginViewModel;
    }
    public bool OnPageKeyPress(int keyCode, int modifiers)
    {
        return ((LoginViewModel)BindingContext).OnPageKeyPress(keyCode, modifiers);
    }
    public bool OnPageKeyDown(int keyCode)
    {
        return ((LoginViewModel)BindingContext).OnPageKeyDown(keyCode);
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is LoginViewModel vm)
        {
            vm.OnPage();
        }
    }
}