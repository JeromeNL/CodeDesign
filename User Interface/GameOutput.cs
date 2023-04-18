using Level_Data.Models;
using Level_Data.Models.Interfaces;
using Level_Data.StrategyPattern;
using System.Reflection.Emit;

namespace User_Interface
{
    public static class GameOutput
    {
        private const string Title = "Der Tempel des Todes - Janique & Joram";

        public static void PrintGameBasedOnPlayerId(Game Game)
        {
            Console.Clear();
            PrintHeader();
            IRoom Room = Game.Rooms.First(e => e.Id == Game.Player.StartRoomId);

            for (int Ypos = 0; Ypos < Room.Height; Ypos++)
            {
                for (int xPos = 0; xPos < Room.Width; xPos++)
                {
                    ITile tile = Room.Tiles.First(e => e.Key.X == xPos && e.Key.Y == Ypos).Value;
                    SetConsoleColor(tile.Color());
                    PrintSymbol(tile.Symbol(Game, new Position(xPos, Ypos)));
                }
                PrintLine();
            }
            PrintStatistics(Game);
        }


        public static void PrintMainMenu(bool XmlIsSelected)
        {
            Console.Clear();
            PrintVisibleLine(Console.WindowWidth);
            Console.WriteLine("\r\n______            _____                         _       _             _____         _           \r\n|  _  \\          |_   _|                       | |     | |           |_   _|       | |          \r\n| | | |___ _ __    | | ___ _ __ ___  _ __   ___| |   __| | ___  ___    | | ___   __| | ___  ___ \r\n| | | / _ \\ '__|   | |/ _ \\ '_ ` _ \\| '_ \\ / _ \\ |  / _` |/ _ \\/ __|   | |/ _ \\ / _` |/ _ \\/ __|\r\n| |/ /  __/ |      | |  __/ | | | | | |_) |  __/ | | (_| |  __/\\__ \\   | | (_) | (_| |  __/\\__ \\\r\n|___/ \\___|_|      \\_/\\___|_| |_| |_| .__/ \\___|_|  \\__,_|\\___||___/   \\_/\\___/ \\__,_|\\___||___/\r\n                                    | |                                                         \r\n                                    |_|                                                         \r\n");
            PrintVisibleLine(Console.WindowWidth);
            Console.WriteLine("Wählen Sie einen Reader zum Laden aus.\n\n");
            
            if(XmlIsSelected)
            {
                PrintXmlChooser();
            }
            else
            {
                PrintJsonChooser();
            }

            PrintVisibleLine(Console.WindowWidth);
        }


        public static void PrintXmlChooser()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(" Die XML-Datei  ");
            Console.ResetColor();
            Console.WriteLine(" Die JSON-Datei  ");
        }

        public static void PrintJsonChooser()
        {
            Console.WriteLine(" Die XML-Datei  ");
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(" Die JSON-Datei  ");
            Console.ResetColor();
        }


        private static void PrintHeader()
        {
            PrintVisibleLine(Title.Length);
            Console.WriteLine(Title);
            PrintVisibleLine(Title.Length);
            Console.WriteLine();
        }


        private static void PrintStatistics(Game game)
        {
            Console.WriteLine();
            Console.ResetColor();
            PrintVisibleDoubleLine(Title.Length);
            Console.WriteLine();
            Console.Write("Leben übrig: ");
            SetConsoleColor(ConsoleColor.Red);
            for (int i = 0; i < game.Player.Lives; i++)
            {
                Console.Write("♥ ");
            }

            Console.ResetColor();
            Console.WriteLine("\nTasche: ");

            PrintItemToInventory(game);
            PrintVisibleDoubleLine(Title.Length);
        }

        private static void PrintItemToInventory(Game game)
        {
            Dictionary<string, int> amountOfItems = new Dictionary<string, int>
            {
                { "Sankara Steine", 0 },
                { "Grüner Schlüssel", 0 },
                { "Roter Schlüssel", 0 }
            };


            foreach (Iitem item in game.Player.Items)
            {
                if (item.Type == "sankara_stone")
                {
                    amountOfItems.TryGetValue("Sankara Steine", out int count);
                    amountOfItems["Sankara Steine"] = count + 1;
                }
                else if (item.Type == "key" && item.ItemColor == ConsoleColor.Green)
                {
                    amountOfItems.TryGetValue("Grüner Schlüssel", out int count);
                    amountOfItems["Grüner Schlüssel"] = count + 1;
                }
                else if (item.Type == "key" && item.ItemColor == ConsoleColor.Red)
                {
                    amountOfItems.TryGetValue("Roter Schlüssel", out int count);
                    amountOfItems["Roter Schlüssel"] = count + 1;
                }
            }


            foreach (KeyValuePair<string, int> itemCategory in amountOfItems)
            {
                if (itemCategory.Value > 0) Console.WriteLine(itemCategory.Value + "x " + itemCategory.Key.ToString());
            }
        }

        public static void PrintGameWon(Game game)
        {
            Console.Clear();
            PrintVisibleDoubleLine(Console.WindowWidth);
            Console.Write("\r\n______         _               _                                                     _ \r\n|  _  \\       | |             | |                                                   | |\r\n| | | |_   _  | |__   __ _ ___| |_    __ _  _____      _____  _ __  _ __   ___ _ __ | |\r\n| | | | | | | | '_ \\ / _` / __| __|  / _` |/ _ \\ \\ /\\ / / _ \\| '_ \\| '_ \\ / _ \\ '_ \\| |\r\n| |/ /| |_| | | | | | (_| \\__ \\ |_  | (_| |  __/\\ V  V / (_) | | | | | | |  __/ | | |_|\r\n|___/  \\__,_| |_| |_|\\__,_|___/\\__|  \\__, |\\___| \\_/\\_/ \\___/|_| |_|_| |_|\\___|_| |_(_)\r\n                                      __/ |                                            \r\n                                     |___/                                             \r\n");
            PrintVisibleDoubleLine(Console.WindowWidth);
        }

        public static void printDied(Game game)
        {
            Console.Clear();
            PrintVisibleDoubleLine(Console.WindowWidth);
            Console.Write("\r\n______         _     _     _     _      _     _             _        _     \r\n|  _  \\       | |   (_)   | |   | |    (_)   | |           | |      | |    \r\n| | | |_   _  | |__  _ ___| |_  | | ___ _  __| | ___ _ __  | |_ ___ | |_   \r\n| | | | | | | | '_ \\| / __| __| | |/ _ \\ |/ _` |/ _ \\ '__| | __/ _ \\| __|  \r\n| |/ /| |_| | | |_) | \\__ \\ |_  | |  __/ | (_| |  __/ |    | || (_) | |_ _ \r\n|___/  \\__,_| |_.__/|_|___/\\__| |_|\\___|_|\\__,_|\\___|_|     \\__\\___/ \\__(_)\r\n                                                                           \r\n                                                                           \r\n");
            PrintVisibleDoubleLine(Console.WindowWidth);
        }


        private static void SetConsoleColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }

        private static void PrintSymbol(char symbol)
        {
            Console.Write(symbol + " ");
        }

        private static void PrintLine()
        {
            Console.WriteLine();
        }

        private static void PrintVisibleLine(int length)
        {
            for (int i = 0; i < length; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
        }

        private static void PrintVisibleDoubleLine(int length)
        {
            for (int i = 0; i < length; i++)
            {
                Console.Write("=");
            }
        }
    }
}












