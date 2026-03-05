using Digitavox.Models;
using Digitavox.ViewModels;

namespace Digitavox.Views;

public partial class ExercisesStatisticsView : ContentPage, IOnPageKeyPress
{
	public ExercisesStatisticsView(ExercisesStatisticsViewModel exercisesStatisticsViewModel)
	{
		InitializeComponent();
        BindingContext = exercisesStatisticsViewModel;
    }
    public bool OnPageKeyPress(int keyCode, int modifiers)
    {
        return ((ExercisesStatisticsViewModel)BindingContext).OnPageKeyPress(keyCode, modifiers);
    }
    public bool OnPageKeyDown(int keyCode)
    {
        return ((ExercisesStatisticsViewModel)BindingContext).OnPageKeyDown(keyCode);
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is ExercisesStatisticsViewModel vm)
        {
            vm.OnPage();
        }
    }
}