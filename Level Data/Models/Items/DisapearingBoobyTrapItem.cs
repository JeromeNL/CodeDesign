﻿
using Level_Data;
using Level_Data.Models;
using Level_Data.Models.Interfaces;
using Level_Data.Models.Items;
using Level_Data.StrategyPattern;

namespace Level_Data.Models.Items
{

    public class DisapearingBoobyTrapItem : Iitem, ITile
    {
        public string Type { get; set; }
        public ConsoleColor? ItemColor { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int? Damage { get; set; }


        public DisapearingBoobyTrapItem(string setType, ConsoleColor? setColor, int setX, int setY, int? setDamage)
        {
            Type = setType;
            ItemColor = setColor;
            X = setX;
            Y = setY;
            Damage = setDamage;
        }


        public void Use(Game game)
        {
            game.Rooms.First(x => x.Id == game.Player.StartRoomId).Items.RemoveAll(e => e.Type.Equals("disappearing_boobytrap") && e.X == X && e.Y == Y);
            game.Player.Lives -= Damage.Value;
        }


        public bool Interact(Game game)
        {
            Use(game);
            return true;
        }


        public char Symbol(Game game, Position position)
        {
            return '@';
        }


        public ConsoleColor Color()
        {
            return ConsoleColor.White;
        }
    }
}


