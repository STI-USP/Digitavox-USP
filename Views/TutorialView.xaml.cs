using Digitavox.Models;
using Digitavox.ViewModels;

namespace Digitavox.Views;

public partial class TutorialView : ContentPage, IOnPageKeyPress
{
	public TutorialView(TutorialViewModel tutorialViewModel)
	{
		InitializeComponent();
		BindingContext = tutorialViewModel;
	}
    public bool OnPageKeyPress(int keyCode, int modifiers)
    {
        return ((TutorialViewModel)BindingContext).OnPageKeyPress(keyCode, modifiers);
    }
    public bool OnPageKeyDown(int keyCode)
    {
        return ((TutorialViewModel)BindingContext).OnPageKeyDown(keyCode);
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is TutorialViewModel vm)
        {
            vm.OnPage();
        }
    }
}