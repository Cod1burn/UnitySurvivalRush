using UnityEngine;

namespace Controllers
{
    public class MeleeEnemyController : EnemyController
    {
        // Start is called before the first frame update
        new void Awake()
        {
            base.Awake();
            IsMelee = true;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            base.FixedUpdate();
            ChaseTarget();
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!CompareTag("AliveEnemy")) return;
            if (other.gameObject.GetComponent<PlayerController>() != null)
            {
                if (AttackTimer <= 0.0f) AttackAnimation(other.gameObject, "Attack");
            }
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (!CompareTag("AliveEnemy")) return;
            if (other.gameObject.GetComponent<PlayerController>() != null)
            {
                if (AttackTimer <= 0.0f) AttackAnimation(other.gameObject, "Attack");
            }
        }
    }
}
