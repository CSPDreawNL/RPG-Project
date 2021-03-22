using UnityEngine;
using RPG.Saving;
using TMPro;

namespace RPG.Core
{
    public class Health : MonoBehaviour, ISaveable
    {
        [Header("Components")]
        [SerializeField] Animator animator;
        [SerializeField] TextMeshProUGUI healthText;

        [Header("Floats")]
        [SerializeField] float healthPoints;

        bool isDead = false;

        public bool IsDead()
        {
            return isDead;
        }

        private void Start()
        {
            healthText.text = healthPoints.ToString();
        }

        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0f);
            if (healthPoints == 0f)
            {
                Die();
            }
            healthText.text = healthPoints.ToString();
        }

        private void Die()
        {
            if (isDead)
                return;

            isDead = true;
            animator.SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
            Destroy(healthText);
        }

        public object CaptureState()
        {
            return healthPoints;
        }

        public void RestoreState(object state)
        {
            healthPoints = (float)state;
            if (healthPoints <= 0)
            {
                Die();
            }
            healthText.text = healthPoints.ToString();
        }
    }
}
