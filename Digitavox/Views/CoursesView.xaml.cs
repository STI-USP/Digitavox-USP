using Digitavox.Models;
using Digitavox.ViewModels;

namespace Digitavox.Views;

public partial class CoursesView : ContentPage, IOnPageKeyPress
{
	public CoursesView(CoursesViewModel coursesViewModel)
	{
		InitializeComponent();
        BindingContext = coursesViewModel;
    }
    public bool OnPageKeyPress(int keyCode, int modifiers)
    {
        return ((CoursesViewModel)BindingContext).OnPageKeyPress(keyCode, modifiers);
    }
    public bool OnPageKeyDown(int keyCode)
    {
        return ((CoursesViewModel)BindingContext).OnPageKeyDown(keyCode);
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is CoursesViewModel vm)
        {
            vm.OnPage();
        }
    }
}