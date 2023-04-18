using Level_Data.Decorators.Door;
using Level_Data.Models;
using Level_Data.Models.Interfaces;
using Level_Data.Models.Items;

namespace Level_Data.StrategyPattern
{

    public class Room : IRoom
    {
        public int Id { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string type { get; set; }
        public List<Iitem> Items { get; set; }
        public Dictionary<Position, ITile> Tiles { get; set; }
        public bool toggle { get; set; }

        public Dictionary<Position, ITile> ConvertRoomMatrix(Game Game, Room Room)
        {
            Dictionary<Position, ITile> ConnectionTiles = CreateRoomTiles(Game, Room);
            Dictionary<Position, ITile> WallTiles = CreateWallTiles(ConnectionTiles, Room);
            Dictionary<Position, ITile> ItemTiles = CreateItemTiles(WallTiles, Room);
            Dictionary<Position, ITile> EmptyTiles = CreateEmptyTiles(ItemTiles, Room, Game);
            return CreatePlayerItems(EmptyTiles, Room, Game);
        }


        private static Dictionary<Position, ITile> CreateRoomTiles(Game Game, Room Room)
        {
            Dictionary<Position, ITile> Tiles = new Dictionary<Position, ITile>();
            List<Connection> Connections = Game.Connections.Where(e => e.North == Room.Id || e.South == Room.Id || e.East == Room.Id || e.West == Room.Id).ToList();
            PlainDoor PlainDoor = new PlainDoor();
            DoorDecorator? DecoratedDoor = new OpenedDoorDecorator(PlainDoor);

            foreach (Connection? Conn in Connections)
            {
                if (Conn.Door == null && Conn.West != null && Conn.West != Room.Id) { Tiles.Add(new Position(0, Room.Height / 2), DecoratedDoor); }
                else if (Conn.Door != null && Conn.West != null && Conn.West != Room.Id) { Tiles.Add(new Position(0, Room.Height / 2), Conn.Door); }

                if (Conn.Door == null && Conn.East != null && Conn.East != Room.Id) { Tiles.Add(new Position(Room.Width - 1, Room.Height / 2), DecoratedDoor); }
                else if (Conn.Door != null && Conn.East != null && Conn.East != Room.Id) { Tiles.Add(new Position(Room.Width - 1, Room.Height / 2), Conn.Door); }

                if (Conn.Door == null && Conn.North != null && Conn.North != Room.Id) { Tiles.Add(new Position(Room.Width / 2, 0), DecoratedDoor); }
                else if (Conn.Door != null && Conn.North != null && Conn.North != Room.Id) { Tiles.Add(new Position(Room.Width / 2, 0), Conn.Door); }

                if (Conn.Door == null && Conn.South != null && Conn.South != Room.Id) { Tiles.Add(new Position(Room.Width / 2, Room.Height - 1), DecoratedDoor); }
                else if (Conn.Door != null && Conn.South != null && Conn.South != Room.Id) { Tiles.Add(new Position(Room.Width / 2, Room.Height - 1), Conn.Door); }
            }
            return Tiles;
        }


        private static Dictionary<Position, ITile> CreateItemTiles(Dictionary<Position, ITile> Tiles, Room Room)
        {
            foreach (Iitem Item in Room.Items)
            {
                Tiles.Add(new Position(Item.X, Item.Y), Item);
            }
            return Tiles;
        }


        private static Dictionary<Position, ITile> CreateWallTiles(Dictionary<Position, ITile> Tiles, Room Room)
        {
            PlainDoor PlainDoor = new PlainDoor();
            DoorDecorator? DecoratedDoor = new WallDecorator(PlainDoor);

            for (int Xpos = 0; Xpos < Room.Width; Xpos++)
            {
                for (int Ypos = 0; Ypos < Room.Height; Ypos++)
                {
                    int NoOfConnectionsOfRoom = Tiles.Where(e => e.Key.X == Xpos && e.Key.Y == Ypos).Count();

                    if ((Xpos == 0 || Ypos == 0 || Xpos == Room.Width - 1 || Ypos == Room.Height - 1) && NoOfConnectionsOfRoom < 1)
                    {
                        Tiles.Add(new Position(Xpos, Ypos), DecoratedDoor);
                    }
                }
            }
            return Tiles;
        }


        private static Dictionary<Position, ITile> CreateEmptyTiles(Dictionary<Position, ITile> Tiles, Room Room, Game Game)
        {
            for (int Xpos = 0; Xpos < Room.Width; Xpos++)
            {
                for (int Ypos = 0; Ypos < Room.Height; Ypos++)
                {
                    int NoOfEmptyTiles = Tiles.Where(e => e.Key.X == Xpos && e.Key.Y == Ypos).Count();

                    if (NoOfEmptyTiles < 1 && !(Game.Player.CurrentX == Xpos && Game.Player.CurrentY == Ypos && Game.Player.StartRoomId == Room.Id))
                    {
                        Tiles.Add(new Position(Xpos, Ypos), new Tile(Xpos, Ypos));
                    }
                }
            }
            return Tiles;
        }


        private static Dictionary<Position, ITile> CreatePlayerItems(Dictionary<Position, ITile> Tiles, Room Room, Game Game)
        {
            for (int Xpos = 0; Xpos < Room.Width; Xpos++)
            {
                for (int Ypos = 0; Ypos < Room.Height; Ypos++)
                {
                    if (Game.Player.CurrentX == Xpos && Game.Player.CurrentY == Ypos && Game.Player.StartRoomId == Room.Id)
                    {
                        Tiles.Add(new Position(Xpos, Ypos), (ITile)Game.Player);
                    }
                }
            }
            return Tiles;
        }






    }
}


