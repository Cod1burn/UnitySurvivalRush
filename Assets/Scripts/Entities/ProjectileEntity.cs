using System;
using Auras;
using UnityEngine;
using UnityEngine.Serialization;

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
        public bool damageOverTime = false;
        /// <summary>
        /// If a projectile is not piercing, it will be destroyed after collide with first object.
        /// If a projectile is damageOverTime, it will attack enemies by the time they stay in the collider.
        /// </summary>
        private bool _isAttack;

        [NonSerialized] public Aura AuraOnHit;

        // Start is called before the first frame update
        void Awake()
        {
            _lifeTimer = 0.0f;
            _damageTimer = 0.0f;
            _isAttack = false;
            AuraOnHit = null;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (_isAttack)
            {
                if (damageOverTime) _damageTimer = DamageInterval;
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
            if (damageOverTime) return;
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
            if (!damageOverTime) return;
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
            if (AuraOnHit != null) AuraOnHit.Copy().ApplyTo(entity);
            _isAttack = true;
        }
    
    }
}
