using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Entities.Npc.Enemy.Wizard
{
    public class WizardAnimationScript : MonoBehaviour
    // this script has a value of -10 in the Unity execution order
    // so it should be ready to give the animator to the controller
    {
        public Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }
    }
}