using Level_Data;
using Level_Data.Models.Interfaces;
using Level_Data.StrategyPattern;

namespace Level_Data.Models.Interfaces
{
    public interface IStrategy
    {
        Game CreateBasedOnFile();
    }
}