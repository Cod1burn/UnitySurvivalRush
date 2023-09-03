using System;
using UnityEngine;

namespace Entities
{
    public class ProjectileEntity : MonoBehaviour
    {
        [NonSerialized] public float Attack;
        [NonSerialized] public float Speed;
    
        /// <summary>
        /// -1.0 for infinite duration
        /// </summary>
        [NonSerialized] public float Duration = -1.0f;
        private float _lifeTimer;
    
        [NonSerialized] public float Scale = 1.0f;

        [NonSerialized] public float DamageInterval;
        private float _damageTimer;
    
        /// <summary>
        /// -1.0 for infinite range
        /// </summary>
        [NonSerialized] public float Range = -1.0f;
    
        [Header("Projectile Properties")]
        public bool piercing = false; 
        public bool aoe = false;
        private bool _isAttack;

        // Start is called before the first frame update
        void Awake()
        {
            _lifeTimer = 0.0f;
            _damageTimer = 0.0f;
            _isAttack = false;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (_isAttack)
            {
                if (aoe) _damageTimer = DamageInterval;
                else if (!piercing)
                {
                    Destroy(gameObject);
                    return;
                }
                _isAttack = false;
            }
        
            _damageTimer -= Time.deltaTime;
            _lifeTimer += Time.deltaTime;
            if (Duration > 0.0f && _lifeTimer > Duration)
            {
                Destroy(gameObject);
                return;
            }

            if (Range > 0.0f && _lifeTimer * Speed > Range)
            {
                Destroy(gameObject);
                return;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (aoe) return;
            if (!other.CompareTag("AliveEnemy")) return;
            EnemyController enemy = other.gameObject.GetComponent<EnemyController>();
            if (enemy is not null)
            {
                Hit(enemy.Entity);
                enemy.GetHurt();
            }
            if (!piercing) Destroy(gameObject);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (!aoe) return;
            if (_damageTimer <= 0.0f)
            {
                if (!other.CompareTag("AliveEnemy")) return;
                EnemyController enemy = other.gameObject.GetComponent<EnemyController>();
                if (enemy is not null)
                {
                    Hit(enemy.Entity);
                    enemy.GetHurt();
                }
            }
        }

        void Hit(BaseEntity entity)
        {
            entity.TakeDamage(Attack);
            _isAttack = true;
        }
    
    }
}
