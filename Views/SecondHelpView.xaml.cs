using Digitavox.Models;
using Digitavox.ViewModels;

namespace Digitavox.Views;

public partial class SecondHelpView : ContentPage, IOnPageKeyPress
{
	public SecondHelpView(SecondHelpViewModel secondHelpViewModel)
	{
		InitializeComponent();
		BindingContext = secondHelpViewModel;
    }
    public bool OnPageKeyPress(int keyCode, int modifiers)
    {
        return ((SecondHelpViewModel)BindingContext).OnPageKeyPress(keyCode, modifiers);
    }
    public bool OnPageKeyDown(int keyCode)
    {
        return ((SecondHelpViewModel)BindingContext).OnPageKeyDown(keyCode);
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is SecondHelpViewModel vm)
        {
            vm.OnPage();
        }
    }
}