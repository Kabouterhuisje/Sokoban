using Sokoban.Enums;

namespace Sokoban.Models
{
    public class Forklift
    {
        public virtual Location Coordinate { get; private set; }

        public Forklift(Location coordinate)
        {
            Coordinate = coordinate;
        }

        public virtual void Walk(DirectionType direction)
        {
            Coordinate = Game.CalculateNewCoordinate(Coordinate, direction);
        }

    }
}

