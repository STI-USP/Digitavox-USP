using AVFoundation;
using Foundation;

namespace Digitavox.PlatformsImplementations {

  public partial class DVSpeak : AVSpeechSynthesizerDelegate {

    private AVSpeechSynthesizer synthesizer;
    private float iosSpeechRate;
    private Dictionary<AVSpeechUtterance, Action> callbacks = new();

    public partial void Cancel() {
      Console.WriteLine("Cancel Speech");
      synthesizer.StopSpeaking(AVSpeechBoundary.Immediate);
      float savedRate = NSUserDefaults.StandardUserDefaults.FloatForKey("speechRateKey");
      iosSpeechRate = savedRate != 0 ? savedRate : 0.5f;
    }

    public partial void Init(object obj) {
      Console.WriteLine("Init Speech");
      synthesizer = new AVSpeechSynthesizer();
      synthesizer.Delegate = this;
      synthesizer.UsesApplicationAudioSession = true; // [jo:231016]
    }

    public partial void InitCompleted() {
      Console.WriteLine("Init Completed");
    }

    public partial void SpeakText(string text, Action onFinished) {
      var utterance = new AVSpeechUtterance(text) {
        Voice = AVSpeechSynthesisVoice.FromLanguage("pt-BR"),
        Rate = iosSpeechRate // 0.5f
      };
      synthesizer.SpeakUtterance(utterance);
      callbacks[utterance] = onFinished;
    }

    public partial void SetSpeechRate(int speechRate) {
      Console.WriteLine("Set Speech Rate");
      iosSpeechRate = ((float)speechRate - 1) * 0.0625f + 0.5f; // ajustar 0.1f para velocidade
      NSUserDefaults.StandardUserDefaults.SetFloat(iosSpeechRate, "speechRateKey");
    }

    public partial void Completed(object id) {
      Console.WriteLine("Completed Speech");
    }

    public override void DidFinishSpeechUtterance(AVFoundation.AVSpeechSynthesizer synthesizer, AVFoundation.AVSpeechUtterance utterance) {
      Console.WriteLine("Did Finish Speech Utterance");
      try {
        callbacks[utterance].Invoke();
        callbacks.Remove(utterance);
      }
      catch { }
    }

  } // end class DVSpeech
} // end namespace Digitavos.PlatformsImplementarions