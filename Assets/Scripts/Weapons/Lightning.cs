using System;
using System.Xml;
using Auras;
using Controllers;
using Entities;
using UnityEngine;

namespace Weapons
{
    public class Lightning: Weapon
    {
        private float _slowAuraAmount;
        private SpeedAura _auraTemplate;
        private int projDirections;

        public Lightning(GameObject player) : base(player, WeaponType.Lightning, Resources.Load("Projectile/Lightning") as GameObject)
        {
            Name = "Lightning";
            Description = "Summon flying lightnings to attack enemies in front of you.";
            IconSprite = Resources.Load<Sprite>("Icons/Weapons/lightning");
            ProjDuration = 2.0f;
            ProjNum = 1;
            MaxLevel = 8;
            
            GetLevelBonus();
        }

        public override void Attack()
        {
            Vector2 direction = Controller.Direction;
            for (int i = 0; i < projDirections; i++)
            {
                SummonLightning(direction);
                // Rotate the direction vector by pi/2.
                float radians = 0.5f * Mathf.PI;
                float sin = Mathf.Sin(radians);
                float cos = Mathf.Cos(radians);
                direction = new Vector2(cos * direction.x - sin * direction.y, sin * direction.x + cos * direction.y);
            }
            ProjCount++;
            base.Attack();
        }

        void SummonLightning(Vector2 direction)
        {
            GameObject p = Controller.ShootProjectile(Projectile, Player.transform.position + (Vector3)direction * 0.8f);
            p.SetActive(true);
            ProjectileEntity projEntity = p.GetComponent<ProjectileEntity>();
            projEntity.Attack = Entity.Attack * AttackMultiplier;
            projEntity.Duration = ProjDuration;
            projEntity.Speed = ProjSpeed;
            projEntity.AuraOnHit = _auraTemplate;

            ProjectileController projController = p.GetComponent<ProjectileController>();
            projController.Shoot(Player, direction);
        }

        public override void GetLevelBonus()
        {
            TextAsset xmlData = Resources.Load<TextAsset>("Data/Weapons/lightning");

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlData.text);

            XmlNode levelNode = xmlDoc.SelectSingleNode($"weapon/level[@lvl='{Level}']");

            if (levelNode != null)
            {
                Tooltip = levelNode.SelectSingleNode("description")?.InnerText;
                AttackMultiplier = float.Parse(levelNode.SelectSingleNode("attack")?.InnerText);
                AttackInterval = float.Parse(levelNode.SelectSingleNode("interval")?.InnerText);
                MinimumInterval = 0.15f * AttackInterval;
                projDirections = int.Parse(levelNode.SelectSingleNode("direction")?.InnerText);
                ProjSpeed = float.Parse(levelNode.SelectSingleNode("projSpeed")?.InnerText);
                _slowAuraAmount = float.Parse(levelNode.SelectSingleNode("slow")?.InnerText);
                if (_slowAuraAmount > 0.0f)
                    _auraTemplate = new SpeedAura("Lightning slow", 2.0f, -1.0f * _slowAuraAmount);
                else _auraTemplate = null;
                ProjRange = ProjSpeed * ProjDuration;
            }
            else
            {
                Debug.Log("Failed to load weapon: lightning");
            }
        }
    }
}