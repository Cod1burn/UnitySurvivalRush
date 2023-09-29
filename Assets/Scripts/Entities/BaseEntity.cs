using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Auras;
using UnityEngine;

namespace Entities
{
    public class BaseEntity
    {
        protected float BaseMoveSpeed;
        public float MoveSpeedMultiplier = 1.0f;

        
        protected float BaseMaxHealth;
        public float Health;
        public float HealthMultiplier = 1.0f;
        
        protected float BaseAttack;
        public float AttackMultiplier = 1.0f;

        protected List<Aura> Auras;

        public BaseEntity()
        {
            Auras = new List<Aura>();
        }

        public virtual void FixedUpdate()
        {
            Auras.ForEach(aura => aura.FixedUpdate());
            Auras.RemoveAll(aura => aura.Expired);
        }

        public virtual void Heal(float value) {}
        
        public virtual void TakeDamage(float value) {}

        public void ApplyAura(Aura aura)
        {
            Auras.Add(aura);
        }

        public void RemoveAura(Aura aura)
        {
            Auras.Remove(aura);
        }

        public Aura[] GetAuras()
        {
            return Auras.ToArray();
        }

    }
}