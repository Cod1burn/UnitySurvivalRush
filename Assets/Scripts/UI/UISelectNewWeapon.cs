using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Weapons;

namespace UI
{
    public class UISelectNewWeapon: MonoBehaviour
    {
        public static UISelectNewWeapon Instance { get; private set; }
        public GameObject menu;
        public GameObject[] options;

        void Awake()
        {
            Instance = this;
        }

        public void ShowMenu()
        {
            menu.SetActive(true);
            List<WeaponType> types = WeaponManager.Instance.SampleFromNotObtained(options.Length);
            for (int i = 0; i < options.Length; i++)
            {
                // Weapon will be set as null if there is no enough non-obtained weapons.
                options[i].GetComponent<UIWeaponOption>().SetWeapon(
                    i < types.Count ? WeaponManager.Instance.GetWeapon(types[i]) : null);
            }
        }

        public void HideMenu()
        {
            menu.SetActive(false);
        }
    }
}