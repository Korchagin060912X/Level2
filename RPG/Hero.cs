namespace RPG
{
    public class Hero
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int Damage { get; set; }

        public Hero(string name, int health, int damage)
        {
            Name = name;
            Health = health;
            Damage = damage;
        }

        public void Attack(Monster target)
        {
            Console.WriteLine($"{Name} атакует {target.Name} и наносит {Damage} урона");
            target.Health -= Damage;
        }

        public bool IsAlive()
        {
            return Health > 0;
        }
    }
}
