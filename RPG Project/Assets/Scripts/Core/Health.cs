using UnityEngine;
using RPG.Saving;
using TMPro;
using UnityEngine.AI;

namespace RPG.Core
{
    public class Health : MonoBehaviour, ISaveable
    {
        [Header("Components")]
        [SerializeField] Animator animator;
        [SerializeField] TextMeshProUGUI healthText;

        [Header("Floats")]
        [SerializeField] float healthPoints;
        [SerializeField] float maxHealth;
        [SerializeField] float timer;

        bool isDead = false;

        public bool IsDead()
        {
            return isDead;
        }

        private void Start()
        {
            healthText.text = healthPoints.ToString();
        }

        private void Update()
        {
            if (healthPoints < maxHealth)
            {
                timer += Time.deltaTime;
            }
            else
            {
                timer = 0;
            }

            if (!IsDead())
            {
                if (healthPoints < maxHealth && timer > 10f)
                {
                    healthPoints++;
                    timer = 0;
                    healthText.text = healthPoints.ToString();
                }
            }
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
            GetComponent<NavMeshAgent>().enabled = false;
            GetComponent<ActionScheduler>().CancelCurrentAction();
            healthText.enabled = false;
        }

        private void Reborn()
        {
            isDead = false;
            healthText.enabled = true;
            healthText.text = healthPoints.ToString();
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
            if (healthPoints > 0 && !IsDead())
            {
                Reborn();
            }
            healthText.text = healthPoints.ToString();
        }
    }
}
