using Digitavox.Models;
using Digitavox.ViewModels;

namespace Digitavox.Views;

public partial class ConfigView : ContentPage, IOnPageKeyPress
{
	public ConfigView(ConfigViewModel configViewModel)
	{
		InitializeComponent();
		BindingContext = configViewModel;
	}
    public bool OnPageKeyPress(int keyCode, int modifiers)
    {
        return ((ConfigViewModel)BindingContext).OnPageKeyPress(keyCode, modifiers);
    }
    public bool OnPageKeyDown(int keyCode)
    {
        return ((ConfigViewModel)BindingContext).OnPageKeyDown(keyCode);
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is ConfigViewModel vm)
        {
            vm.OnPage();
        }
    }
}