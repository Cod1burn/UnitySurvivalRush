using System;
using TMPro;
using UnityEngine;

namespace Controllers
{
    public class FloatingNumberController : MonoBehaviour
    {
        private TextMeshPro _text;
        private Color _color;

        private float _length;
        private float _speed;
        private Vector2 _angle;

        private float _timer;

        // Start is called before the first frame update
        void Awake()
        {
            _text = GetComponent<TextMeshPro>();
            _timer = 0.0f;
        }

        // Update is called once per frame
        void Update()
        {
        }

        public void SetTracjectory(GameObject parent, Vector2 angle, float length, float speed)
        { 
            transform.SetParent(parent.transform);
            _angle = angle;
            _length = length;
            _speed = speed;
        }

        public void SetText(string text, Color color)
        {
            _text.text = text;
            _color = color;
        }

        private void FixedUpdate()
        {
            _color.a = 1.0f - (_timer * _speed) / _length;
            _text.color = _color;
            
            Vector2 position = transform.position;
            position += _angle * (Time.deltaTime * _speed);
            transform.position = position;
            
            _timer += Time.deltaTime;
            if (_timer * _speed > _length) Destroy(gameObject);
        }
    }
}
