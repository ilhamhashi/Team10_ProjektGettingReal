using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DATApp.Core;
using DATApp.MVVM.Model.Classes;
using SearchResult = DATApp.MVVM.Model.Classes.SearchResult;

namespace DATApp.MVVM.ViewModel
{
    public class SkillsViewModel : INotifyPropertyChanged 
    {
        private string _searchTerm;
        private ObservableCollection<SearchResult> _searchResults;
        private SkillSearcher _skillSearcher;


        public SkillsViewModel()
        {
            _searchResults = new ObservableCollection<SearchResult>();
            _skillSearcher = new SkillSearcher();

            //tilføj nogle dummy-'skill' til objekter til 'skillSearcher
            var skills = new List<Skill>

            {
                new Skill { SkillNumber = 1, Name = "Angst", Description = "X" },
                new Skill { SkillNumber = 2, Name = "Vrede", Description = "X" },
                new Skill { SkillNumber = 3, Name = "Stress", Description = "X" },
                new Skill { SkillNumber = 4, Name = "Depression", Description = "X" }
            };

            _skillSearcher.SetSkills(skills);

            //definér søgekommandoen

            SearchCommand = new RelayCommand(Search);
        }

        public string SearchTerm
        {
            get => _searchTerm;
            set
            {
                if (_searchTerm != value)
                {
                    _searchTerm = value;
                    OnPropertyChanged(nameof(SearchTerm)); //opdaterer til UI'et
                }
            }
        }
        public ObservableCollection<SearchResult> SearchResults
        { get =>  _searchResults; 
            set
            { if (_searchResults != value) 
                {
                _searchResults = value;
                    OnPropertyChanged(nameof(SearchResults));
                }

            }

        }
        public ICommand SearchCommand { get; set; }
        //Søgningens logik

        private void Search()
        {
            var results = _skillSearcher.Search(SearchTerm);

            SearchResults.Clear();
            foreach (var result in results)
            {
                SearchResults.Add(result);
            }
        }
            //Implementering af INotifyPropertyChanged

            public event PropertyChangedEventHandler PropertyChanged;
         
           protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        }
    }

