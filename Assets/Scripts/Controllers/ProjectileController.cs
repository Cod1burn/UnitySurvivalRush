using System;
using Entities;
using UnityEngine;

namespace Controllers
{
    public class ProjectileController : MonoBehaviour
    {
        public GameObject owner;
    
        private Vector2 _direction;
        private ProjectileEntity _pe;

        public bool directional;
    
        // Start is called before the first frame update
        void Awake()
        {
            _direction = Vector2.up;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            Vector2 position = transform.position;
            position += _direction * (Time.deltaTime * _pe.Speed);
            transform.position = position;
        }
    
        public void Shoot(GameObject ownerObject, Vector2 shootDirection)
        {
            owner = ownerObject;
            _pe = GetComponent<ProjectileEntity>();
            gameObject.transform.localScale *= _pe.Scale;
            ChangeDirection(shootDirection);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (_pe.damageOverTime) return;
            if (owner.CompareTag("Player") && other.gameObject.CompareTag("AliveEnemy"))
            {
                EnemyController enemy = other.gameObject.GetComponent<EnemyController>();
                if (enemy is not null)
                {
                    if(_pe.Hit(enemy.Entity))
                        enemy.GetHurt();
                }
            } else if (owner.CompareTag("AliveEnemy") && other.gameObject.CompareTag("Player"))
            {
                PlayerController player = other.gameObject.GetComponent<PlayerController>();
                if (player is not null)
                {
                    if (_pe.Hit(player.Entity))
                        player.GetHurt();
                }
            }
            else return;
            Destroy(gameObject);
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (!_pe.damageOverTime) return;
            if (owner.CompareTag("Player") && other.gameObject.CompareTag("AliveEnemy"))
            {
                EnemyController enemy = other.gameObject.GetComponent<EnemyController>();
                if (enemy is not null)
                {
                    if(_pe.Hit(enemy.Entity))
                        enemy.GetHurt();
                }
            } else if (owner.CompareTag("AliveEnemy") && other.gameObject.CompareTag("Player"))
            {
                PlayerController player = other.gameObject.GetComponent<PlayerController>();
                if (player is not null)
                {
                    if (_pe.Hit(player.Entity))
                        player.GetHurt();
                }
            }
        }

        public void ChangeDirection(Vector2 direction)
        {
            if(directional) transform.Rotate(0, 0, Vector2.Angle(direction, _direction));
            _direction = direction;
        }
    
    }
}
