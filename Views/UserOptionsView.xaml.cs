using Digitavox.Models;
using Digitavox.ViewModels;

namespace Digitavox.Views;

public partial class UserOptionsView : ContentPage, IOnPageKeyPress
{
	public UserOptionsView(UserOptionsViewModel userOptionsViewModel)
	{
		InitializeComponent();
		BindingContext = userOptionsViewModel;
	}
    public bool OnPageKeyPress(int keyCode, int modifiers)
    {
        return ((UserOptionsViewModel)BindingContext).OnPageKeyPress(keyCode, modifiers);
    }
    public bool OnPageKeyDown(int keyCode)
    {
        return ((UserOptionsViewModel)BindingContext).OnPageKeyDown(keyCode);
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is UserOptionsViewModel vm)
        {
            vm.OnPage();
        }
    }
}