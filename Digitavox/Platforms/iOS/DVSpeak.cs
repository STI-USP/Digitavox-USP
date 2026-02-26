using AVFoundation;
using Foundation;

namespace Digitavox.PlatformsImplementations {

  public partial class DVSpeak : AVSpeechSynthesizerDelegate {

    private static Lazy<AVSpeechSynthesizer> lazySynthesizer = new Lazy<AVSpeechSynthesizer>(() => new AVSpeechSynthesizer());
    private AVSpeechSynthesizer synthesizer => lazySynthesizer.Value;
    //private AVSpeechSynthesizer synthesizer;
    private float iosSpeechRate;
    private Dictionary<string, Action> callbacks = new Dictionary<string, Action>();
    private Dictionary<AVSpeechUtterance, string> utteranceToIdMap = new Dictionary<AVSpeechUtterance, string>();

    public partial void Cancel() {
      //Console.WriteLine("Cancel Speech");
      synthesizer.StopSpeaking(AVSpeechBoundary.Immediate);
      float savedRate = NSUserDefaults.StandardUserDefaults.FloatForKey("speechRateKey");
      iosSpeechRate = savedRate != 0 ? savedRate : 0.5f;
    }

    public partial void Init(object obj) {
      //Console.WriteLine("Init Speech");
      synthesizer.Delegate = this;
      synthesizer.UsesApplicationAudioSession = true; // [jo:231016]
    }

    public partial void InitCompleted() {
      //Console.WriteLine("Init Completed");
    }

    public partial void SpeakText(string text, Action onFinished) {
        var utteranceId = Guid.NewGuid().ToString();
        var utterance = new AVSpeechUtterance(text) {
            Voice = AVSpeechSynthesisVoice.FromLanguage("pt-BR"),
            Rate = iosSpeechRate
        };
        synthesizer.SpeakUtterance(utterance);
        callbacks[utteranceId] = onFinished;
        utteranceToIdMap[utterance] = utteranceId;
    }

    public partial void SetSpeechRate(int speechRate) {
      //Console.WriteLine("Set Speech Rate");
      iosSpeechRate = ((float)speechRate - 1) * 0.0625f + 0.5f; // ajustar 0.1f para velocidade
      NSUserDefaults.StandardUserDefaults.SetFloat(iosSpeechRate, "speechRateKey");
    }

    public partial void Completed(object id) {
      //Console.WriteLine("Completed Speech");
    }


    private Queue<string> speechQueue = new Queue<string>();

    public void EnqueueSpeech(string text)
    {
        speechQueue.Enqueue(text);
        if (!synthesizer.Speaking)
        {
            SpeakNextInQueue();
        }
    }

    private void SpeakNextInQueue()
    {
        if (speechQueue.Count > 0)
        {
            string nextSpeech = speechQueue.Dequeue();
            SpeakText(nextSpeech, () => {
                // Callback opcional após a fala
            });
        }
    }

    public override void DidFinishSpeechUtterance(AVFoundation.AVSpeechSynthesizer synthesizer, AVFoundation.AVSpeechUtterance utterance)
    {
        //Console.WriteLine("Did Finish Speech Utterance");
        try
        {
            var utteranceId = utteranceToIdMap[utterance];
            if (callbacks.TryGetValue(utteranceId, out var callback))
            {
                callback.Invoke();
                callbacks.Remove(utteranceId);
                utteranceToIdMap.Remove(utterance);
            }
            SpeakNextInQueue(); // Fala o próximo item na fila
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in DidFinishSpeechUtterance: {ex.Message}");
        }
    }


    } // end class DVSpeech
} // end namespace Digitavos.PlatformsImplementarions