using Controllers;
using UnityEngine;

namespace Entities
{
    public class PlayerEntity: BaseEntity
    {
        public PlayerController Controller { get; private set; }

        public float MoveSpeed
        {
            get { return BaseMoveSpeed * MoveSpeedMultiplier; }
            set { BaseMoveSpeed = value; }
        }

        public float MaxHealth
        {
            get { return BaseMaxHealth * HealthMultiplier; }
            set { BaseMaxHealth = value; }
        }

        public float Attack
        {
            get { return BaseAttack * AttackMultiplier; }
            set { BaseAttack = value; }
        }
        
        
        public const float InitialMoveSpeed = 4.5f;
    
        public const float InitialMaxHealth = 10.0f;
    
        public const float InitialAttack = 3.0f;

        public float AttackSpeed;
    
        

        public int Level = 1;
        public float Exp = 0.0f;
        public float MaxExp = 100.0f;

        public float ExpAmp = 1.0f;
        public float DefAmp = 1.0f;

        public PlayerEntity(PlayerController controller) : base()
        {
            MoveSpeed = InitialMoveSpeed;
            MaxHealth = InitialMaxHealth;
            Attack = InitialAttack;
            Health = InitialMaxHealth;
            AttackSpeed = 1.0f;
            UpdateExpBar();
            UpdateHealthBar();

            Controller = controller;

        }

        // Update is called once per frame
        public override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        public override void Heal(float value)
        {
            Health = Mathf.Clamp(Health + value, Health, MaxHealth);
            Controller.FloatingText($"{value:F1}", Color.green);
            UpdateHealthBar();
        }

        public override void TakeDamage(float value)
        {
            value *= DefAmp;
            Controller.FloatingText($"{value:F1}", Color.red);
            Health = Mathf.Clamp(Health - value, 0.0f, Health);
            UpdateHealthBar();
        }

        void UpdateHealthBar()
        {
            UIHealthBar.Instance.SetValue(Health, MaxHealth);
        }

        void UpdateExpBar()
        {
            UIEXPBar.Instance.SetProgress(Exp, MaxExp, Level);
        }
        public void LevelUp()
        {
            Level += 1;
            MaxExp += Level * 20;
        }

        public void GetExp(float amount)
        {
            amount *= ExpAmp;
            Exp += amount;
            while (Exp >= MaxExp)
            {
                Exp -= MaxExp;
                LevelUp();
            }
            UpdateExpBar();
        }
    }
}
