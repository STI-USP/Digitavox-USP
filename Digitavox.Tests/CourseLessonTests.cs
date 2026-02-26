using Digitavox.Models;
using System.Net.Http.Headers;

namespace Digitavox.Tests
{
    public class CourseLessonTests
    {
        [Fact]
        public void Test1()
        {
            CourseLesson courseLesson = new CourseLesson();
            courseLesson.setLessonChars("asdfg", 2);
            courseLesson.startTimer();
            courseLesson.charPressed("a");
            Thread.Sleep(1000);
            courseLesson.charPressed("s");
            Thread.Sleep(1000);
            courseLesson.charPressed("f");
            courseLesson.charPressed("f");
            courseLesson.charPressed("g");
            courseLesson.charPressed(" ");
            courseLesson.charPressed("a");
            courseLesson.charPressed("s");
            courseLesson.charPressed("f");
            courseLesson.charPressed("f");
            courseLesson.charPressed("g");
            courseLesson.stopTimer();
            Assert.Equal(2, courseLesson.getStatistics().practiceTime);
            Assert.Equal(9, courseLesson.getStatistics().correctChars);
            Assert.Equal(2, courseLesson.getStatistics().incorrectChars);
            Assert.Equal(2, courseLesson.getStatistics().errorsByChar["d"]);
            Assert.Equal(12, courseLesson.totalChars());
            Assert.Equal(11, courseLesson.pressedChars());
            Assert.Equal(75, courseLesson.correctPercent());
            Assert.Equal(330, courseLesson.charsByMinute());
        }
    }
}
