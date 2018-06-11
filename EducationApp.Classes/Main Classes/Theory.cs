using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Classes.Main_Classes
{
    public class Theory
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public int? TopicId { get; set; }
        [JsonIgnore]
        public virtual Topic Topic { get; set; }
    }
}
