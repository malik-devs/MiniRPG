using MiniRPG.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniRPG
{
    public class MainClass
    {
        public static void Main(string[] args)
        {
           Player player = new Player("Hero");
            player.printStats();
        }
    }
}
