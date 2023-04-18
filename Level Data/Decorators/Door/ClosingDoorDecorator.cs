using Level_Data;
using Level_Data.Decorators.Door;
using Level_Data.Models;
using Level_Data.Models.Interfaces;
using Level_Data.StrategyPattern;

namespace Level_Data.Decorators.Door
{
    public class ClosingDoorDecorator : DoorDecorator, ITile
    {
        public bool Toggle { get; set; }
        public int Passable { get; set; }

        public ClosingDoorDecorator(IDoor door) : base(door)
        {
            Toggle = true;
            Passable = 0;
        }


        public override bool Open(Game game)
        {
            if (Passable < 2)
            {
                Passable++;
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
            return 'n';
        }


        public ConsoleColor Color()
        {
            return Door.Color().Equals("white") ? ConsoleColor.White : Door.Color();
        }
    }
}

