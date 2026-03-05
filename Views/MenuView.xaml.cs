using Digitavox.Models;
using Digitavox.ViewModels;

namespace Digitavox.Views;

public partial class MenuView : ContentPage, IOnPageKeyPress
{
	public MenuView(MenuViewModel menuViewModel)
	{
		InitializeComponent();
		BindingContext = menuViewModel;
    }
    public bool OnPageKeyPress(int keyCode, int modifiers)
    {
		return ((MenuViewModel)BindingContext).OnPageKeyPress(keyCode, modifiers);
    }
    public bool OnPageKeyDown(int keyCode)
    {
        return ((MenuViewModel)BindingContext).OnPageKeyDown(keyCode);
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is MenuViewModel vm)
        {
            vm.OnPage();
        }
    }
}