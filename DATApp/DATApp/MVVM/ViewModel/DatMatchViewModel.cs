using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATApp.MVVM.Model.Classes;
using DATApp.MVVM.Model.Repositories;

namespace DATApp.MVVM.ViewModel
{
    class DatMatchViewModel
    {
        public ObservableCollection<EmotionalState> emotions { get; }

        public DatMatchViewModel()
        {
            emotions = new ObservableCollection<EmotionalState>();
        }
    }
}
