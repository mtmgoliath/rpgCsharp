using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Engine.Models
{
    public class GameDetails
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Version { get; set; }

        public ObservableCollection<PlayerAttribute> PlayerAttributes { get; set; } =
            new ObservableCollection<PlayerAttribute>();
        public List<Race> Races { get; } =
            new List<Race>();

        public GameDetails(string title, string subTitle, string version)
        {
            Title = title;
            SubTitle = subTitle;
            Version = version;
        }
    }
}