using CommunityToolkit.Mvvm.ComponentModel;
using Digitavox.Helpers;
using Digitavox.Models;
using Digitavox.PlatformsImplementations;

namespace Digitavox.ViewModels
{
    public partial class KeyboardViewModel : ObservableObject, IOnPageKeyPress
    {
        List<string> pressedKeys = new List<string>();
        int escPressed = 0;
        [ObservableProperty]
        private FormattedString pageFormattedLabel;
        [ObservableProperty]
        private string _pageLabel;
        [ObservableProperty]
        private double _textSize;
        private DVViewModelSpeak dVViewModelSpeak;
        private DVViewModelFunctions dVViewModelFunctions;
        private FingerMapping fingerMapping;
        public KeyboardViewModel(DVViewModelSpeak dVViewModelSpeak,
                                    DVViewModelFunctions dVViewModelFunctions,
                                    FingerMapping fingerMapping)
        {
            this.dVViewModelSpeak = dVViewModelSpeak;
            this.fingerMapping = fingerMapping;
            this.dVViewModelFunctions = dVViewModelFunctions;
        }
        public void OnPage()
        {
            dVViewModelFunctions.SetCurrentPageIdentifier("na tela de reconhecimento de teclado");
            var textList = new List<string>()
            {
                "Aperte qualquer tecla para ouvir sua identificação, função e qual dedo ideal para digitação.",
                "Para encerrar o reconhecimento, tecle ESCAPE duas vezes.",
                string.Empty
            };

            var speechList = new List<string>()
            {
                "Aperte qualquer tecla para ouvir sua identificação, função e qual dedo ideal para digitação. ",
                "Para encerrar o reconhecimento tecle esqueipe duas vezes.",
                string.Empty
            };
            textList = textList.Select(text => dVViewModelFunctions.EditStringForVoiceOver(text)).ToList();
            speechList = speechList.Select(speech => dVViewModelFunctions.EditStringForVoiceOver(speech)).ToList();

            dVViewModelSpeak.SetTextAndSpeech(textList, speechList).RegisterUpdateScreen((text) =>
            {
                //PageLabel = text;
                //PageFormattedLabel = dVViewModelFunctions.UpdateFormattedText(text);
                PageFormattedLabel = text;
                TextSize = DVPersistence.Get<double>("fontSize");
            });


            escPressed = 0;
            dVViewModelSpeak.SpeakAll();
        }
        private void CountEsc()
        {
            escPressed += 1; // Se apertar outra tecla depois do primeiro esc volta para 0
            if (escPressed == 2) NavigateBack();
        }
        private async void NavigateBack()
        {
            await Shell.Current.GoToAsync("..");
        }
        public bool OnPageKeyDown(int keyCode)
        {
            string code = fingerMapping.mapKeyCode(keyCode);
            if (!pressedKeys.Contains(code))
            {
                pressedKeys.Add(code);
            }
            return true;
        }
        public bool OnPageKeyPress(int keyCode, int modifiers)
        {
            pressedKeys.Remove(fingerMapping.mapKeyCode(keyCode));
            var bean = fingerMapping.MapKey(keyCode, modifiers, pressedKeys);
            if (bean.show != null && bean.speak != null)
            {
                dVViewModelSpeak.Skip();
                if (DVDevice.IsVirtual() && (bean.code != "!" || escPressed == 0))
                {
                    dVViewModelSpeak.ChangeLine(bean.show, bean.speak, 2);
                    dVViewModelSpeak.SpeakOneLine(2, () => { });
                }
                else if (!DVDevice.IsVirtual() && (bean.code != "Escape" || escPressed == 0))

                {
                    dVViewModelSpeak.ChangeLine(bean.show, bean.speak, 2);
                    dVViewModelSpeak.SpeakOneLine(2, () => { });
                }
                if (bean.code == "Escape")
                {
                    CountEsc();
                }
                else if (bean.code == "!" && DVDevice.IsVirtual())
                {
                    CountEsc();
                }
                else
                {
                    escPressed = 0;
                }
            }
            return true;
        }
    }
}
