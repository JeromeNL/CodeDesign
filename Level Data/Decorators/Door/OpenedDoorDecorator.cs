using Level_Data;
using Level_Data.Decorators.Door;
using Level_Data.Models;
using Level_Data.Models.Interfaces;
using Level_Data.StrategyPattern;

namespace Level_Data.Decorators.Door
{
    public class OpenedDoorDecorator : DoorDecorator, ITile
    {
        public bool Toggle { get; set; }
        public OpenedDoorDecorator(IDoor Door) : base(Door)
        {
            Toggle = true;
        }


        public override bool Open(Game Game)
        {
            return true;
        }


        public bool Interact(Game Game)
        {
            return Open(Game);
        }


        public char Symbol(Game Game, Position Position)
        {
            return ' ';
        }


        public ConsoleColor Color()
        {
            return Door.Color().Equals("white") ? ConsoleColor.White : Door.Color();
        }
    }
}

