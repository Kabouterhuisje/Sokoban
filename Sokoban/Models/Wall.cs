using Sokoban.Enums;

namespace Sokoban.Models
{
	public class Wall : Field
    {
        public Wall(Location coordinate) : base(coordinate)
        {
            Type = FieldType.Wall;
            CanWalkOn = false;
        }

		public override char GetIdentifier()
		{
		    return '█';
		}
	}
}

