using System;
using Entities;
using TMPro;
using UnityEngine;
using Weapons;

namespace Controllers
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance { get; private set; }
        public GameObject numberTemplate;
        public GameObject aimArrow;
        
        private float _horizontal;
        private float _vertical;

        private Animator _animator;

        public Vector2 Direction
        {
            get { return _direction; }
        }
        private Vector2 _direction;

        public Vector2 AimDirection
        {
            get { return _aimDirection; }
        }
        private Vector2 _aimDirection;

        private Rigidbody2D _rigidbody;
        public PlayerEntity Entity;

        private SpriteRenderer _sr;
        private Color _spriteColor;
        private float _hurtAnimTimer;
        private const float HurtAnimTime = 0.3f;

        private void Awake()
        {
            Instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            Entity = new PlayerEntity(this);
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _sr = GetComponent<SpriteRenderer>();
            _direction = Vector2.left;
            if (_animator != null) _animator.SetBool("Idle", true);
            _spriteColor = Color.white;
        }

        // Update is called once per frame
        void Update()
        {
            _horizontal = Input.GetAxis("Horizontal");
            _vertical = Input.GetAxis("Vertical");

            if (_animator != null)
            {
                if (Mathf.Abs(_horizontal) < 0.15f && Mathf.Abs(_vertical) < 0.15f) _animator.SetBool("Idle", true);
                else
                {
                    _direction = new Vector2(_horizontal, _vertical).normalized;
                    if (_horizontal == 0.0f) _animator.SetBool("Idle", false);
                    else
                    {
                        _animator.SetBool("Idle", false);
                        if (_horizontal > 0) _animator.SetFloat("Horizontal", 1);
                        else _animator.SetFloat("Horizontal", -1);
                    }
                }
            }
            // Get the transform position of mouse
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _aimDirection = (mousePos - transform.position).normalized;

        }

        private void FixedUpdate()
        {
            if (Entity.Health <= 0.0f)
            {
                GetComponent<WeaponManager>().enabled = false; // Disable all weapons;
                _animator.SetBool("Dead", true);
                gameObject.layer = 12; // Layer 12: Dead
                return;
            }
            _hurtAnimTimer -= Time.deltaTime;
            if (_hurtAnimTimer <= 0.0f) _spriteColor = Color.white;
            else _spriteColor = Color.red;
            _sr.color = _spriteColor;

            Vector2 direction = new Vector2(_horizontal, _vertical);
            MoveFor(Time.deltaTime * Entity.MoveSpeed * direction);
            
            // Rotate the aimarrow based on aim direction
            float angle = Mathf.Atan2(_aimDirection.y, _aimDirection.x) * Mathf.Rad2Deg;
            aimArrow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            
            Entity.FixedUpdate();
        }
        
        public void MoveFor(Vector2 distance)
        {
            Vector2 position = _rigidbody.position;
            position += distance;
            _rigidbody.MovePosition(position);
        }

        public void GetHurt()
        {
            _hurtAnimTimer = HurtAnimTime;
        }

        public void PlayerDie()
        {
            Destroy(gameObject);
            UIGameOver.Instance.ShowText();
        }
    
        public GameObject ShootProjectile(GameObject prefab, Vector2 position)
        {
            return Instantiate(prefab, position, Quaternion.identity);
        }

        public void FloatingText(string text, Color color)
        {
            GameObject floatingText = Instantiate(numberTemplate, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
            FloatingNumberController controller = floatingText.GetComponent<FloatingNumberController>();
            controller.SetTracjectory(gameObject, Vector2.up, 1.2f, 1.2f);
            controller.SetText(text, color);
        }

    }
}
