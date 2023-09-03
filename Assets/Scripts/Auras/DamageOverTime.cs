namespace Auras
{
    public class DamageOverTime: Aura
    {
        /// <summary>
        /// Setting a damage interval to show damage numbers in the interface.
        /// Healing interval default 1.0f.
        /// </summary>
        public float Interval = 1.0f;
        private int _count;
        
        public DamageOverTime(string name, float duration, float value) : base(name, duration, value)
        {
            if (this.Values[0] < 0) this.Values[0] = 0.0f; 
            Description = $"Received {this.Values[0]:F1} damage per second";

            _count = 0;
        }

        public override void FixedUpdate()
        {
            // Healing by interval
            if (Timer <= Interval * _count)
            {
                Owner.Heal(Values[0]);
                _count++;
            }
            base.FixedUpdate();
        }
    }
}