using Sokoban.Enums;

namespace Sokoban.Models
{
	public class Crate
	{
	    public virtual Location Coordinate { get; private set; }

        public Crate(Location coordinate)
        {
            Coordinate = coordinate;
        }

		public virtual void Move(DirectionType direction)
		{
            Coordinate = Game.CalculateNewCoordinate(Coordinate, direction);
		}

	}
}

