using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float healthPoints = 100f;

        private Animator animator;
        private bool isDead = false;

        public bool IsDead() 
        { 
            return isDead; 
        }

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            print(healthPoints);
            if (healthPoints == 0)
            {
                Death();
            }
        }

        private void Death()
        {
            if (isDead) return;

            isDead = true;
            animator.SetTrigger("die");
        }
    }

}