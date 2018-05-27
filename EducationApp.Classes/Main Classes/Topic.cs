using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Classes
{
    public class Topic
    {
        public string Name { get; set; }
        public List<QuestionModel1> ChooseAnswerQuestions { get; set; }
        public List<QuestionModel2> WriteAnswerQuestions { get; set; }
    }
}
