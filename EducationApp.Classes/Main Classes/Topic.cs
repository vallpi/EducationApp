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
    public class Topic
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }


        public int SubjectId { get; set; }
        [JsonIgnore]
        public Subject Subject { get; set; }
        [JsonIgnore]
        public List<QuestionModel1> ChooseAnswerQuestions { get; set; }
        [JsonIgnore]
        public List<QuestionModel2> WriteAnswerQuestions { get; set; }
        [JsonIgnore]
        public List<Theory> TheoryText { get; set; }
        public Topic()
        {
            ChooseAnswerQuestions = new List<QuestionModel1>();
            WriteAnswerQuestions = new List<QuestionModel2>();
            TheoryText = new List<Theory>();
        }

        
    }
}
