﻿using System;

namespace Sokoban.Views
{
	public class ResultView : IViewInterface
	{
		public static void PrintView()
		{
            Console.Clear();
            Console.Write(
                "┌──────────┐\n" +
                "| Sokoban  |\n" +
                "└──────────┘\n" +
                "─────────────────────────────────────────────────────────────────────────\n" +
                "WOEHOEE!!!, je hebt gewonnen!\n" +
                "─────────────────────────────────────────────────────────────────────────\n" +
                "> druk op een willekeurige toets om door te gaan..\n"
            );
		}

	    void IViewInterface.PrintView()
	    {
	        PrintView();
	    }
	}
}

