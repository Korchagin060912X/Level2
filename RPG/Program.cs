using RPG;

class Program
{
    static void Main(string[] args)
    {
        Hero hero = new Hero("Герой", 100, 10);
        Monster monster = new Monster("Монстр", 120, 10);

        Console.WriteLine($"{hero.Name} (HP: {hero.Health}) vs {monster.Name} (HP: {monster.Health})");
        Console.WriteLine("Бой начинается!\n");

        while (hero.IsAlive() && monster.IsAlive())
        {
            hero.Attack(monster);
            Console.WriteLine($"{monster.Name} HP: {monster.Health}\n");

            if (!monster.IsAlive())
            {
                break;
            }

            monster.Attack(hero);
            Console.WriteLine($"{hero.Name} HP: {hero.Health}\n");
        }

        if (hero.IsAlive())
        {
            Console.WriteLine($"{hero.Name} победил!");
        }
        else
        {
            Console.WriteLine($"{monster.Name} победил!");
        }
    }
}
