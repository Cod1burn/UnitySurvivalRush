using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Weapons
{
    public class WeaponManager : MonoBehaviour
    {
        public static WeaponManager Instance { get; private set; }
     
        public int maxWeapons = 5;
    
        private List<Weapon> _weapons;
        // Start is called before the first frame update
        void Awake()
        {
            _weapons = new List<Weapon>();
            Instance = this;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            _weapons.ForEach(w => w.Update());
        }

        public Weapon GetWeapon(WeaponType type)
        {
            Weapon weapon_;
            switch (type)
            {
                case WeaponType.MagicWand:
                    return new MagicWand(gameObject);

                case WeaponType.FireWand:
                    return new FireWand(gameObject);

                case WeaponType.Lightning:
                    return new Lightning(gameObject);

                default:
                    return new MagicWand(gameObject);
            }
        }

        public void AddWeapon(Weapon weapon)
        {
            if (_weapons.Count >= maxWeapons) return;
            foreach (Weapon w in _weapons)
            {
                if (w.Type == weapon.Type) return;
            }
            _weapons.Add(weapon);
            UIWeaponsFrame.Instance.AddNewWeapon(weapon);
        }
    

        public void LevelUpAllWeapon(WeaponType type)
        {
            foreach (Weapon weapon in _weapons)
            {
                if (weapon.Type == type) weapon.LevelUp();
            }
        }
    }
}
