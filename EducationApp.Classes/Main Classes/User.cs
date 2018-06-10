using App.Classes.Main_Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Classes
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool Hash { get; set; }

        [JsonIgnore]
        public List<TestResult> TestResults { get; set; }
        public User()
        {
            TestResults = new List<TestResult>();
        }
    }
}
