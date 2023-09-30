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
        }

        public void AddNewWeaponComponent(Weapon weapon)
        {
            GameObject weaponComponentObject = Instantiate(weaponComponent, gameObject.transform);
            weaponComponentObject.transform.localPosition = new Vector3(0.0f, -28.0f * (Num + 1), 0.0f);
            UIWeaponComponent controller = weaponComponentObject.GetComponent<UIWeaponComponent>();
            controller.SetWeapon(weapon);
            Num++;
        }
    }
}
