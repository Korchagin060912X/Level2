using System;

namespace ElectricTransportApp
{
    public abstract class Character
    {
        public string Name { get; protected set; }
        public double Health { get; protected set; }
        public double Damage { get; protected set; } // Базовый урон
        protected Random rnd = new Random();

        public Character(string name, double health, double damage)
        {
            Name = name;
            Health = health;
            Damage = damage;
        }

        // Метод для получения урона
        public void TakeDamage(double amount)
        {
            Health -= amount;
            if (Health < 0) Health = 0;
        }

        public abstract void Attack(Character enemy);

        public void ShowStatus() => Console.WriteLine($"{Name}: {Health}% HP");
    }

    public class Monster : Character
    {
        // Woosh: 50 HP, базовый урон 5
        public Monster() : base("orc", 90.0, 25.0) { }

        public override void Attack(Character enemy)
        {
            double currentDamage = Damage;

            enemy.TakeDamage(currentDamage);
        }
    }

    public class Hero : Character
    {
        // Minako: 60 HP, базовый урон 15
        public Hero() : base("mina", 100.0, 10.0) { }

        public override void Attack(Character enemy)
        {

            Console.WriteLine($"{Name} наносит мощный удар: {Damage} урона!");
            enemy.TakeDamage(Damage);
        }
    }

    class Program
    {
        static void Main()
        {
            Monster orc = new Monster();
            Hero mina = new Hero();
            int round = 1;
            bool gameOver = false;

            Console.WriteLine("--- БИТВА НАЧАЛАСЬ ---");

            while (!gameOver)
            {
                Console.WriteLine($"\n--- Раунд {round} ---");

                // Woosh атакует Minako
                orc.Attack(mina);
                mina.ShowStatus();

                if (mina.Health <= 0)
                {
                    Console.WriteLine($"\n{mina.Name} уничтожен! {orc.Name} победил!");
                    gameOver = true;
                    break;
                }

                Console.WriteLine("---------------");

                // Minako атакует Woosh
                mina.Attack(orc);
                orc.ShowStatus();

                if (orc.Health <= 0)
                {
                    Console.WriteLine($"\n{orc.Name} уничтожен! {mina.Name} победил!");
                    gameOver = true;
                }

                round++;
                System.Threading.Thread.Sleep(1000);
            }

            Console.WriteLine("\nБой завершен. Нажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}