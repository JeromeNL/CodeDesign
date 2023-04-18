using Level_Data.Models.Interfaces;

namespace Level_Data.StrategyPattern
{
    class Context
    {
        private IStrategy _Strategy;

        public Context()
        {
        }

        public Context(IStrategy Strategy)
        {
            _Strategy = Strategy;
        }

        public void SetStrategy(IStrategy Strategy)
        {
            _Strategy = Strategy;
        }

        public Game GetGame()
        {
            return _Strategy.CreateBasedOnFile();
        }
    }
}
