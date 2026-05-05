namespace ElectricTransportApp;

public abstract class Item : IUsable
{
    public string Name { get; }
    public virtual bool ConsumeOnUse => true;

    protected Item(string name)
    {
        Name = name;
    }

    public abstract void Use(Character target);
}

public sealed class HealthPotion : Item
{
    readonly double _healAmount;

    public HealthPotion(string name, double healAmount) : base(name)
    {
        _healAmount = healAmount;
    }

    public override void Use(Character target)
    {
        Console.WriteLine($"{target.Name} использует {Name}");
        target.Heal(_healAmount);
    }
}

public sealed class Sword : Item
{
    public double DamageBonus { get; }

    public Sword(string name, double damageBonus) : base(name)
    {
        DamageBonus = damageBonus;
    }

    public override void Use(Character target)
    {
        Console.WriteLine($"{target.Name} использует {Name}");
        target.EquipSword(this);
    }
}

public sealed class Bomb : Item
{
    readonly double _areaDamage;

    public Bomb(string name, double areaDamage) : base(name)
    {
        _areaDamage = areaDamage;
    }

    public override void Use(Character target)
    {
        Console.WriteLine($"{target.Name} получает урон от {Name}");
        target.TakeDamage(_areaDamage);
    }

    public void UseArea(IEnumerable<IDamageable> targets)
    {
        foreach (var target in targets.Where(t => t.IsAlive))
        {
            target.TakeDamage(_areaDamage);
        }
    }
}
