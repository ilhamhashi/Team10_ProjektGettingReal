using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATApp.MVVM.Model.Classes;

namespace DATApp.MVVM.Model.Repositories
{
    public interface INoteRepository
    {
        Note GetByID(int id);
        IEnumerable<Note> GetAll();
        void Add(Note note);
        void Update(Note note);
        void Delete(Note note);
       

    }
}
