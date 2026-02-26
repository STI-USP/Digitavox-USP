using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digitavox.Models
{
    public class CourseLesson
    {
        DateTime startTime;
        DateTime stopTime;
        private string lessonChars;
        private int nextIndexToType;
        private CourseLessonBean bean = new CourseLessonBean();
        public void setLessonChars(string lessonChars, int exerciseRepetitions)
        {
            this.lessonChars = lessonChars;
            bean.exerciseRepetitions = exerciseRepetitions;
            if (bean.exerciseRepetitions > 1)
            {
                this.lessonChars += " ";
            }
        }
        public void startTimer()
        {
            startTime = DateTime.Now;
            nextIndexToType = 0;
            bean.errorsByChar = new Dictionary<string, int>();
        }
        public void charPressed(string c)
        {
            string correct = lessonChars.ToCharArray()[nextIndexToType].ToString();
            if (correct.Equals(c))
            {
                bean.correctChars += 1;
            }
            else
            {
                bean.incorrectChars += 1;
                if (!bean.errorsByChar.ContainsKey(correct))
                {
                    bean.errorsByChar.Add(correct, 0);
                }
                bean.errorsByChar[correct] += 1;
            }
            nextIndexToType = (nextIndexToType + 1) % lessonChars.Length;
        }
        public void stopTimer()
        {
            stopTime = DateTime.Now;
            bean.practiceTime = (int)(stopTime - startTime).TotalSeconds;
        }
        public CourseLessonBean getStatistics()
        {
            return bean;
        }
        public int totalChars()
        {
            return lessonChars.Length * bean.exerciseRepetitions;
        }
        public int pressedChars()
        {
            return bean.correctChars + bean.incorrectChars;
        }
        public int correctPercent()
        {
            return bean.correctChars * 100 / totalChars();
        }
        public int charsByMinute()
        {
            return (bean.correctChars + bean.incorrectChars) * 60 / bean.practiceTime;
        }
    }
}
