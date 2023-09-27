using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class UITooltip : MonoBehaviour
    {
        public static UITooltip Instance { get; private set; }

        public TextMeshProUGUI title;
        public TextMeshProUGUI body;

        private CanvasGroup _canvasGroup;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        private void Awake()
        {
            Instance = this;
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = 0;
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void SetText(string titleText, string bodyText)
        {
            title.text = titleText;
            body.text = bodyText;
        }

        public void ShowTooltip(Vector2 position)
        {
            transform.position = position; 
            _canvasGroup.alpha = 1;
        }

        public void HideTooltip()
        {
            _canvasGroup.alpha = 0;
        }

    }
}
