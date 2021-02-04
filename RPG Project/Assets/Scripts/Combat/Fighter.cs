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
        [SerializeField] float timeBetweenAttacks;          // Used for making sure the animation plays completely and that we don't hit them too fast
        float timeSinceLastAttack = Mathf.Infinity;         // Used for checking how  much time has passed since we last hit an enemy

        [SerializeField] Transform handTransform = null;
        [SerializeField] Weapon defaultWeapon = null;
        Weapon currentWeapon = null;


        private void Start()
        {
            EquipWeapon(defaultWeapon);
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null)
                return;

            if (target.IsDead())
                return;

            if (!GetIsInRange())
            {
                mover.MoveTo(target.transform.position, 0.8f);
            }
            else
            {
                mover.Cancel();
                AttackBehaviour();
            }
        }

        public void EquipWeapon(Weapon weapon)
        {
            currentWeapon = weapon;
            Animator animator = GetComponent<Animator>();
            weapon.Spawn(handTransform, animator);
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

            target.TakeDamage(currentWeapon.GetDamage());
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < currentWeapon.GetRange();
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
            GetComponent<Mover>().Cancel();
        }

        private void StopAttack()
        {
            animator.ResetTrigger("attack");
            animator.SetTrigger("stopAttack");
        }
    }
}