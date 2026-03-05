using Digitavox.Models;
using Digitavox.ViewModels;

namespace Digitavox.Views;

public partial class LessonsHelpView : ContentPage, IOnPageKeyPress
{
	public LessonsHelpView(LessonsHelpViewModel lessonsHelpViewModel)
	{
		InitializeComponent();
		BindingContext = lessonsHelpViewModel;
	}
    public bool OnPageKeyPress(int keyCode, int modifiers)
    {
        return ((LessonsHelpViewModel)BindingContext).OnPageKeyPress(keyCode, modifiers);
    }
    public bool OnPageKeyDown(int keyCode)
    {
        return ((LessonsHelpViewModel)BindingContext).OnPageKeyDown(keyCode);
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is LessonsHelpViewModel vm)
        {
            vm.OnPage();
        }
    }

}