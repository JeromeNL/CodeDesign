using Level_Data;
using Level_Data.Decorators.Door;
using Level_Data.Models;
using Level_Data.Models.Interfaces;
using Level_Data.StrategyPattern;

namespace Level_Data.Decorators.Door
{

    public class OpenOnStonesInRoomDoorDecorator : DoorDecorator, ITile
    {
        public int NumberOfStones { get; set; }


        public OpenOnStonesInRoomDoorDecorator(IDoor Door, int NumberOfStones) : base(Door)
        {
            this.NumberOfStones = NumberOfStones;
        }


        public override bool Open(Game Game)
        {
            if (Door.Open(Game) && Game.Rooms[Game.Player.StartRoomId - 1].Items.Where(x => x.Type.Equals("sankara_stone")).Count() == NumberOfStones)
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

