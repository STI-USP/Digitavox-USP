using Digitavox.Models;
using Digitavox.ViewModels;

namespace Digitavox.Views;

public partial class CoursesHelpView : ContentPage, IOnPageKeyPress
{
    public CoursesHelpView(CoursesHelpViewModel coursesHelpViewModel)
    {
        InitializeComponent();
        BindingContext = coursesHelpViewModel;
    }
    public bool OnPageKeyPress(int keyCode, int modifiers)
    {
        return ((CoursesHelpViewModel)BindingContext).OnPageKeyPress(keyCode, modifiers);
    }
    public bool OnPageKeyDown(int keyCode)
    {
        return ((CoursesHelpViewModel)BindingContext).OnPageKeyDown(keyCode);
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is CoursesHelpViewModel vm)
        {
            vm.OnPage();
        }
    }
}