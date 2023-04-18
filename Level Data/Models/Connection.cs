

using Level_Data.Models;
using Level_Data.Models.Interfaces;
using System.Data;

namespace Level_Data.StrategyPattern
{
    public class Connection : IConnection
    {
        public int? North { get; set; }
        public int? East { get; set; }
        public int? South { get; set; }
        public int? West { get; set; }
        public IDoor Door { get; set; }
    }
}

