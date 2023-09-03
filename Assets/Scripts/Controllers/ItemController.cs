using System;
using Entities;
using UnityEngine;
using UnityEngine.Serialization;

namespace Controllers
{
    public class ItemController : MonoBehaviour
    {
        public ItemEntity Entity;
        
        [NonSerialized] public float Speed = 4.0f;

        private PlayerController _target;

        private Vector2 _direction;
        // Start is called before the first frame update
        void Awake()
        {
            _direction = Vector2.zero;
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void FixedUpdate()
        {
            if (_target == null) return;
            if ((_target.transform.position - transform.position).magnitude < 0.5f)
            {
                Entity.GetConsumed(_target.Entity);
                Destroy(gameObject);
                return;
            } 
            _direction = (_target.transform.position - transform.position).normalized;
            Vector2 position = transform.position;
            position +=  _direction * (Time.deltaTime * Speed);
            transform.position = position;
        
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                _target = player;
            }
        }

        public void SetItemEntity(ItemType it, float[] values)
        {
            Entity = new ItemEntity(it, values);
        }
    
    }
}
