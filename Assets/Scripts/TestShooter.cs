using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEngine;

public class TestShooter : MonoBehaviour
{
    public GameObject projectile;

    private PlayerController _player;
    // Start is called before the first frame update
    void Awake()
    {
        _player = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            GameObject p = Instantiate(projectile, transform.position, Quaternion.identity);
            ProjectileController controller = p.GetComponent<ProjectileController>();
            controller.Shoot(gameObject, _player.Direction);
        }
    }
}
