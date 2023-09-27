namespace Auras
{
    public class DamageAura : Aura
    {
        public DamageAura(string name, float duration, float value) : base(name, duration, value)
        {
            if (Values[0] >= 0.0f) Description = $"Damage increased by {(int)(this.Values[0] * 100)}%.";
            else Description = $"Damage decreased by {(int)(this.Values[0] * -100)}%.";
        }

        public override void OnAuraApply()
        {
            Owner.AttackMultiplier += Values[0];
            base.OnAuraApply();
        }

        public override void OnAuraEnd()
        {
            Owner.AttackMultiplier -= Values[0];
            base.OnAuraEnd();
        }

        public override Aura Copy()
        {
            return new DamageAura(Name, Duration, Values[0]);
        }
    }
}