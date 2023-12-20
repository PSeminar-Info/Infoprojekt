using System.Collections;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public abstract class Npc : MonoBehaviour, IEntity
{
    private readonly ArrayList _inventory = new();
    private int InventorySize { get; set; }
    
    public bool IsInvincible { get; set; }
    public float Health { get; set; }
    public float MaxHealth { get; set; }

    public void OnSpawn()
    {
        Health = MaxHealth;
        // TODO: spawn animation
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

    // TODO: implement Item class
    public bool HasItem(object item)
    {
        return _inventory.Contains(item);
    }

    public void AddItem(object item)
    {
        if (_inventory.Count >= InventorySize) return;
        _inventory.Add(item);
    }

    public void RemoveItem(object item)
    {
        _inventory.Remove(item);
    }
    
    public void SetInventorySize(int size)
    {
        InventorySize = size;
        if (_inventory.Count > size) _inventory.RemoveRange(size, _inventory.Count - size);
    }

    // Moves NPC by x
    public void MoveBy(float deltaX, float deltaZ)
    {
        transform.position = new Vector3(transform.position.x + deltaX * Time.deltaTime, transform.position.y, transform.position.z + deltaZ * Time.deltaTime);
    }

    // Moves NPC by x
    public void MoveBy(float deltaX, float deltaY, float deltaZ)
    {
        transform.position = new Vector3(transform.position.x + deltaX * Time.deltaTime, transform.position.y + deltaY * Time.deltaTime, transform.position.z + deltaZ * Time.deltaTime);
    }

    // Moves NPC to x
    public void MoveTo(float x, float y, float z)
    {
        transform.position = new Vector3(x, y, z);
    }
}