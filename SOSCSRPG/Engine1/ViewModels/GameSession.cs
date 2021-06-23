using Engine.Models;

namespace Engine.ViewModel
{
    public class GameSession
    {
        public Player CurrentPlayer { get; set; }

        public GameSession()
        {
            CurrentPlayer = new Player();
            CurrentPlayer.Name = "Ivar Wolfsbane";
            CurrentPlayer.Gold = 1000000;
        }
    }
}
