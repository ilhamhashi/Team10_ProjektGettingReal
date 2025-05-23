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
        IEnumerable<Note> GetAll();
        Note GetNote(int number);
        void Add(Note note);
        void Update(Note note);
        void Delete(Note note);
       

    }
}
