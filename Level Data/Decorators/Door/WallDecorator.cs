using Level_Data;
using Level_Data.Decorators.Door;
using Level_Data.Models;
using Level_Data.Models.Interfaces;
using Level_Data.StrategyPattern;

namespace Level_Data.Decorators.Door
{

    public class WallDecorator : DoorDecorator, ITile
    {

        public WallDecorator(IDoor Door) : base(Door)
        {
        }


        public override bool Open(Game Game)
        {
            return false;
        }


        public bool Interact(Game Game)
        {
            return Open(Game);
        }


        public char Symbol(Game Game, Position Position)
        {
            return '#';
        }


        public ConsoleColor Color()
        {
            return ConsoleColor.Yellow;
        }
    }
}

