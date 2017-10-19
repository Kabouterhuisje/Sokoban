using Sokoban.Enums;

namespace Sokoban.Models
{
	public class Destination : Floor
	{
        public Destination(Location coordinate) : base(coordinate)
        {
            Type = FieldType.Box;
        }

		public override char GetIdentifier()
		{
		    if (HasForklift)
		    {
                return '@';
		    }
            
            if (HasCrate)
		    {
		        return '0';
		    }



            return 'X';
		}

	}
}

