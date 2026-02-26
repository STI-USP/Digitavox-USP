using Digitavox.Helpers;
using System.Text.Json;

namespace Digitavox.Models
{
    public class Course
    {
        JsonElement fileRoot;
        JsonElement courseRoot;
        JsonElement lessonRoot;
        List<JsonElement> jsonList = new List<JsonElement>();
        List<string> courseIdList = new List<string>();
        List<string> courseNameList = new List<string>();
        private string lessonId;
        private string courseId;
        private int courseNumber;
        public void GetCoursesLists()
        {
            if (jsonList.Count == 0)
            {
                var temp = DVPersistence.ReadCourseFiles();
                jsonList = temp.Item1;
                courseIdList = temp.Item2;
                courseNameList = temp.Item3;
            }
        }
        public void SelectCourse(int fileNumber)
        {
            fileRoot = jsonList[fileNumber];
            courseRoot = fileRoot.GetProperty("CURSO");
            courseId = courseIdList[fileNumber];
            courseNumber = fileNumber + 1;
        }
        public void SelectLesson(int lessonNumber)
        {
            lessonId = $"LICAO{lessonNumber}";
            lessonRoot = fileRoot.GetProperty(lessonId);
        }
        public string CourseApresentation()
        {
            return courseRoot.GetProperty("Present").GetProperty("APT").ToString();
        }
        public string CourseInstruction()
        {
            return courseRoot.GetProperty("Instruction").GetProperty("IST").ToString();
        }
        public int TotalLessons()
        {
            return int.Parse(courseRoot.GetProperty("QUANTIDADELICOES").ToString());
        }
        public string CourseProperty(string property)
        {
            return courseRoot.GetProperty(property).ToString();
        }
        public int CourseNumber()
        {
            return courseNumber;
        }
        public string LessonId()
        {
            return lessonId;
        }
        public string LessonApresentation()
        {
            return lessonRoot.GetProperty("Present").GetProperty("APT").ToString();
        }
        public string LessonInstruction()
        {
            return lessonRoot.GetProperty("Instruction").GetProperty("IST").ToString();
        }
        public string LessonProperty(string property)
        {
            return lessonRoot.GetProperty(property).ToString();
        }
        public int LessonNumber()
        {
            return int.Parse(lessonId.Substring(5));
        }
        public string CourseId()
        {
            return courseId.Replace(".json", string.Empty);
        }
        public List<string> CourseNameList()
        {
            return courseNameList;
        }
        public List<string> GetExercises()
        {
            List<string> exercisesList = new List<string>();
            var exercises = lessonRoot.EnumerateObject()
                              .Where(it => it.Name.StartsWith("EXER"));
            foreach (var exercise in exercises)
            {
                exercisesList.Add(exercise.Value.ToString());
            }
            return exercisesList;
        }
    }
}
