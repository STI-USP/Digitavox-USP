using Digitavox.Models;
using Digitavox.ViewModels;

namespace Digitavox.Views;

public partial class ExercisesHelpView : ContentPage, IOnPageKeyPress
{
	public ExercisesHelpView(ExercisesHelpViewModel exercisesHelpViewModel)
	{
		InitializeComponent();
        BindingContext = exercisesHelpViewModel;
	}
    public bool OnPageKeyPress(int keyCode, int modifiers)
    {
        return ((ExercisesHelpViewModel)BindingContext).OnPageKeyPress(keyCode, modifiers);
    }
    public bool OnPageKeyDown(int keyCode)
    {
        return ((ExercisesHelpViewModel)BindingContext).OnPageKeyDown(keyCode);
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is ExercisesHelpViewModel vm)
        {
            vm.OnPage();
        }
    }
}