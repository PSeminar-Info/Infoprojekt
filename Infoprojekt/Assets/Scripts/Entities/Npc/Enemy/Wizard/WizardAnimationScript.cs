using System;
using UnityEngine;

namespace Entities.Npc.Enemy.Wizard
{
    public class AnimationScript : MonoBehaviour
    {
        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }
        
        public void SetAnimation(int animationHash)
        {
            _animator.Play(animationHash);
        }
        
        public void StopAnimation()
        {
            _animator.StopPlayback();
        }
        
    }
}
