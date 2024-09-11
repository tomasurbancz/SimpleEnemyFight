using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEnemyFight
{
    class Program
    {
        public State state;
        private Enemy player;
        private Enemy enemy;

        static void Main(string[] args)
        {
            new Program();
        }

        public Program()
        {
            player = new Enemy("TestHráč", 20, 100);
            enemy = new Enemy("TestNepřítel", 20, 100);
            state = new Menu(this, player, enemy);
            while(true)
            {
                if (!(player.IsLiving() && enemy.IsLiving())) break;
                string line = Console.ReadLine();
                state.CheckForLine(line);
            }
            Console.WriteLine("Konec hry");
            Console.ReadLine();
        }
    }
}
