using Level_Data.Factories;
using Level_Data.Models;
using Level_Data.Models.Interfaces;
using System.Xml;

namespace Level_Data.StrategyPattern
{
    class XmlReaderStrategy : IStrategy
    {
        public XmlReaderStrategy()
        {

        }

        public Game CreateBasedOnFile()
        {
            XmlDocument XML = new XmlDocument();
            XML.Load(@"../LevelDataXml.xml");
            List<IRoom> Rooms = RoomFactory.CreateXMLRoom(XML);
            Game Game = new Game(ActorFactory.CreateXMLPlayer(XML), ConnectionFactory.CreateXMLConnection(XML, Rooms, ActorFactory.CreateXMLPlayer(XML)), Rooms);
            return Game;
        }
    }
}







