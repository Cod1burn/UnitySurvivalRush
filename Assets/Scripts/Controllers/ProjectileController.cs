using Entities;
using UnityEngine;

namespace Controllers
{
    public class ProjectileController : MonoBehaviour
    {
        public GameObject owner;
    
        private Vector2 _direction;
        private ProjectileEntity _pe;
    
        // Start is called before the first frame update
        void Awake()
        {
            _direction = Vector2.up;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            Vector2 position = transform.position;
            position += _direction * (Time.deltaTime * _pe.Speed);
            transform.position = position;
        }
    
        public void Shoot(GameObject ownerObject, Vector2 shootDirection)
        {
            owner = ownerObject;
            _pe = GetComponent<ProjectileEntity>();
            gameObject.transform.localScale *= _pe.Scale;
            ChangeDirection(shootDirection);
        }

        public void ChangeDirection(Vector2 direction)
        {
            transform.Rotate(0, 0, Vector2.Angle(direction, _direction));
            _direction = direction;
        }
    
    }
}
