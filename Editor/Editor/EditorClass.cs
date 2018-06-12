using App.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor
{
    public class EditorClass
    {
        public event Action AddClick;
        public static EditorClass edcl = new EditorClass();
        private static Context ctx = new Context();
        public Subject SelectedSubject;
        public Topic SelectedTopic;

        public EditorClass()
        {
            SelectedSubject = new Subject();
            SelectedTopic = new Topic();
        }

        public List<Subject> GetSubjectList() => ctx.Subjects.ToList();

        public void AddQuestion1(QuestionModel1 newquestion)
        {
            ctx.Questions1.AddOrUpdate(q => q.Question, newquestion);
            ctx.SaveChanges();
            Update("../../../../EducationApp.Classes/Data", "Questions1.json", ctx.Questions1.ToList());
        }

        public void AddQuestion2(QuestionModel2 newquestion)
        {
            ctx.Questions2.AddOrUpdate(q => q.Question, newquestion);
            ctx.SaveChanges();
            Update("../../../../EducationApp.Classes/Data", "Questions2.json", ctx.Questions2.ToList());
        }

        public void AddSubject(Subject newsubject)
        {
            ctx.Subjects.AddOrUpdate(q => q.Name, newsubject);
            ctx.SaveChanges();
            Update("../../../../EducationApp.Classes/Data", "Subjects.json", ctx.Subjects.ToList());
        }

        public void AddTopic(Topic newtopic)
        {
            ctx.Topics.AddOrUpdate(q => q.Name, newtopic);
            ctx.SaveChanges();
            Update("../../../../EducationApp.Classes/Data", "Topics.json", ctx.Topics.ToList());
        }

        public void Update<T>(string DataFolder, string FileName, List<T> list) 
        {
            if (!Directory.Exists(DataFolder))
            {
                Directory.CreateDirectory(DataFolder);
            }
            using (var sw = new StreamWriter(Path.Combine(DataFolder, FileName)))
            {
                using (var jsonWriter = new JsonTextWriter(sw))
                {
                    var serializer = new JsonSerializer();
                    serializer.Serialize(jsonWriter, list);
                }
            }
        }

       /* public void UpdateAll()
        {
            using (var sw = new StreamWriter("../../../../EducationApp.Classes/Data/Questions1.json"))
            {
                using (var jsonWriter = new JsonTextWriter(sw))
                {
                    var serializer = new JsonSerializer();
                    serializer.Serialize(jsonWriter, ctx.Questions1.ToList());
                }
            }
            using (var sw = new StreamWriter("../../../../EducationApp.Classes/Data/Questions2.json"))
            {
                using (var jsonWriter = new JsonTextWriter(sw))
                {
                    var serializer = new JsonSerializer();
                    serializer.Serialize(jsonWriter, ctx.Questions2.ToList());
                }
            }
            using (var sw = new StreamWriter("../../../../EducationApp.Classes/Data/Subjects.json"))
            {
                using (var jsonWriter = new JsonTextWriter(sw))
                {
                    var serializer = new JsonSerializer();
                    serializer.Serialize(jsonWriter, ctx.Subjects.ToList());
                }
            }
            using (var sw = new StreamWriter("../../../../EducationApp.Classes/Data/TestResults.json"))
            {
                using (var jsonWriter = new JsonTextWriter(sw))
                {
                    var serializer = new JsonSerializer();
                    serializer.Serialize(jsonWriter, ctx.TestResults.ToList());
                }
            }
            using (var sw = new StreamWriter("../../../../EducationApp.Classes/Data/Theories.json"))
            {
                using (var jsonWriter = new JsonTextWriter(sw))
                {
                    var serializer = new JsonSerializer();
                    serializer.Serialize(jsonWriter, ctx.Theories.ToList());
                }
            }
            using (var sw = new StreamWriter("../../../../EducationApp.Classes/Data/Topics.json"))
            {
                using (var jsonWriter = new JsonTextWriter(sw))
                {
                    var serializer = new JsonSerializer();
                    serializer.Serialize(jsonWriter, ctx.Topics.ToList());
                }
            }
            using (var sw = new StreamWriter("../../../../EducationApp.Classes/Data/Users.json"))
            {
                using (var jsonWriter = new JsonTextWriter(sw))
                {
                    var serializer = new JsonSerializer();
                    serializer.Serialize(jsonWriter, ctx.Users.ToList());
                }
            }
        } */

    }
}
