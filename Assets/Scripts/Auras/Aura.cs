using Entities;
using UnityEngine;
using UnityEngine.UI;

namespace Auras
{
    public class Aura
    {
        public string Name;
    
        public string Description;

        public float Duration;

        public float Timer { get; private set; }

        protected BaseEntity Owner;

        public float[] Values { get; private set; }
        // Start is called before the first frame update
        public Aura(string name, float duration, params float[] values)
        {
            this.Name = name;
            this.Duration = duration;
            this.Values = values;
        }

        public virtual void ApplyTo(BaseEntity entity)
        {
            Owner = entity;
            Timer = 0.0f;
            Owner.ApplyAura(this);
            OnAuraApply();
        }

        public void Extend(float time)
        {
            Duration += time;
        }

        public virtual void OnAuraApply() {}

        /// <summary>
        /// To override this method, call the base method at the end of the override method.
        /// </summary>
        public virtual void FixedUpdate()
        {
            Timer += Time.deltaTime;
            if (Timer >= Duration)
            {
                OnAuraEnd();
                Owner.RemoveAura(this);
            }
        }

        public virtual void OnAuraEnd()
        {
            Owner = null;
        }

        public virtual Aura Copy()
        {
            return null;
        }
    
    }
}
