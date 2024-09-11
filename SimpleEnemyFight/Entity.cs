using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEnemyFight
{
    class Entity
    {
        public string name;

        public Entity(string name)
        {
            this.name = name;
        }
    }

    enum Weapon
    {
        Hand = 0,
        Dagger = 1,
        Sword = 2,
        Spear = 3
    }

    enum Potion
    {
        Small = 5,
        Mid = 7,
        Large = 12
    }

    class Enemy : Entity
    {
        private int baseDamage;
        private int HP;
        public Weapon currentWeapon;
        public List<Potion> potions;
        public int gold;

        public Enemy(string name, int baseDamage, int HP) : base(name)
        {
            this.baseDamage = baseDamage;
            this.HP = HP;
            currentWeapon = Weapon.Hand;
            potions = new List<Potion>();
            gold = 0;
        }

        private String GetPotionName(int healAmount)
        {
            if (healAmount == 12) return "Large";
            if (healAmount == 7) return "Mid";
            return "Small";
        }

        private String GetWeaponName(int damage)
        {
            if (damage == 0) return "hand";
            if (damage == 1) return "Dagger";
            if (damage == 2) return "Sword";
            return "Spear";
        }

        public void PrintPotions()
        {
            if(!potions.Any())
            {
                Console.WriteLine("Tvůj inventář je prázdný");
            }
            else
            {
                Console.WriteLine("Tvoje potiony v inventáři:");
                for (int i = 0; i < potions.Count; i++)
                {
                    Console.WriteLine((i + 1) + " - " + GetPotionName((int)potions[i]));
                }
            }
        }

        public void AddGold(int gold)
        {
            this.gold += gold;
            if (gold >= 0) Console.WriteLine("Hráč " + name + " obdržel " + gold + " goldů");
            else Console.WriteLine("Hráč " + name + " ztratil " + (-gold) + " goldů");
        }

        public void PrintStats()
        {
            Console.WriteLine("Statistiky hráče " + name);
            Console.WriteLine("\nBase Damage: " + baseDamage);
            Console.WriteLine("HP: " + HP);
            Console.WriteLine("Weapon: " + GetWeaponName((int) currentWeapon));
            Console.WriteLine("Potions in inventory: " + potions.Count);
            Console.WriteLine("Gold: " + gold);
        }

        public void Attack(Enemy enemy)
        {
            int damage = baseDamage + ((int)currentWeapon);
            enemy.HP -= damage;
            if(!enemy.IsLiving())
            {
                Console.WriteLine("Hráč " + name + " zaútočil na hráče " + enemy.name + " a porazil ho.");
                return;
            }
            Console.WriteLine("Hráč " + name + " zaútočil na hráče " + enemy.name + ", kterému aktuálně zbývá " + enemy.HP + " hp");
        }

        public bool IsLiving()
        {
            HP = Math.Max(HP, 0);
            return HP != 0;
        }

        public void AddPotion(Potion potion)
        {
            potions.Add(potion);
            Console.WriteLine("Hráč " + name + " získal potion " + GetPotionName((int) potion));
        }

        public void Heal(int index)
        {
            Potion potion = potions[index];
            potions.RemoveAt(index);
            HP += (int)potion;
            Console.WriteLine("Hráč " + name + " vypil " + GetPotionName((int)potion) + " a aktuálně má " + HP + " zdraví");
        }

        internal void SetWeapon(Weapon weapon)
        {
            this.currentWeapon = weapon;
        }
    }
}
