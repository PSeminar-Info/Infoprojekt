using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Entities.Npc.Enemy.Skeleton{

public class SkeletonController : Npc
{
    private float minmove = 10f;
    private float maxmove = 30f;
    private string state = "Idle";
    private NavMeshAgent _nav;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            state = "Pursuit";

        }
    }

    private void Patrolling()
    {
            _nav.SetDestination(RandomNavmeshLocation(minmove, maxmove));


    }

    // Update is called once per frame
    void Update()
    {


        switch (state)
        {
            case "Idle":
                Patrolling();
                break;

                
        }
    }
}
}