using System.Collections;
using UnityEngine;
using UnityEngine.AI;

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
    
    /// <summary>
    /// get a random point on the navmesh within a radius of the current position
    /// </summary>
    public Vector3 RandomNavmeshLocation(float radius) {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out var hit, radius, 1)) {
            finalPosition = hit.position;            
        }
        return finalPosition;
    }
    
    public Vector3 RandomNavmeshLocation(float radius, float minDistance) {
        var randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        var finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out var hit, radius, 1)) {
            finalPosition = hit.position;            
        }
        if (Vector3.Distance(finalPosition, transform.position) < minDistance)
        {
            return RandomNavmeshLocation(radius, minDistance);
        }
        return finalPosition;
    }
    
}