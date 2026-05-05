namespace ElectricTransportApp;

public interface IUsable
{
    void Use(Character target);
}

public interface IDamageable
{
    string Name { get; }
    double Health { get; }
    bool IsAlive { get; }
    void TakeDamage(double amount);
}
