using Level_Data.Models;
using Level_Data.Models.Interfaces;

namespace Level_Data.StrategyPattern
{
    public class Player : ITile, IActor
    {
        public int StartRoomId { get; set; }
        public int CurrentX { get; set; }
        public int CurrentY { get; set; }
        public int Lives { get; set; }
        public List<Iitem> Items { get; set; }


        public bool Interact(Game game)
        {
            return true;
        }

        public char Symbol(Game game, Position position)
        {
            return 'X';
        }

        public ConsoleColor Color()
        {
            return ConsoleColor.White;
        }


        public Game DecidePlayerAction(Position Pos, Game Game)
        {
            Dictionary<Position, ITile> RoomTiles = Game.Rooms.First(e => e.Id == Game.Player.StartRoomId).Tiles;
            IRoom Room = Game.Rooms.First(e => e.Id == Game.Player.StartRoomId);
            List<Connection> SortedConnections = Game.Connections.Where(e => e.North == Room.Id || e.South == Room.Id || e.East == Room.Id || e.West == Room.Id).ToList();

            if ((Pos.X == 0 || Pos.Y == 0 || Pos.X == Room.Width - 1 || Pos.Y == Room.Height - 1) && (!RoomTiles.First(e => e.Key.X == Pos.X && e.Key.Y == Pos.Y).Value.Symbol(Game, new Position(0, 0)).Equals('#')))
            {
                EnterDoor(RoomTiles, Room, SortedConnections, Pos, Game);
            }
            else if ((Pos.X > 0 || Pos.Y > 0 || Pos.X < Room.Width || Pos.Y < Room.Height) && (!RoomTiles.First(e => e.Key.X == Pos.X && e.Key.Y == Pos.Y).Value.Symbol(Game, new Position(0, 0)).Equals('#')))
            {
                UseItem(Pos, Game);
            }

            return Game;
        }


        private Game UseItem(Position Pos, Game Game)
        {
            Game.Rooms.First(e => e.Id == Game.Player.StartRoomId).Tiles.First(e => e.Key.X == Pos.X && e.Key.Y == Pos.Y).Value.Interact(Game);
            Game.Player.CurrentX = Pos.X;
            Game.Player.CurrentY = Pos.Y;

            return Game;
        }


        private Game EnterDoor(Dictionary<Position, ITile> RoomTiles, IRoom Room, List<Connection> Connections, Position Pos, Game Game)
        {
            foreach (Connection Conn in Connections)
            {
                bool Open = (RoomTiles.First(e => e.Key.X == Pos.X && e.Key.Y == Pos.Y).Value.Interact(Game));

                if (Open && (Conn.West != null && Conn.West != Room.Id && (Pos.X == 0 && Pos.Y == Room.Height / 2)))
                {
                    return DirectPlayer(Game, Conn, (int)Conn.West, Game.Rooms.First(e => e.Id == (int)Conn.West).Width - 2, Game.Rooms.First(e => e.Id == (int)Conn.West).Height / 2);
                }

                if (Open && (Conn.East != null && Conn.East != Room.Id && (Pos.X == Room.Width - 1 && Pos.Y == Room.Height / 2)))
                {
                    return DirectPlayer(Game, Conn, (int)Conn.East, 1, Game.Rooms.First(e => e.Id == (int)Conn.East).Height / 2);
                }

                if (Open && (Conn.North != null && Conn.North != Room.Id && (Pos.X == Room.Width / 2 && Pos.Y == 0)))
                {
                    return DirectPlayer(Game, Conn, (int)Conn.North, Game.Rooms.First(e => e.Id == (int)Conn.North).Width / 2, Game.Rooms.First(e => e.Id == (int)Conn.North).Height - 2);
                }

                if (Open && (Conn.South != null && Conn.South != Room.Id && (Pos.X == Room.Width / 2 && Pos.Y == Room.Height - 1)))
                {
                    return DirectPlayer(Game, Conn, (int)Conn.South, Game.Rooms.First(e => e.Id == (int)Conn.South).Width / 2, 1);
                }
            }
            return Game;
        }


        private Game DirectPlayer(Game Game, Connection conn, int Direction, int X, int Y)
        {
            Game.Player.StartRoomId = Direction;
            Game.Player.CurrentX = X;
            Game.Player.CurrentY = Y;
            Game.Rooms.First(e => e.Id == Direction).Tiles.First(e => e.Key.X == Game.Player.CurrentX && e.Key.Y == Game.Player.CurrentY).Value.Interact(Game);

            return Game;
        }

    }
}



