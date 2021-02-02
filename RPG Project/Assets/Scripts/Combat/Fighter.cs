using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [Header("Scripts")]
        [SerializeField] Mover mover;                       // Used for getting the mover script
        [SerializeField] ActionScheduler actionScheduler;   // Used for getting the action scheduler script
        Health target;                                      // We get the health script from the enemy when we do damage

        [Header("Components")]
        [SerializeField] Animator animator;                 // Used for getting the animator

        [Header("Floats")]
        [SerializeField] float weaponRange;                 // Used for getting the current weapon range
        [SerializeField] float timeBetweenAttacks;          // Used for making sure the animation plays completely and that we don't hit them too fast
        [SerializeField] float weaponDamage;                // Used for dealing damage to the enemy
        float timeSinceLastAttack = Mathf.Infinity;         // Used for checking how  much time has passed since we last hit an enemy


        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null)
                return;

            if (target.IsDead())
                return;

            if (!GetIsInRange())
            {
                mover.MoveTo(target.transform.position);
            }
            else
            {
                mover.Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if (timeSinceLastAttack >= timeBetweenAttacks)
            {
                // this triggers the Hit() event
                TriggerAttack();
                timeSinceLastAttack = 0f;
            }
        }

        private void TriggerAttack()
        {
            animator.ResetTrigger("stopAttack");
            animator.SetTrigger("attack");
        }

        // Animation event
        void Hit()
        {
            if (target == null)
                return;

            target.TakeDamage(weaponDamage);
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null)
                return false;
            
            Health enemyHealth = combatTarget.GetComponent<Health>();
            return enemyHealth != null && !enemyHealth.IsDead();
        }

        public void Attack(GameObject combatTarget)
        {
                target = combatTarget.GetComponent<Health>();
                actionScheduler.StartAction(this);
        }

        public void Cancel()
        {
            StopAttack();
            target = null;
        }

        private void StopAttack()
        {
            animator.ResetTrigger("attack");
            animator.SetTrigger("stopAttack");
        }
    }
}