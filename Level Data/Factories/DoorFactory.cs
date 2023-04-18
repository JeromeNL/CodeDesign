using Level_Data;
using Level_Data.Decorators.Door;
using Level_Data.Factories;
using Level_Data.Models;
using Level_Data.Models.Interfaces;
using Level_Data.Models.Items;
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

    public static class DoorFactory
    {

        public static IDoor CreateXMLRoomDoors(XmlNode RoomNode, List<IRoom> Rooms, Player Player)
        {
            XmlDocument RoomDoc = new XmlDocument();
            bool FirstDoorIsCreated = false;
            DoorDecorator? DoorDecorator = null;
            PressurePlateItem PressurePlateItem = (PressurePlateItem)Rooms[6].Items.First(e => e.Type.Equals("pressure_plate"));

            if (RoomNode.InnerXml != "")
            {
                RoomDoc.LoadXml(RoomNode.InnerXml);
                XmlNode ItemNode = RoomDoc.DocumentElement.SelectSingleNode("/doors");

                for (int i = 0; i < ItemNode.ChildNodes.Count; i++)
                {
                    if (FirstDoorIsCreated == false)
                    {
                        PlainDoor PlainDoor = new PlainDoor();
                        string Type = ItemNode.ChildNodes[i].LocalName;
                        if (Type.Equals("colouredDoor")) { DoorDecorator = new ColoredDoorDecorator(PlainDoor, ItemNode.ChildNodes[i].Attributes["color"].Value); }
                        if (Type.Equals("openOnStoneInRoomDoor")) { DoorDecorator = new OpenOnStonesInRoomDoorDecorator(PlainDoor, int.Parse(ItemNode.ChildNodes[i].Attributes["no_of_stones"].Value)); }
                        if (Type.Equals("openOnOddLivesDoor")) { DoorDecorator = new OpenOnOddDoorDecorator(PlainDoor); }
                        if (Type.Equals("toggleDoor")) { DoorDecorator = new ToggleDoorDecorator(PlainDoor, PressurePlateItem); PressurePlateItem.Attach((IObserver)DoorDecorator); }
                        if (Type.Equals("closingGate")) { DoorDecorator = new ClosingDoorDecorator(PlainDoor); }
                        FirstDoorIsCreated = true;
                        if (ItemNode.ChildNodes.Count == 1) { return DoorDecorator; }
                    }
                    else
                    {
                        IDoor? DecoratedDoor = null;
                        string Type = ItemNode.ChildNodes[i].LocalName;
                        if (Type.Equals("colouredDoor")) { DecoratedDoor = new ColoredDoorDecorator(DoorDecorator, ItemNode.ChildNodes[i].Attributes["color"].Value); }
                        if (Type.Equals("openOnStoneInRoomDoor")) { DecoratedDoor = new OpenOnStonesInRoomDoorDecorator(DoorDecorator, int.Parse(ItemNode.ChildNodes[i].Attributes["no_of_stones"].Value)); }
                        if (Type.Equals("openOnOddLivesDoor")) { DecoratedDoor = new OpenOnOddDoorDecorator(DoorDecorator); }
                        if (Type.Equals("toggleDoor")) { DecoratedDoor = new ToggleDoorDecorator(DoorDecorator, PressurePlateItem); PressurePlateItem.Attach((IObserver)DoorDecorator); }
                        if (Type.Equals("closingGate")) { DecoratedDoor = new ClosingDoorDecorator(DoorDecorator); }
                        return DecoratedDoor;
                    }
                }
            }
            return DoorDecorator;
        }

        public static IDoor ProduceDoors(JToken JTokenDoor, List<IRoom> Rooms)
        {
            bool FirstDoorIsCreated = false;
            DoorDecorator? DoorDecorator = null;
            PressurePlateItem PressurePlateItem = (PressurePlateItem)Rooms[6].Items.First(e => e.Type.Equals("pressure_plate"));

            foreach (JToken Door in JTokenDoor)
            {
                if (FirstDoorIsCreated == false)
                {
                    PlainDoor PlainDoor = new PlainDoor();
                    string Type = Door["type"].Value<string>();
                    if (Type.Equals("colored")) { DoorDecorator = new ColoredDoorDecorator(PlainDoor, Door["color"].Value<string>()); }
                    if (Type.Equals("open on stones in room")) { DoorDecorator = new OpenOnStonesInRoomDoorDecorator(PlainDoor, Door["no_of_stones"].Value<int>()); }
                    if (Type.Equals("open on odd")) { DoorDecorator = new OpenOnOddDoorDecorator(PlainDoor); }
                    if (Type.Equals("toggle")) { DoorDecorator = new ToggleDoorDecorator(PlainDoor, PressurePlateItem); PressurePlateItem.Attach((IObserver)DoorDecorator); }
                    if (Type.Equals("closing gate")) { DoorDecorator = new ClosingDoorDecorator(PlainDoor); }
                    FirstDoorIsCreated = true;
                    if (JTokenDoor.Count() == 1) { return DoorDecorator; }
                }

                else
                {
                    IDoor? DecoratedDoor = null;
                    string type = Door["type"].Value<string>();
                    if (type.Equals("colored")) { DecoratedDoor = new ColoredDoorDecorator(DoorDecorator, Door["color"].Value<string>()); }
                    if (type.Equals("open on stones in room")) { DecoratedDoor = new OpenOnStonesInRoomDoorDecorator(DoorDecorator, Door["no_of_stones"].Value<int>()); }
                    if (type.Equals("open on odd")) { DecoratedDoor = new OpenOnOddDoorDecorator(DoorDecorator); }
                    if (type.Equals("toggle")) { DecoratedDoor = new ToggleDoorDecorator(DoorDecorator, PressurePlateItem); PressurePlateItem.Attach((IObserver)DoorDecorator); }
                    if (type.Equals("closing gate")) { DecoratedDoor = new ClosingDoorDecorator(DoorDecorator); }
                    return DecoratedDoor;
                }
            }
            return DoorDecorator;
        }

    }
}

