using App.Classes.Main_Classes;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Classes
{
    public class Context : DbContext
    {
        public DbSet<QuestionModel1> Questions1 { get; set; }
        public DbSet<QuestionModel2> Questions2 { get; set; }
        public DbSet<Theory> Theories { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Subject> Subjects { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<TestResult> TestResults { get; set; }

        public Context() : base("localsql")
        {

        }
    }
}
