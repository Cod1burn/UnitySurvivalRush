using System;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace Entities
{
    [XmlRoot(ElementName = "enemy")]
    public class EnemyEntity: BaseEntity
    {
        [XmlElement("name")]
        public string Name;
        
        [XmlElement("speed")]
        public float MoveSpeed
        {
            get { return BaseMoveSpeed * MoveSpeedMultiplier; }
            set { BaseMoveSpeed = value; }
        }
        
        [XmlElement("maxHealth")]
        public float MaxHealth
        {
            get { return BaseMaxHealth * HealthMultiplier; }
            set { BaseMaxHealth = value; }
        }
        
        [XmlElement("attack")]
        public float Attack
        {
            get { return BaseAttack * AttackMultiplier; }
            set { BaseAttack = value; }
        }
    
        [XmlElement("difficulty")]
        public int Difficulty;
    
        [XmlElement("atkItv")]
        public float AtttackInterval;

        [XmlElement("isMelee")]
        public bool IsMelee;

        private EnemyController _controller;
    
        // For enemy objects.
        public EnemyEntity(EnemyEntity template, EnemyController controller): base()
        {
            _controller = controller;

            Name = template.Name;
            Difficulty = template.Difficulty;
            AtttackInterval = template.AtttackInterval;
            MoveSpeed = template.MoveSpeed;
            MaxHealth = template.MaxHealth;
            Attack = template.Attack;
        
            Health = MaxHealth;
        }
    
        // For creating templates
        public EnemyEntity()
        { }

        // Update is called once per frame
        public override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        public override void Heal(float value)
        {
            Health = Mathf.Clamp(Health + value, Health, MaxHealth);
            _controller.FloatingText($"{value:F1}", Color.green);
        }

        public override void TakeDamage(float value)
        {
            Health = Mathf.Clamp(Health - value, 0.0f, Health);
            _controller.FloatingText($"{value:F1}", Color.red);
        }

        public static EnemyEntity ImportFromXML(string path)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(EnemyEntity));
                StreamReader reader = new StreamReader(path);
                return serializer.Deserialize(reader) as EnemyEntity;
            }
            catch (Exception e)
            {
                Debug.Log("Exception importing xml file: " + e);
                return default;
            }
        }
    }
}
