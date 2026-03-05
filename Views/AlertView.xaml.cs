using Digitavox.Models;
using System.ComponentModel;

namespace Digitavox.Views;

public partial class AlertView : ContentPage, IOnPageKeyPress, INotifyPropertyChanged
{
    private string _alertText;

    public string AlertText
    {
        get { return _alertText; }
        set
        {
            if (_alertText != value)
            {
                _alertText = value;
                OnPropertyChanged(nameof(AlertText));
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    public AlertView()
    {
        InitializeComponent();
        BindingContext = this;
        AlertText = "Para que a síntese de voz do aplicativo funcione adequadamente, é necessário estar com o Talkback desabilitado. Para desativá-lo vá até as configurações e na seção de acessibilidade selecione para desligar ou utilize seu atalho definido. Quando desativar, essa janela será automaticamente removida e o aplicativo passará ao seu funcionamento normal. Caso necessário pressione espaço para repetir o texto da página e lembre-se de ligar o Talkback novamente ao sair do aplicativo.";

    }
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    public bool OnPageKeyPress(int keyCode, int modifiers)
    {
		return true;
    }
    public bool OnPageKeyDown(int keyCode)
    {
        return true;
    }
}