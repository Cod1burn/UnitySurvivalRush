using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Weapons;

namespace UI
{
    public class UISelectNewWeapon: MonoBehaviour
    {
        [NonSerialized] public bool OptionsAssigned;
        public static UISelectNewWeapon Instance { get; private set; }
        public GameObject menu;
        public GameObject[] options;
        [NonSerialized] public Button AddWeaponButton;
        public TextMeshProUGUI addWeaponButtonText;

        void Awake()
        {
            Instance = this;
            OptionsAssigned = false;
            menu.SetActive(false);
            AddWeaponButton = GetComponent<Button>();
        }

        void Update()
        {
            addWeaponButtonText.text = UIEXPBar.Instance.SkillPoint.ToString();
            if (UIEXPBar.Instance.SkillPoint > 0)
            {
                AddWeaponButton.interactable = true;
            }
            else
            {
                HideMenu();
                AddWeaponButton.interactable = false;
            }
        }

        /// <summary>
        /// Button onclick function
        /// Generate weapon options, toggle the menu.
        /// </summary>
        public void ButtonOnClick()
        {
            if (!OptionsAssigned) AssignWeapons();
            menu.SetActive(!menu.activeSelf);
        }

        /// <summary>
        /// Generate new weapons and assigned to each option Gameobject.
        /// </summary>
        public void AssignWeapons()
        {
            List<WeaponType> types = WeaponManager.Instance.SampleFromNotObtained(options.Length);
            for (int i = 0; i < options.Length; i++)
            {
                // Weapon will be set as null if there is no enough non-obtained weapons.
                options[i].GetComponent<UIWeaponOption>().SetWeapon(
                    i < types.Count ? WeaponManager.Instance.GetWeapon(types[i]) : null);
            }

            OptionsAssigned = true;
        }

        public void ShowMenu()
        {
            menu.SetActive(true);
            
        }

        public void HideMenu()
        {
            menu.SetActive(false);
        }
    }
}