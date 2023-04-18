using Level_Data.Models;
using Level_Data.StrategyPattern;

namespace User_Interface
{
    public static class GameInput
    {
        public static Position GetKeyInput(Game Game)
        {
            Position Position = new Position(Game.Player.CurrentX, Game.Player.CurrentY);
            ConsoleKey Input = Console.ReadKey().Key;

            if (Input.Equals(ConsoleKey.UpArrow))
            {
                Position.Y -= 1;
            }
            else if (Input.Equals(ConsoleKey.RightArrow))
            {
                Position.X += 1;
            }
            else if (Input.Equals(ConsoleKey.DownArrow))
            {
                Position.Y += 1;
            }
            else if (Input.Equals(ConsoleKey.LeftArrow))
            {
                Position.X -= 1;
            }
            return Position;
        }


        private static ConsoleKey GetDefaultKeyInput()
        {
            return Console.ReadKey().Key;
        }


        public static bool GetFileSelection()
        {
            bool XmlIsSelected = true;
            bool pressedEnter = false;

            while (!pressedEnter)
            {
                var input = GameInput.GetDefaultKeyInput();

                if (input.Equals(ConsoleKey.Enter))
                {
                    pressedEnter = true;
                }

                if (input.Equals(ConsoleKey.UpArrow) || input.Equals(ConsoleKey.DownArrow))
                {
                    XmlIsSelected = !XmlIsSelected;
                }
                   
                GameOutput.PrintMainMenu(XmlIsSelected);
            }
            
            return XmlIsSelected;
        }
    }

}
