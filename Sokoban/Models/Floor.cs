using Sokoban.Enums;

namespace Sokoban.Models
{
	public class Floor : Field
	{
		public bool HasCrate { get; set; }

		public bool HasForklift { get; set; }

        public Floor(Location coordinate) : base(coordinate)
        {
            Type = FieldType.Floor;
        }

		public override char GetIdentifier()
		{
		    if (HasForklift)
		    {
                return '@';
            }
            
            if (HasCrate)
		    {
		        return 'O';
		    }

		    return base.GetIdentifier();
		}
	}
}

