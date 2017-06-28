using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luky_Cviceni
{
    class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player(5, 5, 5, 5, 5);

            Arena arena = new Arena(player);
            arena.AddAbillity(new Abillity("Fireball", "ball of fire", 0D, 1D, 0, 1.5D, 0.5D, AttackEffect.None, 0,-2, -1, 0));
            arena.AddAbillity(new Abillity("Frostball", "ball of ice", 0D, 1D, 0, 1.5D, 0.5D, AttackEffect.Silence,1, -2, -1, 1));
            arena.Start();
            Console.ReadLine();
        }
    }
}
