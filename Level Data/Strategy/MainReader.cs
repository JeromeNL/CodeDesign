namespace Level_Data.StrategyPattern
{
    public static class MainReader
    {
        public static Game ReadGameData(bool XmlIsSelected)
        {
            Context Context = new Context();

            if (XmlIsSelected)
            {
                Context.SetStrategy(new XmlReaderStrategy());
            }
            else
            {
                Context.SetStrategy(new JsonReaderStrategy());
            }

            return Context.GetGame();
        }
    }
}