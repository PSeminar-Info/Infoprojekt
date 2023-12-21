using UnityEngine.AI;


public class Sheep : Npc
{
    NavMeshAgent _agent;
    public float maxTargetDistance;
    // TODO: Move randomly, move to Player sometimes to "say hello", eat grass
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        InvokeRepeating("WalkToRandomLocation", 0, 5);
    }

    private void WalkToRandomLocation()
    {
        _agent.SetDestination(RandomNavmeshLocation(maxTargetDistance));
    }
    
}