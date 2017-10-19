using System;
using System.Collections.Generic;
using System.Linq;
using Sokoban.Enums;

namespace Sokoban.Models
{
	public class Maze
	{
		public Dictionary<string, Field> Map;

	    public Maze()
	    {
	        Map = new Dictionary<string, Field>();
	    }

		public bool AllCratesOnDestination()
		{
		    return Map.Where(field => field.Value.Type == FieldType.Box).All(field => ((Destination) field.Value).HasCrate);
		}

        public virtual Field GetNextField(Location coordinate, DirectionType direction)
        {
            return Map[Game.CalculateNewCoordinate(coordinate, direction).ToString()];
        }
	}
}
