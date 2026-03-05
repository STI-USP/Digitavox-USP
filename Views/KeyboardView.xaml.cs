using Digitavox.Models;
using Digitavox.ViewModels;

namespace Digitavox.Views;

public partial class KeyboardView : ContentPage, IOnPageKeyPress
{
	public KeyboardView(KeyboardViewModel keyboardViewModel)
	{
		InitializeComponent();
		BindingContext = keyboardViewModel;
	}
    public bool OnPageKeyPress(int keyCode, int modifiers)
    {
		return ((KeyboardViewModel)BindingContext).OnPageKeyPress(keyCode, modifiers);
    }
    public bool OnPageKeyDown(int keyCode)
    {
        return ((KeyboardViewModel)BindingContext).OnPageKeyDown(keyCode);
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is KeyboardViewModel vm)
        {
            vm.OnPage();
        }
    }
}