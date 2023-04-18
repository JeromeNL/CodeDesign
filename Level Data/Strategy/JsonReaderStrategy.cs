using Level_Data.Factories;
using Level_Data.Models;
using Level_Data.Models.Interfaces;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Level_Data.StrategyPattern
{
    partial class JsonReaderStrategy : IStrategy
    {
        JObject GameJsonObj = JObject.Parse(File.ReadAllText(@"../LevelDataJson.json"));
        public Game CreateBasedOnFile()
        {
            List<IRoom> Rooms = RoomFactory.CreateRoom(GameJsonObj);
            return new Game(ActorFactory.CreatePlayer(GameJsonObj), ConnectionFactory.CreateConnection(GameJsonObj, Rooms), Rooms);
        }
        
    }
}

