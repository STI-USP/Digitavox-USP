using Digitavox.Models;
using Digitavox.ViewModels;

namespace Digitavox.Views;

public partial class ExercisesView : ContentPage, IOnPageKeyPress
{
	public ExercisesView(ExercisesViewModel exercisesViewModel)
	{
		InitializeComponent();
		BindingContext = exercisesViewModel;
	}
    public bool OnPageKeyPress(int keyCode, int modifiers)
    {
        return ((ExercisesViewModel)BindingContext).OnPageKeyPress(keyCode, modifiers);
    }
    public bool OnPageKeyDown(int keyCode)
    {
        return ((ExercisesViewModel)BindingContext).OnPageKeyDown(keyCode);
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is ExercisesViewModel vm)
        {
            vm.OnPage();
        }
    }
}