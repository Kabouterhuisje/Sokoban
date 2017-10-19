using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban.Views
{
    class GameOverView : IViewInterface
    {
        public static void PrintView()
        {
            Console.Clear();
            Console.Write(
                "┌──────────┐\n" +
                "| Sokoban  |\n" +
                "└──────────┘\n" +
                "─────────────────────────────────────────────────────────────────────────\n" +
                "JE HEBT HELAAS VERLOREN!\n" +
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
