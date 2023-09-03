using Controllers;
using Entities;
using ObjectManager;
using Unity.VisualScripting;
using UnityEngine;

namespace Weapons
{
    public class FireWand : Weapon
    {
        private float _maxRange;
        private EnemyManager _manager;
        private float _damageInterval;
        public FireWand(GameObject player) : base(player, WeaponType.FireWand, Resources.Load("Projectile/FireSet") as GameObject)
        {
            AttackMultiplier = 0.25f;
            AttackInterval = 5.0f;

            _damageInterval = 0.25f;

            ProjDuration = 3.0f;
            ProjScale = 1.2f;
            ProjNum = 1;
            ProjSpeed = 0.0f;

            _maxRange = 10.0f;

            _manager = EnemyManager.Instance;
        }

        public override void Attack()
        {
            _manager.FindEnemies(ProjNum).ForEach(e => FireAt(e.transform.position));
            base.Attack();
        }

        void FireAt(Vector2 position)
        {
            if ((position - (Vector2)(Player.transform.position)).magnitude < _maxRange)
            {
                GameObject p = Controller.ShootProjectile(Projectile, position);
                p.SetActive(true);
                ProjectileEntity projEntity = p.GetComponent<ProjectileEntity>();
                projEntity.Attack = Entity.Attack * AttackMultiplier;
                projEntity.Duration = ProjDuration;
                projEntity.Scale = ProjScale;
                projEntity.DamageInterval = _damageInterval;

                ProjectileController projController = p.GetComponent<ProjectileController>();
                projController.Shoot(Player, Vector2.up);
                ProjCount++;
            }
        }
        
        public override void GetLevelBonus()
        {
            switch (Level)
            {
                case 2:
                    ProjNum++;
                    break;
            }
            base.GetLevelBonus();
        }
    }
}