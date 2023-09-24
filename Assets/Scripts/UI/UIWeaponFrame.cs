using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIWeaponFrame : MonoBehaviour
    {
        public Button addWeponButton;
        
        // Start is called before the first frame update
        void Start()
        {
        
        }

        private void Awake()
        {
            addWeponButton.gameObject.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        
        /// <summary>
        /// Open the level up menu to select a new weapon or upgrade an existing weapon.
        /// Call by button on-click event in inspector.
        /// </summary>
        public void OpenLevelUpMenu()
        {
        
        }
    }
}
