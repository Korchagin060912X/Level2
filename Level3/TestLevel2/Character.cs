namespace ElectricTransportApp;

public abstract class Character : IDamageable
{
    protected static readonly Random Rng = new();

    public string Name { get; protected set; }
    public double Health { get; protected set; }
    public double BaseDamage { get; protected set; }
    public int Level { get; set; } = 1;
    public int Experience { get; set; }
    public List<Item> Inventory { get; } = [];
    public Sword? EquippedItem { get; private set; }
    public bool IsAlive => Health > 0;

    public event Action<Character>? OnDeath;

    protected Character(string name, double health, double baseDamage)
    {
        Name = name;
        Health = health;
        BaseDamage = baseDamage;
    }

    public virtual void Attack(IDamageable target)
    {
        var damage = GetAttackDamage();
        Console.WriteLine($"{Name} атакует {target.Name} на {damage} урона");
        target.TakeDamage(damage);
        AfterAttack(target);
    }

    protected virtual double GetAttackDamage() => BaseDamage + (EquippedItem?.DamageBonus ?? 0);

    protected virtual void AfterAttack(IDamageable target)
    {
    }

    public void TakeDamage(double amount)
    {
        if (!IsAlive)
        {
            return;
        }

        Health -= amount;
        if (Health <= 0)
        {
            Health = 0;
            OnDeath?.Invoke(this);
        }
    }

    public void Heal(double amount)
    {
        if (!IsAlive)
        {
            return;
        }

        Health += amount;
        Console.WriteLine($"{Name} восстанавливает {amount} HP");
    }

    public void AddXp(int xp)
    {
        Experience += xp;
        while (Experience >= 100)
        {
            Level++;
            Experience -= 100;
            Console.WriteLine($"{Name} повышает уровень до {Level}");
        }
    }

    public void AddItem(Item item)
    {
        Inventory.Add(item);
        Console.WriteLine($"{Name} получает предмет: {item.Name}");
    }

    public bool RemoveItem(Item item) => Inventory.Remove(item);

    public void ShowInventory()
    {
        Console.WriteLine($"Инвентарь {Name}:");
        if (Inventory.Count == 0)
        {
            Console.WriteLine("  пусто");
            return;
        }

        for (var i = 0; i < Inventory.Count; i++)
        {
            Console.WriteLine($"  [{i}] {Inventory[i].Name}");
        }
    }

    public bool UseItem(int index, Character target)
    {
        if (index < 0 || index >= Inventory.Count)
        {
            return false;
        }

        var item = Inventory[index];
        item.Use(target);

        if (item.ConsumeOnUse)
        {
            RemoveItem(item);
        }

        return true;
    }

    public void EquipSword(Sword sword)
    {
        EquippedItem = sword;
        Console.WriteLine($"{Name} экипировал {sword.Name}, бонус урона +{sword.DamageBonus}");
    }
}

public sealed class Warrior : Character
{
    public Warrior() : base("Mina", 125, 20)
    {
    }

    protected override double GetAttackDamage()
    {
        var damage = base.GetAttackDamage();
        return Rng.Next(0, 100) < 30 ? damage * 2 : damage;
    }
}

public sealed class Mage : Character
{
    public Mage() : base("Mike", 85, 24)
    {
    }

    protected override void AfterAttack(IDamageable target)
    {
        Heal(5);
    }
}

public sealed class Monster : Character
{
    public Monster(string name) : base(name, 105, 15)
    {
    }
}

public sealed class DeathLogger
{
    public void Subscribe(Character character)
    {
        character.OnDeath += HandleDeath;
    }

    void HandleDeath(Character character)
    {
        Console.WriteLine($"[LOG] {character.Name} погиб");
    }
}
