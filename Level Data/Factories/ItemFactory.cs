using Level_Data.Models.Interfaces;
using Level_Data.Models.Items;
using Newtonsoft.Json.Linq;
using System.Xml;

namespace Level_Data.StrategyPattern
{
    public class ItemsFactory
    {
        public string Type { get; set; }
        public string? Color { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int? Damage { get; set; }
        public JToken JsonItem { get; }
        public ConsoleColor? ItemColor { get; set; }
        private const int DEFAULTDAMAGE = 0;


        public ItemsFactory(JToken JsonItem)
        {
            ItemColor = new ConsoleColor();
            Type = JsonItem["type"].Value<string>();
            X = JsonItem["x"].Value<int>();
            Y = JsonItem["y"].Value<int>();

            try
            {
                Color = JsonItem["color"].Value<string?>();
            }
            catch (Exception)
            {
                Color = "";
            }

            try
            {
                Damage = JsonItem["damage"].Value<int?>();
            }
            catch (Exception)
            {
                Damage = DEFAULTDAMAGE;
            }
        }


        public ItemsFactory(XmlNode XmlItem)
        {
            ItemColor = new ConsoleColor();
            Type = XmlItem.LocalName;
            X = Int32.Parse(XmlItem.Attributes["x"].Value);
            Y = Int32.Parse(XmlItem.Attributes["y"].Value);


            if (XmlItem.Attributes != null && XmlItem.Attributes["color"] != null)
            {
                Color = XmlItem.Attributes["color"].Value;
            }
            else
            {
                Color = "";
            }


            if (XmlItem.Attributes != null && XmlItem.Attributes["damage"] != null)
            {
                Damage = Int32.Parse(XmlItem.Attributes["damage"].Value);
            }
            else
            {
                Damage = DEFAULTDAMAGE;
            }
        }



        public Iitem ProduceItems()
        {
            if ("sankara stone".Equals(Type) || "sankara_stone".Equals(Type))
            {
                Type = "sankara_stone";
                return new SankaraStoneItem(Type, ItemColor, X, Y, Damage);
            }

            else if ("boobytrap".Equals(Type))
            {
                return new BoobyTrapItem(Type, ItemColor, X, Y, Damage);
            }

            else if ("disappearing boobytrap".Equals(Type) || "disappearing_boobytrap".Equals(Type))
            {
                Type = "disappearing_boobytrap";
                return new DisapearingBoobyTrapItem(Type, ItemColor, X, Y, Damage);
            }

            else if ("pressure plate".Equals(Type) || "pressure_plate".Equals(Type))
            {
                Type = "pressure_plate";
                return new PressurePlateItem(Type, ItemColor, X, Y, Damage);
            }

            else if ("key".Equals(Type)) {

                if (Color.Equals("green")) ItemColor = ConsoleColor.Green;

                else if (Color.Equals("red")) ItemColor = ConsoleColor.Red;
                
            return new KeyItem(Type, ItemColor, X, Y, Damage);
            }
            
            return null;
        }
    }
}


