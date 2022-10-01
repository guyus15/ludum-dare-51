public interface IDamagable
{
    public int MaxHealth { get; set; }
    public int CurrentHealth { get; }

    public void RemoveHealth(int amount);
    public void AddHealth(int amount);

    public void Die();
}