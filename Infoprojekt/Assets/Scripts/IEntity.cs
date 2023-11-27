public interface IEntity
{
    bool IsInvincible { get; set; }
    float Health { get; set; }
    float MaxHealth { get; set; }
    
    void OnSpawn();
    void OnDespawn();
    void Die();
    
    public void Heal(float amount)
    {
        Health += amount;
        if (Health > MaxHealth) Health = MaxHealth;
    }
    
    public void TakeDamage(float amount)
    {
        if (IsInvincible) return;
        
        Health -= amount;
        if (Health <= 0) Die();
    }
    
    
}
