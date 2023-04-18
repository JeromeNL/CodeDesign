using Level_Data;
using Level_Data.Factories;
using Level_Data.Models;
using Level_Data.Models.Interfaces;
using Level_Data.StrategyPattern;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Level_Data.Factories
{
    public static class ActorFactory
    {
        public static Player CreateXMLPlayer(XmlDocument XML)
        {
            List<Iitem> Items = new List<Iitem>();
            XmlNode PlayerNode = XML.DocumentElement.SelectSingleNode("/temple/player/start");

            return new Player
            {
                CurrentY = int.Parse(PlayerNode.ChildNodes.OfType<XmlElement>().First(e => e.LocalName == "y").InnerText),
                CurrentX = int.Parse(PlayerNode.ChildNodes.OfType<XmlElement>().First(e => e.LocalName == "x").InnerText),
                StartRoomId = int.Parse(PlayerNode.ChildNodes.OfType<XmlElement>().First(e => e.LocalName == "roomId").InnerText),
                Lives = int.Parse(XML.GetElementsByTagName("player")[0].Attributes["lives"].Value),
                Items = Items,
            };
        }


        public static IActor CreatePlayer(JObject GameJsonObj)
        {
            List<Iitem> Items = new List<Iitem>();
            JToken Player = GameJsonObj["player"];
            return new Player
            {
                CurrentY = Player["startY"].Value<int>(),
                CurrentX = Player["startX"].Value<int>(),
                StartRoomId = Player["startRoomId"].Value<int>(),
                Lives = Player["lives"].Value<int>(),
                Items = Items,
            };
        }
    }
}
