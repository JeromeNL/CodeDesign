using Level_Data;
using Level_Data.Decorators.Door;
using Level_Data.Models;
using Level_Data.Models.Interfaces;
using Level_Data.StrategyPattern;

namespace Level_Data.Decorators.Door
{
    public abstract class DoorDecorator : IDoor
    {
        protected IDoor Door;

        public DoorDecorator(IDoor Door)
        {
            this.Door = Door;
        }


        public bool Interact(Game Game)
        {
            throw new NotImplementedException();
        }


        public virtual bool Open(Game Game)
        {
            return Door.Open(Game);
        }


        public char Symbol(Game Game, Position Position)
        {
            throw new NotImplementedException();
        }


        public ConsoleColor Color()
        {
            return ConsoleColor.White;
        }
    }
}

