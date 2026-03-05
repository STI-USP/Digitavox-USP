using Digitavox.Models;
using Digitavox.ViewModels;

namespace Digitavox.Views;

public partial class PrivacyPolicyView : ContentPage, IOnPageKeyPress
{
	public PrivacyPolicyView(PrivacyPolicyViewModel privacyPolicyViewModel)
	{
		InitializeComponent();
		BindingContext = privacyPolicyViewModel;
	}
    public bool OnPageKeyDown(int keyCode)
    {
        return ((PrivacyPolicyViewModel)BindingContext).OnPageKeyDown(keyCode);
    }
    public bool OnPageKeyPress(int keyCode, int modifiers)
    {
        return ((PrivacyPolicyViewModel)BindingContext).OnPageKeyPress(keyCode, modifiers);
    }
}