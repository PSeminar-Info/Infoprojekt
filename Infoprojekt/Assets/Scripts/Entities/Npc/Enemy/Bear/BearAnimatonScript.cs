using UnityEngine;

namespace Entities.Npc.Enemy.Bear.Animatoion
{
    public class BearAnimationScript : MonoBehaviour
    {
        public Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }
    }
}