using Android.Runtime;
using Android.Speech.Tts;
using Digitavox.Helpers;
using Java.Interop;
using Java.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextToSpeech = Android.Speech.Tts.TextToSpeech;

namespace Digitavox.PlatformsImplementations
{
    public partial class DVSpeak
    {
        /*private static IEnumerable<Locale> locales;
        private static List<CancellationTokenSource> cancellationTokenSources = new List<CancellationTokenSource>();*/
        private TextToSpeech textToSpeech;
        private MainActivity mainActivity;
        private bool initialized;
        private string initialText;
        private Action initialOnFinished;
        private Dictionary<string, Action> callbacks = new Dictionary<string, Action>();
        public partial void Cancel()
        {
            /*List<CancellationTokenSource> newList = new List<CancellationTokenSource>(cancellationTokenSources);
            foreach (CancellationTokenSource cancellationTokenSource in newList)
            {
                cancellationTokenSource.Cancel();
            }
            cancellationTokenSources.RemoveAll((a) => newList.Contains(a));*/
            /*foreach (var callback in callbacks.Values)
            {
                callback.Invoke();
            }
            callbacks.Clear();*/
            if (initialized) textToSpeech.PlaySilentUtterance(1, QueueMode.Flush, null);
        }
        public partial void Init(object obj)
        {
            /*privateInit();*/
            mainActivity = (MainActivity)obj;
            textToSpeech = mainActivity.createTextToSpeech();
        }
        public partial void InitCompleted()
        {
            initialized = true;
            if (initialText != null)
            {
                string initialTextCopy = initialText;
                Action initialOnFinishedCopy = initialOnFinished;
                initialText = null;
                initialOnFinished = null;
                SpeakText(initialTextCopy, initialOnFinishedCopy);
            }
        }
        /*private static async void privateInit()
        {
            locales = await TextToSpeech.GetLocalesAsync();
        }*/
        public partial void SpeakText(string text, Action onFinished)
        {
            /*CancellationTokenSource cts = new CancellationTokenSource();
            SpeechOptions options = new SpeechOptions()
            {
                Volume = 1.0f,
                Locale = (DVDevice.IsAndroid() ?
                        locales.SingleOrDefault(l => l.Name == "Portuguese (Brazil)") : // se for Android
                        locales.FirstOrDefault(l => l.Language == "pt-BR"))             // se não Android, é iOS ou Mac
            };
            cancellationTokenSources.Add(cts);
            TextToSpeech.Default.SpeakAsync(text, options, cancelToken: cts.Token).ContinueWith((obj) =>
            {
                cancellationTokenSources.Remove(cts);
                onFinished.Invoke();
            });*/
            if (initialized)
            {
                String utteranceId = UUID.RandomUUID().ToString();
                textToSpeech.Speak(text, QueueMode.Add, null, utteranceId);
                callbacks[utteranceId] = onFinished;
            } 
            else
            {
                initialText = text;
                initialOnFinished = onFinished;
            }
        }
        public partial void Completed(object id)
        {
            if (callbacks.ContainsKey((string)id))
            {
                callbacks[(string)id].Invoke();
                callbacks.Remove((string)id);
            }
        }
        public partial void SetSpeechRate(int speechRate)
        {
            if (textToSpeech != null)
            {
                textToSpeech.SetSpeechRate(speechRate * 0.8f + 0.2f);
            }
        }
    }
}
