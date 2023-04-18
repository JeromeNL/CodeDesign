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
    public interface IRoom
    {
        public int Id { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string type { get; set; }
        public List<Iitem> Items { get; set; }
        public Dictionary<Position, ITile> Tiles { get; set; }
        public bool toggle { get; set; }
    }
}
