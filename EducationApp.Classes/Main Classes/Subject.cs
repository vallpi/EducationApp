using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Classes
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Topic> Topics { get; set; }
    }
}
