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
    public static class ConnectionFactory
    {


        public static List<Connection> CreateXMLConnection(XmlDocument XML, List<IRoom> Rooms, Player Player)
        {
            XmlNode ConnectionNode = XML.DocumentElement.SelectSingleNode("/temple/connections");
            List<Connection> Connections = new List<Connection>();

            for (int i = 0; i < ConnectionNode.ChildNodes.Count; i++)
            {
                Connection Connection = new Connection();
                if (ConnectionNode.ChildNodes[i].Attributes["NORTH"] != null) { Connection.North = int.Parse(ConnectionNode.ChildNodes[i].Attributes["NORTH"].Value); }
                if (ConnectionNode.ChildNodes[i].Attributes["SOUTH"] != null) { Connection.South = int.Parse(ConnectionNode.ChildNodes[i].Attributes["SOUTH"].Value); }
                if (ConnectionNode.ChildNodes[i].Attributes["WEST"] != null) { Connection.West = int.Parse(ConnectionNode.ChildNodes[i].Attributes["WEST"].Value); }
                if (ConnectionNode.ChildNodes[i].Attributes["EAST"] != null) { Connection.East = int.Parse(ConnectionNode.ChildNodes[i].Attributes["EAST"].Value); }
                if (ConnectionNode.ChildNodes[i].InnerXml != "") { Connection.Door = DoorFactory.CreateXMLRoomDoors(ConnectionNode.ChildNodes[i], Rooms, Player); }
                Connections.Add(Connection);
            }
            return Connections;
        }





        public static List<Connection> CreateConnection(JObject JObject, List<IRoom> Rooms)
        {
            List<Connection> Connections = new List<Connection>();
            foreach (JToken JTokenConnections in JObject["connections"])
            {
                Connection Connection = new Connection();
                if (JTokenConnections["NORTH"] != null) { Connection.North = JTokenConnections["NORTH"].Value<int?>(); }
                if (JTokenConnections["EAST"] != null) { Connection.East = JTokenConnections["EAST"].Value<int?>(); }
                if (JTokenConnections["WEST"] != null) { Connection.West = JTokenConnections["WEST"].Value<int?>(); }
                if (JTokenConnections["SOUTH"] != null) { Connection.South = JTokenConnections["SOUTH"].Value<int?>(); }

                List<IDoor> doors = new List<IDoor>();
                if (JTokenConnections["doors"] != null) { Connection.Door = DoorFactory.ProduceDoors(JToken.FromObject(JTokenConnections["doors"]), Rooms); }
                Connections.Add(Connection);
            }
            return Connections;
        }

    }
}
