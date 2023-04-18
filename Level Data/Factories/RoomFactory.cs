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
    public static class RoomFactory
    {

        public static List<IRoom> CreateXMLRoom(XmlDocument XML)
        {
            XmlNode RoomNode = XML.DocumentElement.SelectSingleNode("/temple/rooms");
            List<IRoom> Rooms = new List<IRoom>();

            for (int i = 0; i < RoomNode.ChildNodes.Count; i++)
            {
                List<Iitem> SetItems = new List<Iitem>();
                if (RoomNode.ChildNodes[i].InnerXml != "") { SetItems = CreateXMLRoomItems(RoomNode.ChildNodes[i]); }
                Rooms.Add(new Room
                {
                    Id = int.Parse(RoomNode.ChildNodes[i].Attributes["id"].Value),
                    Width = int.Parse(RoomNode.ChildNodes[i].Attributes["width"].Value),
                    Height = int.Parse(RoomNode.ChildNodes[i].Attributes["height"].Value),
                    Items = SetItems
                });
            }
            return Rooms;
        }


        private static List<Iitem> CreateXMLRoomItems(XmlNode RoomNode)
        {
            XmlDocument RoomDoc = new XmlDocument();
            List<Iitem> Items = new List<Iitem>();

            if (RoomNode.InnerXml != "")
            {
                RoomDoc.LoadXml(RoomNode.InnerXml);
                XmlNode ItemNode = RoomDoc.DocumentElement.SelectSingleNode("/items");

                for (int i = 0; i < ItemNode.ChildNodes.Count; i++)
                {
                    ItemsFactory ItemFactory = new ItemsFactory(ItemNode.ChildNodes[i]);
                    Items.Add(ItemFactory.ProduceItems());
                }
            }
            return Items;
        }


        public static List<IRoom> CreateRoom(JObject GameJsonObj)
        {
            List<IRoom> Rooms = new List<IRoom>();
            foreach (JToken Room in GameJsonObj["rooms"])
            {
                List<Iitem> Items = new List<Iitem>();
                if (Room["items"] != null) { Items = CreateRoomItems(JToken.FromObject(Room["items"])); }
                Rooms.Add(new Room
                {
                    Id = Room["id"].Value<int>(),
                    Width = Room["width"].Value<int>(),
                    Height = Room["height"].Value<int>(),
                    type = Room["type"].Value<string>(),
                    Items = Items,
                    toggle = true
                });
            }
            return Rooms;
        }


        public static List<Iitem> CreateRoomItems(JToken JtokenItems)
        {
            List<Iitem> Items = new List<Iitem>();
            foreach (JToken JtokenItem in JtokenItems)
            {
                var ItemFactory = new ItemsFactory(JtokenItem);
                Items.Add(ItemFactory.ProduceItems());
            }
            return Items;
        }

    }
}
