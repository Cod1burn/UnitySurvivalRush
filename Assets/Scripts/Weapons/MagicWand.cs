using Controllers;
using Entities;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace Weapons
{
    public class MagicWand : Weapon
    {

        public MagicWand(GameObject player) : base(player, WeaponType.MagicWand, Resources.Load("Projectile/BlueOrb") as GameObject)
        {
            AttackMultiplier = 1.0f;
            AttackInterval = 1.0f;

            ProjNum = 1;

            ProjSpeed = 10.0f;
            ProjRange = 20.0f;
        }

        public override void Update()
        {
            base.Update();
        }
        
        public override void Attack()
        {
            GameObject p = Controller.ShootProjectile(Projectile, Player.transform.position);
            ProjectileEntity projEntity = p.GetComponent<ProjectileEntity>();
            projEntity.Attack = AttackMultiplier * Entity.Attack;
            projEntity.Speed = ProjSpeed;
            projEntity.Range = ProjRange;
            
            ProjectileController projController = p.GetComponent<ProjectileController>();
            projController.Shoot(Player, Controller.Direction);
            ProjCount++;
            base.Attack();
        }

        public override void GetLevelBonus()
        {
            switch (Level)
            {
                case 2:
                    ProjNum++;
                    break;
                case 3:
                    AttackMultiplier = 1.2f;
                    break;
            }
        }
    }
}