using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digitavox.Models
{
    public record struct CourseLessonBean(bool finished, bool concluded, float correctPercent, int totalChars,
        int correctChars, int incorrectChars, int totalWords, int correctWords, int incorrectWords,
        int timeDivisor, int exerciseRepetitions, int totalTime, int practiceTime, Dictionary<string, int> errorsByChar);
}
