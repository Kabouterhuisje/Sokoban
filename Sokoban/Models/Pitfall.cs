using System;
using Sokoban.Enums;
using Sokoban.Views;

namespace Sokoban.Models
{
    class Pitfall : Field
    {
        private int _lives = 3;

        public bool HasCrate { get; set; }

        public bool HasForklift { get; set; }

        public bool HasPitfall { get; set; }


        public Pitfall(Location coordinate) : base(coordinate)
        {
            Type = FieldType.PitFall;
        }

        public int GetLives()
        {
            return _lives;
        }

        public override char GetIdentifier()
        {
            if (HasForklift)
            {
                if (_lives > 0)
                {
                    _lives--;
                    return '@';
                }
            }
            if (HasCrate)
            {
                if (_lives > 0)
                {
                    _lives--;
                    return 'O';
                }
            }
            if (HasPitfall)
            {
                if (_lives == 0)
                {
                    return ' ';
                }
                return '~';
            }

            return base.GetIdentifier();
        }
    }
}
