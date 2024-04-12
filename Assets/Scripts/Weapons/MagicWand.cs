using System.Xml;
using Controllers;
using Entities;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;

namespace Weapons
{
    public sealed class MagicWand : Weapon
    {

        public MagicWand(GameObject player) : base(player, WeaponType.MagicWand, Resources.Load("Projectile/BlueOrb") as GameObject)
        {
            Name = "Magic Wand";
            Description = "Shoot magic orbs in your facing direction.";
            IconSprite = Resources.Load<Sprite>("Icons/Weapons/magicwand");
            
            MaxLevel = 8;
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
            projController.Shoot(Player, Controller.AimDirection);
            ProjCount++;
            base.Attack();
        }

        public override void GetLevelBonus()
        {
            TextAsset xmlData = Resources.Load<TextAsset>("Data/Weapons/magicwand");

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlData.text);

            XmlNode levelNode = xmlDoc.SelectSingleNode($"weapon/level[@lvl='{Level}']");

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
            else
            {
                Debug.Log("Failed to load weapon: magicwand");
            }
        }
    }
}