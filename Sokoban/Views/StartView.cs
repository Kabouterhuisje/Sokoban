using System;

namespace Sokoban.Views
{
	public class StartView : IViewInterface
	{
		public static void PrintView()
        {
            Console.Clear();
		    Console.WriteLine(
		        "┌────────────────────────────────────────────────────┐\n" +
		        "| Welkom bij Sokoban                                 |\n" +
		        "|                                                    |\n" +
		        "| betekenis van de symbolen   |   doel van het spel  |\n" +
		        "|                             |                      |\n" +
		        "| spatie : outerspace         |   duw met de aap     |\n" +
		        "|      █ : muur               |   de banaan(en)      |\n" +
		        "|      · : vloer              |   naar de kisten     |\n" +
		        "|      O : kist               |                      |\n" +
		        "|      0 : kist op bestemming |                      |\n" +
		        "|      x : bestemming         |                      |\n" +
		        "|      @ : heftruck           |                      |\n" +
                "|      ~ : valkuil            |                      |\n" +
		        "└────────────────────────────────────────────────────┘\n" +
		        "\n" +
		        "> Kies een doolhof (1 - 6) | r = reset, s = stop\n"
		    );

		}

	    void IViewInterface.PrintView()
	    {
	        PrintView();
	    }
	}
}

