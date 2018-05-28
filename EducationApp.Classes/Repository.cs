using App.Classes.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace App.Classes
{
    internal class Repository : IRepository
    {

        public class Data
        {
            public List<Subject> Subjects { get; set; }
            public List<User> Users { get; set; }
        }

        private Data _data;
        private User _authorizedUser;
        private Subject SelectedSubject;
        private const string DataFolder = "Data";
        private const string FileName = "Info.json";
        // Кодировка пароля
        private static string GetHash(string password)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hash);
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
        // Получение информации о предметах
        public string GetSubject(int Id)
        {
            var Subject = _data.Subjects.Where(n => n.Id == Id).Select(n => n.Name).First();
            return Subject;
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
                _data.Users.Add(new User { FullName = fullname, Email = email, Login = login, Password = GetHash(password), Id = id, Hash = true, Subjects = new List<Subject>() });
                Save();
                return true;
            }
            return false;
        }
        //Сохранение Data в файл
        private void Save()
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
                    serializer.Serialize(jsonWriter, _data);
                }
            }
        }

        private string GetPath(string subject) => Path.Combine(DataFolder, subject  + ".json");
        // Прочитка файла
        public Repository()
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
            foreach(var user in _data.Users)
            {
                if (user.Subjects == null)
                    user.Subjects = new List<Subject>();
                if (user.Hash == false)
                {
                    user.Password = GetHash(user.Password);
                    user.Hash = true;
                }
            }
            foreach(var subject in _data.Subjects)
            {
                try
                {
                    using (var sr = new StreamReader(GetPath(subject.Name)))
                    {
                        using (var jsonReader = new JsonTextReader(sr))
                        {
                            var serializer = new JsonSerializer();
                            subject.Topics = serializer.Deserialize<List<Topic>>(jsonReader);
                            foreach(var topic in subject.Topics)
                            {
                                if (topic.WriteAnswerQuestions == null)
                                    topic.WriteAnswerQuestions = new List<QuestionModel2>();
                                if (topic.ChooseAnswerQuestions == null)
                                    topic.ChooseAnswerQuestions = new List<QuestionModel1>();
                            }
                        }
                    }
                }
                catch
                {
                    subject.Topics = new List<Topic>();
                }
            }
        }
    }
}
