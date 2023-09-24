using System.Xml;
using Controllers;
using Entities;
using ObjectManager;
using Unity.VisualScripting;
using UnityEngine;

namespace Weapons
{
    public sealed class FireWand : Weapon
    {
        private float _maxRange;
        private EnemyManager _manager;
        private float _damageInterval;
        public FireWand(GameObject player) : base(player, WeaponType.FireWand, Resources.Load("Projectile/FireSet") as GameObject)
        {
            _damageInterval = 0.25f;
            AttackInterval = 5.0f;
            MinimumInterval = 0.1f * AttackInterval;
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
            TextAsset xmlData = Resources.Load<TextAsset>("Data/Weapons/MagicWand");

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlData.text);

            XmlNode levelNode = xmlDoc.SelectSingleNode($"weapon/level[@number='{Level}']");

            if (levelNode != null)
            {
                Tooltip = levelNode.SelectSingleNode("description")?.InnerText;
                AttackMultiplier = float.Parse(levelNode.SelectSingleNode("attack")?.InnerText);
                ProjNum = int.Parse(levelNode.SelectSingleNode("projNum")?.InnerText);
                ProjScale = float.Parse(levelNode.SelectSingleNode("scale")?.InnerText);
                ProjDuration = float.Parse(levelNode.SelectSingleNode("duration")?.InnerText);
            }
        }
    }
}