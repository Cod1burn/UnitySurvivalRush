using System;
using System.Collections.Generic;
using System.Linq;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

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

        /// <summary>
        /// Sample some weapon types from all the types that are not obtained by the player.
        /// </summary>
        /// <param name="num">Number of weapon types to be sampled.</param>
        /// <returns></returns>
        public List<WeaponType> SampleFromNotObtained(int num)
        {
            List<WeaponType> lst = new List<WeaponType>();
            var types = Enum.GetValues(typeof(WeaponType));
            var pool = types.Cast<WeaponType>().Where(t =>
            {
                foreach (var w in _weapons)
                    if (w.Type == t)
                        return true;
                return false;
            }).ToList();
            for (int i = 0; i < num; i++)
            {
                if (pool.Count == 0) return lst;
                int randomIndex = Random.Range(0, pool.Count);
                WeaponType type = pool[randomIndex];
                lst.Add(type);
                pool.Remove(type);
            }

            return lst;
        }
        
        /// <summary>
        /// Create a level 1 weapon with given weapon type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>

        public Weapon GetWeapon(WeaponType type)
        {
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
        
        /// <summary>
        /// Add the weapon to the weapon list of character.
        /// </summary>
        /// <param name="weapon"></param>

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
