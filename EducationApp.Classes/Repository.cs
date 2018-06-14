using App.Classes.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using App.Classes.Main_Classes;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace App.Classes
{
    internal class Repository : IRepository
    {

        public class Data
        {
            public List<Subject> Subjects { get; set; }
            public List<User> Users { get; set; }
        }

        private static Context ctx = new Context();
        private int score = 0;
        private Topic SelectedTopic;
        private int question_Number = 1;
        private Data _data = new Data();
        private User _authorizedUser;
        private Subject SelectedSubject;
        private const string DataFolder = "../App.Classes/Data";
        private const string FileName = "Info.json";
        // Кодировка пароля
        private static string GetHash(string password)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hash);
        }

        public void interruptTest()
        {
            score = 0;
            question_Number = 1;
            return;
        }

        // Авторизация
        public bool Authorization(string login, string password)
        {
            var user = _data.Users.FirstOrDefault(u => u.Login == login && u.Password == GetHash(password));
            if (user != null)
            {
                _authorizedUser = user;
                return true;
            }
            return false;
        }
        // Получение информации о пользователе
        public string GetUserData(string requiredData)
        {
            string UserData = null;
            switch(requiredData)
            {
                case "FullName":
                    UserData = _authorizedUser.FullName;
                    break;
                case "Email":
                    UserData = _authorizedUser.Email;
                    break;
                case "Password":
                    UserData = _authorizedUser.Password;
                    break;
                case "Login":
                    UserData = _authorizedUser.Login;
                    break;
            }
            return UserData;
        }

        public void CheckAnswer(string answer, int questionType)
        {
            if (questionType == 1)
            { 
                if (answer == SelectedTopic.ChooseAnswerQuestions.Where(n => n.QuestionNumber == question_Number-1).Select(k => k.CorrectAnswer).FirstOrDefault())
                {
                    score++;
                }
            }
            else
            {
                if (answer == SelectedTopic.WriteAnswerQuestions.Where(n => n.QuestionNumber == question_Number-1).Select(k => k.CorrectAnswer).FirstOrDefault())
                {
                    score++;
                }
            }
            return;
        }
        public List<Theory> GetTopicTheory(string topic_Name)
        {
            SelectedTopic = SelectedSubject.Topics.Where(k => k.Name == topic_Name).First();
            var theoryText = SelectedSubject.Topics.Where(n => n.Name == topic_Name).Select(k => k.TheoryText).First();
            return theoryText;
        }

        public string GetSelectedSubjectString()
        {
            return SelectedSubject.Name;
        }

        // Получение информации о предметах
        public string GetSubject(int Id)
        {
            var Subject = _data.Subjects.Where(n => n.Id == Id).Select(n => n.Name).First();
            return Subject;
        }

        public List<Subject> GetSubjectList()
        {
            return _data.Subjects;
        }

        //Сохранение выбранного предмета для перехода по гиперссылке
        public void SelectSubject(int Id)
        {
            SelectedSubject = _data.Subjects.FirstOrDefault(n => n.Id == Id);
            return;
        }
        //Получение выбранного предмета
        public List<string> ReturnSubjectTopics()
        {
            var Topics = SelectedSubject.Topics;
            List<string> TopicNames = new List<string>();
            foreach (var topic in Topics)
            {
                TopicNames.Add(topic.Name);
            }
            return TopicNames;
        }
        //Регистрация
        public bool Registration(string fullname, string email, string login, string password)
        {
            var id = _data.Users.Count > 0 ? _data.Users.Max(u => u.Id) + 1 : 1;
            if (!_data.Users.Any(n => n.Email == email))
            {
                ctx.Users.AddOrUpdate(new User { FullName = fullname, Email = email, Login = login, Password = GetHash(password), Id = id, Hash = true });
                ctx.SaveChanges();
                User user = ctx.Users.Where(n => n.Email == email).FirstOrDefault();
                _data.Users.Add(user);
                Save("Users");
                _authorizedUser = user;
                return true;
            }
            return false;
        }
        //Сохранение Data в файл
        private void Save(string type)
        {
            if (!Directory.Exists(DataFolder))
            {
                Directory.CreateDirectory(DataFolder);
            }
            using (var sw = new StreamWriter(GetPath(type)))
            {
                using (var jsonWriter = new JsonTextWriter(sw))
                {
                    var serializer = new JsonSerializer();
                    switch (type)
                    {
                        case "Users":
                            {
                                serializer.Serialize(jsonWriter, ctx.Users.ToList());
                                break;
                            }
                        case "TestResults":
                            {
                                var testres = ctx.TestResults.ToList();
                                serializer.Serialize(jsonWriter, testres);
                                break;
                            }
                    }
                }
            }
        }
        public IEnumerable<ScoreItem> GetScore()
        {
            List<ScoreItem> Score = new List<ScoreItem>();
            for(int i = 0; i < _authorizedUser.TestResults.Count; i++)
            {
                for(int k = i+1; k < _authorizedUser.TestResults.Count; k++)
                {
                    if((_authorizedUser.TestResults[i].SubjectId == _authorizedUser.TestResults[k].SubjectId) && (_authorizedUser.TestResults[i].TopicId == _authorizedUser.TestResults[k].TopicId))
                    {
                        for(int l = k+1; l<_authorizedUser.TestResults.Count; l++)
                        {
                            _authorizedUser.TestResults[l - 1] = _authorizedUser.TestResults[l];
                        }
                        if (k == _authorizedUser.TestResults.Count-1)
                            _authorizedUser.TestResults.RemoveAt(k);
                    }
                }
            }
            if ((_authorizedUser.TestResults != null) && (_authorizedUser.TestResults.Count != 0))
            {
                foreach (var testres in _authorizedUser.TestResults)
                {
                    List<Topic> topics = _data.Subjects.Where(n => n.Id == testres.SubjectId).Select(j => j.Topics).First();
                    var AllChooseQuestions = topics.Where(n => n.Id == testres.TopicId).Select(l => l.ChooseAnswerQuestions).First();
                    var AllWriteQuestions = topics.Where(n => n.Id == testres.TopicId).Select(l => l.WriteAnswerQuestions).First();
                    Score.Add(new ScoreItem { TopicName = topics.Where(n => n.Id == testres.TopicId).Select(l => l.Name).First(), score = testres.Score, MaximumScore = AllChooseQuestions.Count + AllWriteQuestions.Count });
                }
            }
            return Score;
        }
        public void GetTestResult()
        {
            if (_authorizedUser.TestResults != null)
            {
                if (!_authorizedUser.TestResults.Any(n => n.TopicId == SelectedTopic.Id))
                {
                    _authorizedUser.TestResults.Add(new TestResult { Id = _authorizedUser.TestResults.Count, SubjectId = SelectedSubject.Id, UserId = _authorizedUser.Id, Score = score, TopicId = SelectedTopic.Id });
                    ctx.TestResults.Add(new TestResult { Id = _authorizedUser.TestResults.Count, UserId = _authorizedUser.Id, SubjectId = SelectedSubject.Id, Score = score, TopicId = SelectedTopic.Id });
                    ctx.SaveChanges();
                }
                else
                {
                    var e = _authorizedUser.TestResults.Where(s => s.TopicId == SelectedTopic.Id).First();
                    e.Score = score;
                    var testres = ctx.TestResults.Where(t => t.UserId == _authorizedUser.Id).Where(k => k.TopicId == SelectedTopic.Id).First();
                    ctx.TestResults.AddOrUpdate(u => u.TopicId, testres);
                    ctx.SaveChanges();
                }
            }
            else
            {
                _authorizedUser.TestResults = new List<TestResult>();
                _authorizedUser.TestResults.Add(new TestResult { Id = _authorizedUser.TestResults.Count, UserId = _authorizedUser.Id, SubjectId = SelectedSubject.Id, Score = score, TopicId = SelectedTopic.Id });
                ctx.TestResults.Add(new TestResult { Id = _authorizedUser.TestResults.Count, UserId = _authorizedUser.Id, SubjectId = SelectedSubject.Id, Score = score, TopicId = SelectedTopic.Id });
                ctx.SaveChanges();
            }
            Save("TestResults");
            score = 0;
            question_Number = 1;
            return;
        }

        public QuestionModel1 ReturnQuestionModel1()
        {
            var question = SelectedTopic.ChooseAnswerQuestions.FirstOrDefault(k => k.QuestionNumber == question_Number);
            question_Number++;
            return question;
        }
        
        public QuestionModel2 ReturnQuestionModel2()
        {
            var question = SelectedTopic.WriteAnswerQuestions.FirstOrDefault(k => k.QuestionNumber == question_Number);
            question_Number++;
            return question;
        }

        public string GetNextQuestion()
        {
            if (SelectedTopic.ChooseAnswerQuestions.Any(n => n.QuestionNumber == question_Number))
                return "TestQuestion1.xaml";
            if (SelectedTopic.WriteAnswerQuestions.Any(n => n.QuestionNumber == question_Number))
                return "TestQuestion2.xaml";
            return null;
        }

        public int ShowResult()
        {
            return score;
        }

        private string GetPath(string subject) => Path.Combine(DataFolder, subject  + ".json");
        // Прочитка файла
        public Repository()
        {

            _data.Subjects = ctx.Subjects.ToList();
            var topics = ctx.Topics.ToList();
            foreach (Topic t in topics)
            {
                t.ChooseAnswerQuestions = ctx.Questions1.Where(n => n.TopicId == t.Id).ToList();
                t.WriteAnswerQuestions = ctx.Questions2.Where(n => n.TopicId == t.Id).ToList();
                t.TheoryText = ctx.Theories.Where(n => n.TopicId == t.Id).ToList();
            }
            foreach (Subject s in _data.Subjects)
                s.Topics = topics.Where(n => n.SubjectId == s.Id).ToList();

           _data.Users = ctx.Users.ToList();
            foreach (User u in _data.Users)
                u.TestResults = ctx.TestResults.Where(n => n.UserId == u.Id).ToList();

        }

        /*public JoJo()
        {
            try
            {
                using (var sr = new StreamReader(Path.Combine(DataFolder, FileName)))
                {
                    using (var jsonReader = new JsonTextReader(sr))
                    {
                        var serializer = new JsonSerializer();
                        _data = serializer.Deserialize<Data>(jsonReader);
                    }
                }
            }
            catch
            {
                _data = new Data
                {
                    Users = new List<User>(),
                    Subjects = new List<Subject>()
                };
            }
            foreach (var user in _data.Users)
            {
                if (user.Subjects == null)
                    user.Subjects = new List<Subject>();
                if (user.Hash == false)
                {
                    user.Password = GetHash(user.Password);
                    user.Hash = true;
                }
            }
            foreach (var subject in _data.Subjects)
            {
                try
                {
                    using (var sr = new StreamReader(GetPath(subject.Name)))
                    {
                        using (var jsonReader = new JsonTextReader(sr))
                        {
                            var serializer = new JsonSerializer();
                            subject.Topics = serializer.Deserialize<List<Topic>>(jsonReader);
                            foreach (var topic in subject.Topics)
                            {
                                if (topic.WriteAnswerQuestions == null)
                                    topic.WriteAnswerQuestions = new List<QuestionModel2>();
                                if (topic.ChooseAnswerQuestions == null)
                                    topic.ChooseAnswerQuestions = new List<QuestionModel1>();
                                foreach (var chooseans in topic.ChooseAnswerQuestions)
                                {
                                    chooseans.Answers = new string[4] { chooseans.Answer1, chooseans.Answer2, chooseans.Answer3, chooseans.Answer4 };
                                }
                                if (topic.Theory != null)
                                {
                                    string line = topic.Theory;
                                    string[] parts = line.Split(';');
                                    topic.TheoryText = new List<string>();
                                    for (int i = 0; i < parts.Length; i++)
                                    {
                                        if (parts[i].Contains(';'))
                                            parts[i] = parts[i].Remove(parts[i].Length);
                                        topic.TheoryText.Add(parts[i]);
                                    }
                                }
                                else
                                {
                                    topic.TheoryText = new List<string>();
                                }
                            }
                        }
                    }
                }
                catch
                {
                    subject.Topics = new List<Topic>();
                }
            }
        } */

    }
}
