using Sokoban.Enums;

namespace Sokoban.Models
{
	public abstract class Field
	{
		public bool CanWalkOn = true;

		public FieldType Type
		{
			get;
			set;
		}

		public Location Coordinate
		{
			get;
			set;
		}

	    protected Field(Location coordinate)
        {
            Coordinate = coordinate;
        }

		public virtual char GetIdentifier()
		{
            return '·';
		}

	}
}

