using Controllers;
using JetBrains.Annotations;
using UnityEngine;

namespace Auras
{
    public class KnockBack: Aura
    {
        
        [CanBeNull] private EnemyController _enemyController;
        [CanBeNull] private PlayerController _playerController;
        
        /// <summary>
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="name"></param>
        /// <param name="duration"></param>
        /// <param name="x">Values[0], x-axis knocking distance</param>
        /// <param name="y">Value[1], y-axis knocking distance</param>
        public KnockBack(EnemyController controller, string name, float duration, float x, float y) : base(name, duration, x, y)
        {
            _enemyController = controller;
            _playerController = null;
            Description = "Get knocked back";
        }
        
        public KnockBack(PlayerController controller, string name, float duration, float x, float y) : base(name, duration, x, y)
        {
            _enemyController = null;
            _playerController = controller;
            Description = "Get knocked back";
        }

        public override void FixedUpdate()
        {
            Vector2 distance = Time.deltaTime * new Vector2(Values[0], Values[1]);
            if (_enemyController != null) _enemyController.MoveFor(distance);
            else if (_playerController != null) _playerController.MoveFor(distance);
            base.FixedUpdate();
        }
    }
}