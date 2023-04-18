
using Level_Data;
using Level_Data.Models;
using Level_Data.Models.Interfaces;
using Level_Data.Models.Items;
using Level_Data.StrategyPattern;

namespace Level_Data.Models.Items
{
    public class SankaraStoneItem : Iitem, ITile
    {
        public string Type { get; set; }
        public ConsoleColor? ItemColor { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int? Damage { get; set; }


        public SankaraStoneItem(string Type, ConsoleColor? setColor, int setX, int Y, int? Damage)
        {
            this.Type = Type;
            ItemColor = setColor;
            X = setX;
            this.Y = Y;
            this.Damage = Damage;
        }


        public void Use(Game Game)
        {
            Game.Player.Items.Add(new SankaraStoneItem(Type, ItemColor, X, Y, Damage));
            Game.Rooms.First(x => x.Id == Game.Player.StartRoomId).Items.RemoveAll(e => e.Type.Equals("sankara_stone") && e.X == X && e.Y == Y);
        }


        public bool Interact(Game Game)
        {
            Use(Game);
            return true;
        }


        public char Symbol(Game game, Position Position)
        {
            return 'S';
        }


        public ConsoleColor Color()
        {
            return ConsoleColor.White;
        }
    }
}
