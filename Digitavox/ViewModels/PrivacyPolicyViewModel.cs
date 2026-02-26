using CommunityToolkit.Mvvm.ComponentModel;
using Digitavox.Helpers;
using Digitavox.Models;

namespace Digitavox.ViewModels
{
    public partial class PrivacyPolicyViewModel : IOnPageKeyPress
    {
        List<string> pressedKeys = new List<string>();
        List<string> pageKeyCodes;
        private FingerMapping fingerMapping;
        private DVViewModelFunctions dVViewModelFunctions;
        public PrivacyPolicyViewModel(FingerMapping fingerMapping,
                                      DVViewModelFunctions dVViewModelFunctions)
        {
            this.fingerMapping = fingerMapping;
            this.dVViewModelFunctions = dVViewModelFunctions;
            pageKeyCodes = new List<string>()
            {
                "Escape"
            };
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
            if (bean.code != null)
            {
                if (pageKeyCodes.Contains(bean.code) || (bean.code == "!" && DVDevice.IsVirtual()))
                {
                    dVViewModelFunctions.HandleKeyCode(bean.code);
                }
                //else if (!DVKeyboard.IsModifierKey(bean.code))
                //{
                //    dVViewModelFunctions.InvalidOption(bean.speakOnlyChar);
                //}
            }
            return true;
        }
    }
}
