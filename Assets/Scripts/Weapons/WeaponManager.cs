using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using Weapons;

public class WeaponManager : MonoBehaviour
{
     
    public int maxWeapons = 5;
    
    private List<Weapon> _weapons;
    // Start is called before the first frame update
    void Awake()
    {
        _weapons = new List<Weapon>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _weapons.ForEach(w => w.Update());
    }

    public void AddWeapon(WeaponType type)
    {
        if (_weapons.Count >= maxWeapons) return;
        foreach (Weapon weapon in _weapons)
        {
            if (weapon.Type == type) return;
        }

        Weapon weapon_;
        switch (type)
        {
            case WeaponType.MagicWand:
                weapon_ = new MagicWand(gameObject);
                break;
            
            case WeaponType.FireWand:
                weapon_ = new FireWand(gameObject);
                break;
            
            default:
                weapon_ = new MagicWand(gameObject);
                break;
        }
        _weapons.Add(weapon_);
        UIWeaponsFrame.Instance.AddNewWeapon(weapon_);
    }

    public void LevelUpWeapon(WeaponType type)
    {
        foreach (Weapon weapon in _weapons)
        {
            if (weapon.Type == type) weapon.LevelUp();
        }
    }
}
