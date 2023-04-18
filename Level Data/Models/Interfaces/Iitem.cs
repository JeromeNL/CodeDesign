
using Level_Data;
using Level_Data.Models;
using Level_Data.Models.Interfaces;
using Level_Data.StrategyPattern;

namespace Level_Data.Models.Interfaces
{
    public interface Iitem : ITile
    {
        public string Type { get; set; }
        public ConsoleColor? ItemColor { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int? Damage { get; set; }
        public void Use(Game game);
    }
}


