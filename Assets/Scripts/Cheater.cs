using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using ObjectManager;
using UnityEngine;
using Weapons;
using Random = UnityEngine.Random;

public class Cheater : MonoBehaviour
{
    private ItemManager _itemManager;
    private WeaponManager _weaponManager;
    
    // Start is called before the first frame update
    void Awake()
    {
        _weaponManager = GetComponent<WeaponManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _weaponManager.AddWeapon(_weaponManager.GetWeapon(WeaponType.MagicWand));
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _weaponManager.AddWeapon(_weaponManager.GetWeapon(WeaponType.Lightning));
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _weaponManager.AddWeapon(_weaponManager.GetWeapon(WeaponType.Lightning));
        }


        if (Input.GetKeyDown(KeyCode.L))
        {
            foreach (WeaponType type in Enum.GetValues(typeof(WeaponType)))
            {
                _weaponManager.LevelUpAllWeapon(type);
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            GetComponent<PlayerController>().Entity.GetExp(100);
        }
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            GetComponent<PlayerController>().FloatingText("1.5", Color.red);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            float angle = Random.value * Mathf.PI;
            float distance = Random.Range(2.0f, 5.0f);
            Vector2 deltaPosition = new Vector2(Mathf.Cos(angle) * distance, Mathf.Sin(angle) * distance);
            Vector2 position = (Vector2)transform.position + deltaPosition;
            EnemyManager.Instance.GenerateEnemy(0, position);
        }
    }
}
