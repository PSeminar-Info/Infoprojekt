using System.Collections;
using UnityEngine;

public abstract class AmbientObject : MonoBehaviour, IEntity
{
    public bool IsInvincible { get; set; }
    public float Health { get; set; }
    public float MaxHealth { get; set; }
    public bool IsDestroyable { get; set; }
    private ArrayList _inventory = new();
    
    public void OnSpawn()
    {
        // TODO: spawn behaviour
    }

    public void OnDespawn()
    {
        // TODO: despawn behaviour
    }

    public void Die()
    {
        Health = 0;
        // TODO: death animation
    }
}
