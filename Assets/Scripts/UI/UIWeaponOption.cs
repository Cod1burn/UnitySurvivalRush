using System;
using UnityEngine;
using UnityEngine.UI;
using Weapons;

namespace UI
{
    public class UIWeaponOption : MonoBehaviour
    {
        public GameObject option;
        public Weapon weapon;

        private void Awake()
        {
            
        }

        public void SetWeapon(Weapon w)
        {
            weapon = w;
            option.GetComponent<Image>().sprite = weapon.IconSprite;
            option.GetComponent<Button>().onClick.AddListener(AddWeaponToPlayer);
        }

        public void AddWeaponToPlayer()
        {
            
        }
    }
}