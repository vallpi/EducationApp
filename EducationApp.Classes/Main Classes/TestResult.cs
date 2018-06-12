using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Classes.Main_Classes
{
    public class TestResult
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Score { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TopicId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SubjectId { get; set; }
        public int? UserId { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }
    }
}
