using Level_Data;
using Level_Data.Models;
using Level_Data.Models.Interfaces;
using Level_Data.StrategyPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Level_Data.Models.Interfaces
{
    public interface IActor
    {
        public int StartRoomId { get; set; }
        public int CurrentX { get; set; }
        public int CurrentY { get; set; }
        public int Lives { get; set; }
        public List<Iitem> Items { get; set; }
        public Game DecidePlayerAction(Position Pos, Game Game);
    }
}
