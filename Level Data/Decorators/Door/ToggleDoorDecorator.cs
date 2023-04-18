
using Level_Data;
using Level_Data.Decorators.Door;
using Level_Data.Models;
using Level_Data.Models.Interfaces;
using Level_Data.Models.Items;
using Level_Data.StrategyPattern;

namespace Level_Data.Decorators.Door
{
    public class ToggleDoorDecorator : DoorDecorator, IObserver, ITile
    {
        private bool _ObserverState;
        private PressurePlateItem _Subject;
        private PressurePlateItem Subject
        {
            get { return _Subject; }
            set { _Subject = value; }
        }

        public ToggleDoorDecorator(IDoor Door, PressurePlateItem Subject) : base(Door)
        {
            _Subject = Subject;
        }


        public override bool Open(Game Game)
        {
            return _ObserverState;
        }


        public bool Interact(Game Game)
        {
            return Open(Game);
        }


        public char Symbol(Game Game, Position Position)
        {
            return 'G';
        }


        public ConsoleColor Color()
        {
            return Door.Color().Equals("white") ? ConsoleColor.White : Door.Color();
        }


        public void Update()
        {
            if (_ObserverState == true)
            {
                _ObserverState = false;
            }
            else if (_ObserverState == false)
            {
                _ObserverState = true;
            }
        }
    }
}

