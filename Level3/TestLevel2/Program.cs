namespace ElectricTransportApp;

internal class Program
{
    static void Main()
    {
        var logger = new DeathLogger();

        var mina = new Warrior();
        var mike = new Mage();
        var orc1 = new Monster("Orc-1");
        var orc2 = new Monster("Orc-2");

        logger.Subscribe(mina);
        logger.Subscribe(mike);
        logger.Subscribe(orc1);
        logger.Subscribe(orc2);

        mina.AddItem(new Sword("Стальной меч", 7));
        mina.AddItem(new HealthPotion("Малое зелье лечения", 35));
        mina.AddItem(new Bomb("Огненная бомба", 18));

        mike.AddItem(new HealthPotion("Большое зелье лечения", 45));
        mike.AddItem(new Bomb("Ледяная бомба", 20));

        Console.WriteLine("--- Бой начался ---\n");
        Console.WriteLine($"=> Первый бой: {mina.Name} против {orc1.Name}");
        Battle(mina, orc1);

        Console.WriteLine("\n" + new string('-', 30) + "\n");

        Console.WriteLine($"=> Второй бой: {mike.Name} против {orc2.Name}");
        Battle(mike, orc2);

        Console.WriteLine("\n--- ПРИКЛЮЧЕНИЕ ЗАВЕРШЕНО ---");
        Console.WriteLine($"Итог: {mina.Name} (Lvl {mina.Level}), {mike.Name} (Lvl {mike.Level})");
    }

    static void Battle(Character hero, Character enemy)
    {
        hero.ShowInventory();

        while (hero.IsAlive && enemy.IsAlive)
        {
            UseBestItem(hero, enemy);
            hero.Attack(enemy);

            if (!enemy.IsAlive)
            {
                Console.WriteLine($"{enemy.Name} повержен!");
                hero.AddXp(100);
                break;
            }

            enemy.Attack(hero);
            Console.WriteLine($"Статус: {hero.Name} ({hero.Health} HP) | {enemy.Name} ({enemy.Health} HP)");
            Thread.Sleep(300);
        }
    }

    static void UseBestItem(Character hero, Character enemy)
    {
        if (hero.EquippedItem is null && hero.Inventory.Any(i => i is Sword))
        {
            var swordIndex = hero.Inventory.FindIndex(i => i is Sword);
            hero.UseItem(swordIndex, hero);
            return;
        }

        if (hero.Health <= 45 && hero.Inventory.Any(i => i is HealthPotion))
        {
            var potionIndex = hero.Inventory.FindIndex(i => i is HealthPotion);
            hero.UseItem(potionIndex, hero);
            return;
        }

        if (enemy.Health <= 35 && hero.Inventory.Any(i => i is Bomb))
        {
            var bombIndex = hero.Inventory.FindIndex(i => i is Bomb);
            hero.UseItem(bombIndex, enemy);
        }
    }
}
