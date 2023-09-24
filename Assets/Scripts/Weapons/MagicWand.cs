using System.Xml;
using Controllers;
using Entities;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace Weapons
{
    public sealed class MagicWand : Weapon
    {

        public MagicWand(GameObject player) : base(player, WeaponType.MagicWand, Resources.Load("Projectile/BlueOrb") as GameObject)
        {
            Description = "Shoot magic orbs in your facing direction.";
            

            GetLevelBonus();
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
            TextAsset xmlData = Resources.Load<TextAsset>("Data/Weapons/MagicWand");

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlData.text);

            XmlNode levelNode = xmlDoc.SelectSingleNode($"weapon/level[@number='{Level}']");

            if (levelNode != null)
            {
                Tooltip = levelNode.SelectSingleNode("description")?.InnerText;
                AttackMultiplier = float.Parse(levelNode.SelectSingleNode("attack")?.InnerText);
                AttackInterval = float.Parse(levelNode.SelectSingleNode("interval")?.InnerText);
                MinimumInterval = 0.1f * AttackInterval;
                ProjNum = int.Parse(levelNode.SelectSingleNode("projNum")?.InnerText);
                ProjSpeed = float.Parse(levelNode.SelectSingleNode("projSpeed")?.InnerText);
                ProjRange = ProjSpeed * 2.0f;
            }
        }
    }
}