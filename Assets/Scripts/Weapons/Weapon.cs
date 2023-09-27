using System;
using Controllers;
using Entities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Weapons
{
    public class Weapon
    {
        protected GameObject Projectile;
        protected GameObject Player;
        protected PlayerEntity Entity;
        protected PlayerController Controller;

        public float AttackMultiplier;
        public float AttackInterval;

        public float MinimumInterval;

        public int ProjNum;
        protected int ProjCount;
        
        public float ProjRange;
        public float ProjSpeed;
        public float ProjDuration;
        public float ProjScale;

        public int Level;
        public int MaxLevel;
        
        public WeaponType Type;

        protected float Timer;

        public string Name { get; protected set; }
        /// <summary>
        /// The weapon description.
        /// </summary>
        public string Description { get; protected set; }
        
        /// <summary>
        /// The description of bonus after level up.
        /// </summary>
        public string Tooltip { get; protected set; }

        public Sprite IconSprite { get; protected set; }
        

        public Weapon(GameObject player, WeaponType type, GameObject projectile)
        {
            Projectile = projectile;
            this.Type = type;
            Player = player;

            
            Controller = Player.GetComponent<PlayerController>();
            Entity = Controller.Entity;

            Level = 1;
            Timer = 0.0f;

            ProjCount = 0;
            
        }

        public virtual void Update()
        {
            Timer -= Time.deltaTime * Entity.AttackSpeed;
            if (Timer <= 0.0f) Attack();
        }

        public virtual void Attack()
        {
            if (ProjCount < ProjNum) Timer = MinimumInterval;
            else
            {
                ProjCount = 0;
                Timer = AttackInterval;
            }
        }

        public void LevelUp()
        {
            Level = Level < MaxLevel ? Level + 1 : MaxLevel;
            GetLevelBonus();
        }

        public virtual void GetLevelBonus()
        {
            
        }
        
    }
}