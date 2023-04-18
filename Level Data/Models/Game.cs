

using Level_Data.Models;
using Level_Data.Models.Interfaces;

namespace Level_Data.StrategyPattern
{
    public class Game
    {
        public IActor Player { get; set; }
        public List<Connection> Connections { get; set; }
        public List<IRoom> Rooms { get; set; }
        public bool Running { get; set; }


        public Game(IActor setPlayer, List<Connection> setConnections, List<IRoom> setRooms)
        {
            Player = setPlayer;
            Connections = setConnections;
            Rooms = setRooms;
            Running = true;
        }

     

        public bool HasDied()
        {
            int KillRate = 1;
            if (Player.Lives < KillRate)
           {
                Running = false;
                return true;
            }
            return false;
        }


        public bool HasWon()
        {
            if (Player.Items.Where(e => e.Type == "sankara_stone").Count() > 4)
            {
                Running = false;
                return true;
            }
            return false;
        }

    }
}


