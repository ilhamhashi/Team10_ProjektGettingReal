using DATApp.MVVM.Model.Classes;

namespace DATApp.MVVM.Model.Repositories
{
    public interface IMatchRepository
    {
        IEnumerable<Match> GetAll();
        Match GetMatch(int number);
        void AddMatch(Match match);
        void UpdateMatch(Match match);
        void DeleteMatch(Match match);
    }
}
