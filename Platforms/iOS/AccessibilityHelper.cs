using System;
using AVFoundation;
using UIKit;
using Digitavox.PlatformsImplementations;
using Digitavox.Helpers;
using Digitavox.ViewModels;

namespace Digitavox.Platforms.iOS
{
    public static class AccessibilityHelper
    {
        private static void Speak(string message)
        {
            var speechSynthesizer = DVSpeak.GetInstance();
            speechSynthesizer.EnqueueSpeech(message);
        }

        public static void PersistVoiceOverStatusAndNotify()
        {
            bool isVoiceOverRunning = UIAccessibility.IsVoiceOverRunning;
            DVPersistence.Set("VoiceOverStatus", isVoiceOverRunning);

            if (isVoiceOverRunning)
            {
                System.Diagnostics.Debug.WriteLine("VoiceOver ON");
                Speak("Para navegar no app com o VoiceOver, pressione simultaneamente as teclas de seta para a direita e esquerda");
            } else {
                System.Diagnostics.Debug.WriteLine("VoiceOver OFF");
            }
        }

        public static void CheckVoiceOverStatusChangeAndNotify()
        {
            bool initialVoiceOverStatus = DVPersistence.Get<bool>("VoiceOverStatus");
            bool currentVoiceOverStatus = UIAccessibility.IsVoiceOverRunning;

            if (initialVoiceOverStatus && currentVoiceOverStatus)
            {
                System.Diagnostics.Debug.WriteLine("VoiceOver estava ON");
                Speak("Para voltar as teclas de setas para o controle do VoiceOver, pressione as setas para a direita e esquerda simultaneamente.");
            }
        }

    }
}
