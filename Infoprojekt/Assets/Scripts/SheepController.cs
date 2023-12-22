using UnityEngine;
using UnityEngine.AI;


public class SheepController : Npc
{
    // Movement
    private NavMeshAgent _agent;
    private Animator _animator;
    
    public float maxTargetDistance;
    public float minTargetDistance;
    public float cooldown;
    private float _lastActionTime;
    private Vector3 _destination;
    
    // Animations: walk_forward, walk_backwards, run_forward, turn_90_L, turn_90_R, trot_forward, sit_to_stand, stand_to_sit, idle
    private string _currentAnimation;
    
    
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        InvokeRepeating("StartWalking", 0, 5);
    }

    private void Update()
    {
        if (Time.time - _lastActionTime > cooldown)
        {
            StartWalking();
            _lastActionTime = Time.time;
        }
        if(_agent.destination == transform.position && _currentAnimation != "idle")
        {
            SetAnimation("idle");
        }
    }

    // rotate to face the target location, then walk to it
    private void WalkToRandomLocation()
    {
        _destination = RandomNavmeshLocation(maxTargetDistance, minTargetDistance);
        var targetRotation = Quaternion.LookRotation(_destination - transform.position);
        
        if (targetRotation.eulerAngles.y is > 45 and < 180 && _currentAnimation != "turn_90_R")
        {
            SetAnimation("turn_90_R");
        }
        else if(targetRotation.eulerAngles.y is > 180 and < 315 && _currentAnimation != "turn_90_L")
        {
            SetAnimation("turn_90_L");
        }
        Invoke(nameof(StartWalking), 1);
    }
    
    private void StartWalking()
    {
        // remove when using WalkToRandomLocation()!!
        _destination = RandomNavmeshLocation(maxTargetDistance);
        
        _agent.SetDestination(_destination);
        SetAnimation("walk_forward");
    }
    
    private void SetAnimation(string animationName)
    {
        if (_currentAnimation == animationName) return;
        _animator.Play(animationName);
        _currentAnimation = animationName;
    }
    
}