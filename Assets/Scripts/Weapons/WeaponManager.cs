using System.Collections;
using System.Collections.Generic;
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
        
        switch (type)
        {
            case WeaponType.MagicWand:
                _weapons.Add(new MagicWand(gameObject));
                break;
            
            case WeaponType.FireWand:
                _weapons.Add(new FireWand(gameObject));
                break;
            
            default: 
                break;
        }
    }

    public void LevelUpWeapon(WeaponType type)
    {
        foreach (Weapon weapon in _weapons)
        {
            if (weapon.Type == type) weapon.LevelUp();
        }
    }
}
