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
                using (var sr = new StreamReader(GetPath(subject.Name)))
                {
                    using (var jsonReader = new JsonTextReader(sr))
                    {
                        var serializer = new JsonSerializer();
                        subject.Topics = serializer.Deserialize<List<Topic>>(jsonReader);
                    }
                }
            }

        }
    }
}
