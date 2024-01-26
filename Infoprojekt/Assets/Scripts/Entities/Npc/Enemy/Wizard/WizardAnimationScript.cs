using UnityEngine;

namespace Entities.Npc.Enemy.Wizard
{
    public class WizardAnimationScript : MonoBehaviour
    {
        public Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }
    }
}