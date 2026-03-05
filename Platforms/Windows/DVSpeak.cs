using System.Speech.Synthesis;
using Digitavox.Helpers;

namespace Digitavox.PlatformsImplementations
{
    public partial class DVSpeak
    {
        private SpeechSynthesizer speechSynthesizer;
        private Dictionary<string, Action> callbacks = new Dictionary<string, Action>();
        public partial void Cancel()
        {
            speechSynthesizer.SpeakAsyncCancelAll();
        }
        public partial void Init(object obj)
        {
            speechSynthesizer = new SpeechSynthesizer();
            speechSynthesizer.SetOutputToDefaultAudioDevice();
            SetSpeechRate(DVPersistence.Get<int>("speakRate"));
        }
        public partial void InitCompleted()
        {
        }
        public partial void SpeakText(string text, Action onFinished)
        {
            string utteranceId = Guid.NewGuid().ToString();
            callbacks[utteranceId] = onFinished;
            SynthSpeakUtterance(text, utteranceId);
        }
        private void SynthSpeakUtterance(string text, string utteranceId)
        {
            var prompt = speechSynthesizer.SpeakAsync(text);
            speechSynthesizer.SpeakCompleted += (sender, args) =>
            {
                if (prompt.IsCompleted && callbacks.TryGetValue(utteranceId, out Action? value))
                {
                    value.Invoke();
                    callbacks.Remove(utteranceId);
                }
                speechSynthesizer.SpeakCompleted -= (s, a) => { };
            };
        }
        public partial void SetSpeechRate(int speechRate)
        {
            speechSynthesizer.Rate = speechRate;
        }
        public partial void Completed(object id)
        {
        }
    }
}
