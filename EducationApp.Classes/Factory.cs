using App.Classes.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Classes
{
    // Получаем репозиторий
    public class Factory
    {
        private static Factory _instance;

        public static Factory Instance => _instance ?? (_instance = new Factory());

        private IRepository _repo;

        public IRepository GetRepository() => _repo ?? (_repo = new Repository());
    }
}
