using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digitavox.PlatformsImplementations
{
    public partial class DVSpeak
    {
        private static readonly Lazy<DVSpeak> instance = new Lazy<DVSpeak>(() => new DVSpeak());
        public partial void Cancel();
        public partial void SpeakText(string text, Action onFinished);
        public partial void Init(object obj);
        public partial void InitCompleted();
        public partial void SetSpeechRate(int speechRate);
        public partial void Completed(object id);
        public static DVSpeak GetInstance()
        {
            return instance.Value;
        }
        private DVSpeak()
        {
        }
    }
}
