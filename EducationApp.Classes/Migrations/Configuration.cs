namespace App.Classes.Migrations
{
    using App.Classes.Main_Classes;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    internal sealed class Configuration : DbMigrationsConfiguration<App.Classes.Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(App.Classes.Context context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            var json = new JsonSerializer();
            string resourceName = "App.Classes.Data.Subjects.json";
            var assembly = Assembly.GetExecutingAssembly();
            var stream = assembly.GetManifestResourceStream(resourceName);

            using (var sr = new StreamReader(stream))
            {
                using (var jsonReader = new JsonTextReader(sr))
                {
                    var subjects = json.Deserialize<List<Subject>>(jsonReader).ToArray();
                    context.Subjects.AddOrUpdate(u => u.Name, subjects);
                }
            }

            resourceName = "App.Classes.Data.Questions1.json";
            stream = assembly.GetManifestResourceStream(resourceName);
            using (var sr = new StreamReader(stream))
            {
                using (var jsonReader = new JsonTextReader(sr))
                {
                    var questions1 = json.Deserialize<List<QuestionModel1>>(jsonReader).ToArray();
                    context.Questions1.AddOrUpdate(u => u.Question, questions1);
                }
            }

            resourceName = "App.Classes.Data.Questions2.json";
            stream = assembly.GetManifestResourceStream(resourceName);
            using (var sr = new StreamReader(stream))
            {
                using (var jsonReader = new JsonTextReader(sr))
                {
                    var questions2 = json.Deserialize<List<QuestionModel2>>(jsonReader).ToArray();
                    context.Questions2.AddOrUpdate(u => u.Question, questions2);
                }
            }

            resourceName = "App.Classes.Data.Topics.json";
            stream = assembly.GetManifestResourceStream(resourceName);
            using (var sr = new StreamReader(stream))
            {
                using (var jsonReader = new JsonTextReader(sr))
                {
                    var topics = json.Deserialize<List<Topic>>(jsonReader).ToArray();
                    context.Topics.AddOrUpdate(u => u.Name, topics);
                }
            }
            
            resourceName = "App.Classes.Data.Theories.json";
            stream = assembly.GetManifestResourceStream(resourceName);
            using (var sr = new StreamReader(stream))
            {
                using (var jsonReader = new JsonTextReader(sr))
                {
                    var theories = json.Deserialize<List<Theory>>(jsonReader).ToArray();
                    context.Theories.AddOrUpdate();
                }
            } 

            resourceName = "App.Classes.Data.Users.json";
            stream = assembly.GetManifestResourceStream(resourceName);
            using (var sr = new StreamReader(stream))
            {
                using (var jsonReader = new JsonTextReader(sr))
                {
                    var users = json.Deserialize<List<User>>(jsonReader).ToArray();
                    context.Users.AddOrUpdate(u => u.Email, users);
                }
            }
            
            resourceName = "App.Classes.Data.TestResults.json";
            stream = assembly.GetManifestResourceStream(resourceName);
            using (var sr = new StreamReader(stream))
            {
                using (var jsonReader = new JsonTextReader(sr))
                {
                    var testresults = json.Deserialize<List<TestResult>>(jsonReader).ToArray();
                    context.TestResults.AddOrUpdate(testresults);
                }
            } 
        }
    }
}

