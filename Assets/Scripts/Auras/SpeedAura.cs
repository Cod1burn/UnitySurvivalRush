namespace Auras
{
    public class SpeedAura : Aura
    {
        public SpeedAura(string name, float duration, float value) : base(name, duration, value)
        {
            if (this.Values[0] >= 0.0f) Description = $"Moving speed increased by {(int)(this.Values[0] * 100)}%.";
            else Description = $"Moving speed decreased by {(int)(this.Values[0] * -100)}%.";
        }

        public override void OnAuraApply()
        {
            Owner.MoveSpeedMultiplier += Values[0];
            base.OnAuraApply();
        }

        public override void OnAuraEnd()
        {
            Owner.MoveSpeedMultiplier -= Values[0];
            base.OnAuraEnd();
        }
    }
}