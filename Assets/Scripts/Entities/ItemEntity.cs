using Auras;
using Unity.VisualScripting;

namespace Entities
{
    public delegate void EntityEffect(BaseEntity entity);

    public class ItemEntity
    {
        private float[] _values;

        private EntityEffect _effect;

        private Aura _aura;

        public void DefaultEffect(BaseEntity entity)
        { }

        public void ExpItemEffect(BaseEntity entity)
        {
            // values[0]: Experience amount
            (entity as PlayerEntity)?.GetExp(_values[0]);
        }

        public void HealingItemEffect(BaseEntity entity)
        {
            // values[0]: Healing amount
            entity.Heal(_values[0]);
        }

        public void ApplyAuraEffect(BaseEntity entity)
        {
            _aura?.ApplyTo(entity);
        }

        /// <summary>
        /// Constructor for all other immediate effects
        /// </summary>
        /// <param name="it">Cannot be ApplyAuraEffect</param>
        /// <param name="values"></param>
        public ItemEntity(ItemType it, float[] values)
        {

            this._values = values;
            switch (it)
            {
                case ItemType.ExpOrb:
                    _effect = ExpItemEffect;
                    break;
            
                case ItemType.HealingPotion:
                    _effect = HealingItemEffect;
                    break;
            
                default:
                    _effect = DefaultEffect;
                    break;
            }
        }

        /// <summary>
        /// For items that applying aura to player
        /// </summary>
        public ItemEntity(Aura aura)
        {
            _aura = aura;
            _effect = ApplyAuraEffect;
        }

        public void GetConsumed(BaseEntity entity)
        {
            _effect(entity);
        }
    }
}