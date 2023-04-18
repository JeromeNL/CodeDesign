
using Level_Data;
using Level_Data.Models;
using Level_Data.Models.Interfaces;
using Level_Data.StrategyPattern;

namespace Level_Data.Models.Interfaces
{
    public interface IDoor : ITile
    {
        bool Open(Game Game);
    }
}

