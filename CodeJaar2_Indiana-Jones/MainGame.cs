using Level_Data.Models;
using Level_Data.Models.Interfaces;
using Level_Data.StrategyPattern;
using User_Interface;

namespace CodeJaar2_Indiana_Jones
{
    public class MainGame
    {
        public Game MainGameObject { get; set; }
        bool XmlIsSelected = true;

        public MainGame()
        {
            OpenMainMenu();
        }

        private void OpenMainMenu()
        {
            GameOutput.PrintMainMenu(XmlIsSelected);
            MainGameObject = ConvertGameToMatrix(MainReader.ReadGameData(GameInput.GetFileSelection()));
            RunGameLoop();
        }


        private void RunGameLoop()
        {
            while (MainGameObject.Running)
            {
                if (GameHasStopped()) break;
               
                GameOutput.PrintGameBasedOnPlayerId(MainGameObject);
                MainGameObject = ConvertGameToMatrix(MainGameObject.Player.DecidePlayerAction(GameInput.GetKeyInput(MainGameObject), MainGameObject));
            }
        }


        private bool GameHasStopped()
        {
            if (MainGameObject.HasDied()){
                GameOutput.printDied(MainGameObject);
            }
            if (MainGameObject.HasWon()){ 
                GameOutput.PrintGameWon(MainGameObject);
                return true; 
            }
            return false;
        }

        public Game ConvertGameToMatrix(Game Game)
        {
            List<IRoom> Rooms = new List<IRoom>();
            foreach (Room Room in Game.Rooms)
            {
                Room.Tiles = Room.ConvertRoomMatrix(Game, Room);
                Rooms.Add(Room);
            }
            Game.Rooms = Rooms;
            return Game;
        }

    }
}

