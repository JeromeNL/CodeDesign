
using Level_Data;
using Level_Data.Decorators.Door;
using Level_Data.Models;
using Level_Data.Models.Interfaces;
using Level_Data.StrategyPattern;

namespace Level_Data.Decorators.Door
{
    public class OpenOnOddDoorDecorator : DoorDecorator, ITile
    {
        public OpenOnOddDoorDecorator(IDoor Door) : base(Door)
        {
        }


        public override bool Open(Game Game)
        {
            int Stones = Game.Player.Items.Where(x => x.Type.Equals("sankara_stone")).Count();

            if (Door.Open(Game) && Game.Player.Lives % 2 != 0)
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
            if (Door.Symbol(Game, Position).Equals('G'))
            {
                return 'G';
            }

            if (Position.Y == Game.Rooms.First(e => e.Id == Game.Player.StartRoomId).Height / 2)
            {
                return '|';
            }
            return '=';
        }


        public ConsoleColor Color()
        {
            return Door.Color().Equals("white") ? ConsoleColor.White : Door.Color();
        }
    }
}

