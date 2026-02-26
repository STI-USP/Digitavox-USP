using Digitavox.Models;
using Digitavox.ViewModels;

namespace Digitavox.Views;

public partial class LessonsView : ContentPage, IOnPageKeyPress
{
	public LessonsView(LessonsViewModel lessonsViewModel)
	{
		InitializeComponent();
		BindingContext = lessonsViewModel;
	}
    public bool OnPageKeyPress(int keyCode, int modifiers)
    {
        return ((LessonsViewModel)BindingContext).OnPageKeyPress(keyCode, modifiers);
    }
    public bool OnPageKeyDown(int keyCode)
    {
        return ((LessonsViewModel)BindingContext).OnPageKeyDown(keyCode);
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is LessonsViewModel vm)
        {
            vm.OnPage();
        }
    }
}