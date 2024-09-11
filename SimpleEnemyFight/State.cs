using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleEnemyFight
{
    abstract class State
    {
        public Program program;
        public Enemy player;
        public Enemy enemy;
        private int maxKey;

        public State(Program program, Enemy player, Enemy enemy, int maxKey)
        {
            this.program = program;
            this.player = player;
            this.enemy = enemy;
            this.maxKey = maxKey;
            Print();
        }

        public void Print() 
        {
            Console.WriteLine();
            PrintMenu();
        }

        public abstract void PrintMenu();

        public void CheckForLine(string line)
        {
            if(int.TryParse(line, out int num))
            {
                if (num >= 1 && num <= maxKey)
                {
                    MakeEvent(num);
                    return;
                }
            }
            Print();
        }

        public abstract void MakeEvent(int i);

        public void ChangeState(State state)
        {
            program.state = state;
        }
    }

    class Menu : State
    {
        public Menu(Program program, Enemy player, Enemy enemy) : base(program, player, enemy, 4)
        {

        }

        public override void MakeEvent(int i)
        {
            if(i == 1)
            {
                player.Attack(enemy);
                if(enemy.IsLiving())
                {
                    enemy.Attack(player);
                    player.AddGold(new Random().Next(1, 10));
                    Thread.Sleep(30);
                    enemy.AddGold(new Random().Next(1, 10));
                    Print();
                }

            }
            else if(i == 2)
            {
                ChangeState(new Shop(program, player, enemy));
            }
            else if (i == 3)
            {
                ChangeState(new Inventory(program, player, enemy));
            }
            else if(i == 4)
            {
                Console.WriteLine();
                player.PrintStats();
                Console.WriteLine();
                enemy.PrintStats();
                Print();
            }
        }

        public override void PrintMenu()
        {
            Console.WriteLine("1 - Zaútočit");
            Console.WriteLine("2 - Navštívit obchod");
            Console.WriteLine("3 - Otevřít inventář");
            Console.WriteLine("4 - Zobrazit statistiky");
        }
    }

    class Shop : State
    {
        public Shop(Program program, Enemy player, Enemy enemy) : base(program, player, enemy, 5)
        {

        }

        public override void MakeEvent(int i)
        {
            if (i == 1)
            {
                if(player.gold >= 10)
                {
                    player.AddGold(-10);
                    player.AddPotion(Potion.Small);
                    ChangeState(new Menu(program, player, enemy));
                }
                else
                {
                    Print();
                }
            }
            else if (i == 2)
            {
                if (player.gold >= 30)
                {
                    player.AddGold(-30);
                    player.AddPotion(Potion.Mid);
                    ChangeState(new Menu(program, player, enemy));
                }
                else
                {
                    Print();
                }
            }
            else if (i == 3)
            {
                if (player.gold >= 50)
                {
                    player.AddGold(-50);
                    player.AddPotion(Potion.Large);
                    ChangeState(new Menu(program, player, enemy));
                }
                else
                {
                    Print();
                }
            }
            if (i == 4)
            {
                if (player.gold >= 50)
                {
                    player.AddGold(-50);
                    player.SetWeapon((Weapon)((int)player.currentWeapon + 1));
                    ChangeState(new Menu(program, player, enemy));
                }
                else
                {
                    Print();
                }
            }
            else if (i == 5)
            {
                ChangeState(new Menu(program, player, enemy));
            }
        }

        public override void PrintMenu()
        {
            Console.WriteLine("SHOP");
            Console.WriteLine("1 - Small potion (10)");
            Console.WriteLine("2 - Mid potion (30)");
            Console.WriteLine("3 - Large potion (50)");
            if ((int)player.currentWeapon != 3) Console.WriteLine("4 - Upgrade weapon (50)");
            else Console.WriteLine("4 - MAX LEVEL");
            Console.WriteLine("5 - exit");
        }
    }    

    class Inventory : State
    {
        public Inventory(Program program, Enemy player, Enemy enemy) : base(program, player, enemy, player.potions.Count + 1)
        {

        }

        public override void MakeEvent(int i)
        {
            if(i != (player.potions.Count + 1))
            {
                player.Heal(i - 1);
            }
            ChangeState(new Menu(program, player, enemy));
        }

        public override void PrintMenu()
        {
            player.PrintPotions();
            Console.WriteLine((player.potions.Count + 1) + " - exit");
        }
    }
}
