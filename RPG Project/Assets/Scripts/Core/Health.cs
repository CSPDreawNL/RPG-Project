using UnityEngine;

namespace RPG.Core
{
    public class Health : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] Animator animator;

        [Header("Floats")]
        [SerializeField] float healthPoints;

        bool isDead = false;

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0f);
            if (healthPoints == 0f)
            {
                Die();
            }
        }

        private void Die()
        {
            if (isDead)
                return;

            isDead = true;
            animator.SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }
    }
}
