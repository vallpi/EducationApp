using App.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor
{
    public class EditorClass
    {
        public static EditorClass edcl = new EditorClass();
        public Subject SelectedSubject;
        public Topic SelectedTopic;

        public EditorClass()
        {
            SelectedSubject = new Subject();
            SelectedTopic = new Topic();
        }
    }
}
