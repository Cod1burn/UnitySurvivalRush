using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Weapons;

namespace UI
{
    public class UIWeaponsFrame : MonoBehaviour
    {
        public static UIWeaponsFrame Instance { get; private set; }
        [NonSerialized] public int Num;
        public GameObject weaponComponent;
        public GameObject addWeaponButton;
        public TextMeshProUGUI addWeaponButtonText;


        
        // Start is called before the first frame update
        void Start()
        {
        
        }

        private void Awake()
        {
            Instance = this;
            Num = 0;
        }

        // Update is called once per frame
        void Update()
        {
            if (UIEXPBar.Instance.SkillPoint > 0)
            {
                addWeaponButton.SetActive(true);
                addWeaponButtonText.text = UIEXPBar.Instance.SkillPoint.ToString();
            }
            else 
                addWeaponButton.SetActive(false);
        }

        public void AddNewWeapon(Weapon weapon)
        {
            GameObject weaponComponentObject = Instantiate(weaponComponent, gameObject.transform);
            weaponComponentObject.transform.localPosition = new Vector3(0.0f, -28.0f * (Num + 1), 0.0f);
            UIWeaponComponent controller = weaponComponentObject.GetComponent<UIWeaponComponent>();
            controller.SetWeapon(weapon);
            Num++;
        }
    }
}
