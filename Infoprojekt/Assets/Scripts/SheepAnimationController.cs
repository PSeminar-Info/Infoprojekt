using System;
using UnityEngine;
using UnityEngine.AI;


public class SheepAnimationController : MonoBehaviour
{
    public Animator animator;
    private NavMeshAgent _navMeshAgent;
    private String _currentAnimation;
    public string walkForwardAnimation = "walk_forward";
    public string walkBackwardAnimation = "walk_backwards";
    public string runForwardAnimation = "run_forward";
    public string turn90LAnimation = "turn_90_L";
    public string turn90RAnimation = "turn_90_R";
    public string trotAnimation = "trot_forward";
    public string sittostandAnimation = "sit_to_stand";
    public string standtositAnimation = "stand_to_sit";
    public string idleAnimation = "idle";

    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        
    }

    void Update()
    {
        // if the sheep is moving, play the walk animation
        if (_navMeshAgent.velocity.magnitude > 0f && _currentAnimation != walkForwardAnimation)
        {
            animator.Play(walkForwardAnimation);
            _currentAnimation = walkForwardAnimation;
            Debug.Log("Playing walk animation");
        } 
        else if (_navMeshAgent.velocity.magnitude == 0 && _currentAnimation != idleAnimation)
        {
            animator.Play(idleAnimation);
            _currentAnimation = idleAnimation;
            Debug.Log("Stopping walk animation");
        }
    }
}