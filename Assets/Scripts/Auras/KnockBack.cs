using Controllers;
using Entities;
using JetBrains.Annotations;
using UnityEngine;

namespace Auras
{
    public class KnockBack: Aura
    {
        
        [CanBeNull] private EnemyController _enemyController;
        [CanBeNull] private PlayerController _playerController;
        
        /// <summary>
        /// Create an empty target knockback aura, Get the controller after the aura is applied to the target.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="duration"></param>
        /// <param name="x">Values[0], x-axis knocking distance</param>
        /// <param name="y">Value[1], y-axis knocking distance</param>
        public KnockBack(string name, float duration, float x, float y) : base(name, duration, x, y)
        {
            _enemyController = null;
            _playerController = null;
            Description = "Get knocked back";
        }

        public override void ApplyTo(BaseEntity entity)
        {
            base.ApplyTo(entity);
            if (entity is EnemyEntity enemyEntity) _enemyController = enemyEntity.Controller;
            else if (entity is PlayerEntity playerEntity) _playerController = playerEntity.Controller;
        }

        public override void FixedUpdate()
        {
            Vector2 distance = Time.deltaTime * new Vector2(Values[0], Values[1]);
            if (_enemyController != null) _enemyController.MoveFor(distance);
            else if (_playerController != null) _playerController.MoveFor(distance);
            base.FixedUpdate();
        }

        public override Aura Copy()
        {
            return new KnockBack(Name, Duration, Values[0], Values[1]);
        }
    }
}