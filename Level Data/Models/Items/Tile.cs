using Level_Data;
using Level_Data.Models;
using Level_Data.Models.Interfaces;
using Level_Data.Models.Items;
using Level_Data.StrategyPattern;

namespace Level_Data.Models.Items
{
    public class Tile : Iitem, ITile
    {
        public string Type { get; set; }
        public ConsoleColor? ItemColor { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int? Damage { get; set; }

        public Tile(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }


        public void Use(Game game)
        {
        }


        public bool Interact(Game game)
        {
            Use(game);
            return true;
        }


        public char Symbol(Game game, Position position)
        {
            return ' ';
        }


        public ConsoleColor Color()
        {
            return ConsoleColor.White;
        }
    }
}
