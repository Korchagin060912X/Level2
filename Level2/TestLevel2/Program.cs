using System;

namespace ElectricTransportApp
{
    public abstract class Character
    {
        public string Name { get; protected set; }
        public double Health { get; protected set; }
        public double Damage { get; protected set; }
        public int Level { get; set; } = 1;
        public int Experience { get; set; } = 0;

        // Общий генератор случайных чисел
        protected static Random rnd = new Random();

        public Character(string name, double health, double damage)
        {
            Name = name;
            Health = health;
            Damage = damage;
        }

        public void TakeDamage(double amount)
        {
            Health -= amount;
            if (Health < 0) Health = 0;
        }

        public void AddXp(int xp)
        {
            Experience += xp;
            if (Experience >= 100)
            {
                Level++;
                Experience -= 100;
                Console.WriteLine($"\n--- {Name} ПОВЫСИЛ УРОВЕНЬ ДО {Level}! ---");
            }
        }

        public abstract void Attack(Character enemy);
    }

    public class Warrior : Character
    {
        public Warrior() : base("Mina", 125.0, 20.0) { }
        public override void Attack(Character enemy)
        {
            // Используем общий rnd
            double dmg = (rnd.Next(0, 100) < 30) ? Damage * 2 : Damage;
            Console.WriteLine($"{Name} бьет на {dmg} урона!");
            enemy.TakeDamage(dmg);
        }
    }

    public class Mage : Character
    {
        public Mage() : base("Mike", 80.0, 25.0) { }
        public override void Attack(Character enemy)
        {
            Console.WriteLine($"{Name} стреляет магией на {Damage} урона");
            enemy.TakeDamage(Damage);
            Health += 5; // Небольшой хил
            Console.WriteLine($"{Name} подлечился на 5 HP");
        }
    }

    public class Monster : Character
    {
        public Monster(string name) : base(name, 105.0, 15.0) { }
        public override void Attack(Character enemy)
        {
            Console.WriteLine($"{Name} атакует {enemy.Name} на {Damage}!");
            enemy.TakeDamage(Damage);
        }
    }

    class Program
    {
        static void Main()
        {
            Warrior mina = new Warrior();
            Mage mike = new Mage();

            // Создаем монстров
            Monster orc1 = new Monster("Orc-1");
            Monster orc2 = new Monster("Orc-2");

            Console.WriteLine("--- Бой начался ---\n");

            // 1. Бьется Мина
            Console.WriteLine($"=> Первый бой: {mina.Name} против {orc1.Name}");
            Battle(mina, orc1);

            Console.WriteLine("\n" + new string('-', 30) + "\n");

            // 2. Бьется Майк
            if (orc2.Health > 0)
            {
                Console.WriteLine($"=> Второй бой: {mike.Name} против {orc2.Name}");
                Battle(mike, orc2);
            }

            Console.WriteLine("\n--- ПРИКЛЮЧЕНИЕ ЗАВЕРШЕНО ---");
            Console.WriteLine($"Итог: {mina.Name} (Lvl {mina.Level}), {mike.Name} (Lvl {mike.Level})");
            Console.ReadKey();
        }

        // Метод ОБЯЗАТЕЛЬНО должен быть static, чтобы Main мог его вызвать
        static void Battle(Character hero, Character enemy)
        {
            while (hero.Health > 0 && enemy.Health > 0)
            {
                hero.Attack(enemy);
                if (enemy.Health <= 0)
                {
                    Console.WriteLine($"{enemy.Name} повержен!");
                    hero.AddXp(100);
                    break;
                }

                enemy.Attack(hero);
                if (hero.Health <= 0)
                {
                    Console.WriteLine($"{hero.Name} проиграл...");
                }

                Console.WriteLine($"Статус: {hero.Name} ({hero.Health} HP) | {enemy.Name} ({enemy.Health} HP)");
                System.Threading.Thread.Sleep(500);
            }
        }
    }
}
