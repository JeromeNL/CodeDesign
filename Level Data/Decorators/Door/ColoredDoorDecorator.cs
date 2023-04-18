using Level_Data;
using Level_Data.Decorators.Door;
using Level_Data.Models;
using Level_Data.Models.Interfaces;
using Level_Data.StrategyPattern;

namespace Level_Data.Decorators.Door
{
    public class ColoredDoorDecorator : DoorDecorator, ITile
    {
        public ConsoleColor DoorColor { get; set; }

        public ColoredDoorDecorator(IDoor Door, string Color) : base(Door)
        {
            if (Color == "red") DoorColor = ConsoleColor.Red;

            if (Color == "green") DoorColor = ConsoleColor.Green;
        }


        public override bool Open(Game Game)
        {
            if (Door.Open(Game) && Game.Player.Items.FindAll(x => x.Type.Equals("key") && x.ItemColor.Equals(DoorColor)).Count() > 0)
            {
                return true;
            }
            return false;
        }


        public bool Interact(Game Game)
        {
            return Open(Game);
        }


        public char Symbol(Game Game, Position Position)
        {
            return 'I';
        }


        public ConsoleColor Color()
        {
            return DoorColor;
        }
    }
}


