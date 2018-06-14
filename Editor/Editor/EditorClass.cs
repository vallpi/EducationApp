using App.Classes;
using App.Classes.Main_Classes;
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
        public static EditorClass _edcl;
        public static EditorClass GetEditorClass() => _edcl ?? (_edcl = new EditorClass());

        public event Action AddClick;
        private static Context ctx = new Context();
        public Subject SelectedSubject;
        public Topic SelectedTopic;
        public QuestionModel1 SelectedQuestion1;
        public QuestionModel2 SelectedQuestion2;
        public Theory SelectedTheory;


        public List<Subject> GetSubjectList() => ctx.Subjects.ToList();
        public List<Topic> GetListTopics() => ctx.Topics.Where(u => u.SubjectId == SelectedSubject.Id).ToList();
        public List<QuestionModel1> GetListQuestions1 => ctx.Questions1.Where(u => u.TopicId == SelectedTopic.Id).ToList();
        public List<QuestionModel2> GetListQuestions2 => ctx.Questions2.Where(u => u.TopicId == SelectedTopic.Id).ToList();
        public List<Theory> GetListTheory => ctx.Theories.Where(u => u.TopicId == SelectedTopic.Id).ToList();


        public void AddButtonClick() => AddClick();
        public void DeleteButtonClick(string rbname)
        {
            switch (rbname)
            {
                case "None": DeleteSubject(); break;
                case "Topic": DeleteTopic(); break;
                case "Question1": DeleteQuestion1(); break;
                case "Question2": DeleteQuestion2(); break;
                case "Theory": DeleteTheory(); break;
            }
        }

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

        public void AddTheory(Theory newtheory)
        {
            ctx.Theories.AddOrUpdate(newtheory);
            ctx.SaveChanges();
            Update("../../../../EducationApp.Classes/Data", "Theories.json", ctx.Theories.ToList());
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

        public void DeleteSubject()
        {
            if (!SelectedSubject.Topics.Any())
            {
                ctx.Subjects.Remove(SelectedSubject);
                ctx.SaveChanges();
                Update("../../../../EducationApp.Classes/Data", "Subjects.json", ctx.Subjects.ToList());
            }
        }

        public void DeleteTopic()
        {
            if (!(SelectedTopic.ChooseAnswerQuestions.Any() || SelectedTopic.WriteAnswerQuestions.Any() || SelectedTopic.TheoryText.Any()))
            {
                foreach (TestResult tr in ctx.TestResults.Where(t => t.TopicId == SelectedTopic.Id))
                    ctx.TestResults.Remove(tr);
                ctx.Topics.Remove(SelectedTopic);
                ctx.SaveChanges();
                Update("../../../../EducationApp.Classes/Data", "Topics.json", ctx.Topics.ToList());
                Update("../../../../EducationApp.Classes/Data", "TestResults.json", ctx.TestResults.ToList());
            }
        }

        public void DeleteQuestion1()
        {
            ctx.Questions1.Remove(SelectedQuestion1);
            ctx.SaveChanges();
            Update("../../../../EducationApp.Classes/Data", "Questions1.json", ctx.Questions1.ToList());
        }

        public void DeleteQuestion2()
        {
            ctx.Questions2.Remove(SelectedQuestion2);
            ctx.SaveChanges();
            Update("../../../../EducationApp.Classes/Data", "Questions2.json", ctx.Questions2.ToList());
        }

        public void DeleteTheory()
        {
            ctx.Theories.Remove(SelectedTheory);
            ctx.SaveChanges();
            Update("../../../../EducationApp.Classes/Data", "Theories.json", ctx.Theories.ToList());
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
