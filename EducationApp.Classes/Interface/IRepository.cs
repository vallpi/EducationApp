﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Classes.Interface
{
    public interface IRepository
    {
        bool Authorization(string login, string password);
        bool Registration(string fullname, string email, string login, string password);
        string GetUserData(string requiredData);
        string GetSubject(int Id);
        List<string> ReturnSubjectTopics();
        void SelectSubject(int Id);
        List<string> GetTopicTheory(string topic_Name);
        int GetQuestionType();
        QuestionModel2 ReturnQuestionModel2();
        QuestionModel1 ReturnQuestionModel1();
        void CheckAnswer(string answer, int questionType);
        void interruptTest();
    }
}
