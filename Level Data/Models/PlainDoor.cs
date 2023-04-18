using Level_Data;
using Level_Data.Models;
using Level_Data.Models.Interfaces;
using Level_Data.Models.Items;
using Level_Data.StrategyPattern;

namespace Level_Data.Models
{

    public class PlainDoor : IDoor
    {
        public PlainDoor()
        {
        }


        public bool Interact(Game Game)
        {
            throw new NotImplementedException();
        }


        public bool Open(Game Game)
        {
            return true;
        }


        public char Symbol(Game Game, Position Position)
        {
            return '<';
        }


        public ConsoleColor Color()
        {
            return ConsoleColor.White;
        }
    }
}

