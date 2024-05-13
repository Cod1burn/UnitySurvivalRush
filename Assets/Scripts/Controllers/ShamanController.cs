using System.Collections;
using System.Collections.Generic;
using Entities;
using UnityEngine;

public class ShamanController : EnemyController
{
    private GameObject _magicOrb;
    private GameObject _lightning;
    
    void Awake()
    {
        IsMelee = false;
    }
    // Start is called before the first frame update
    
    void FixedUpdate()
    {
        base.FixedUpdate();
        
        // Psuedo code
        // If distance between player is more than a certain number:
        // Chase player
        // Else cast random magic, aiming at player with a certain inaccurancy

        float distance = (transform.position - AtkTarget.transform.position).magnitude;
        if (distance >= 0.8 * Entity.AttackRange)
        {
            ChaseTarget();
        }
        else
        {
            int index = Random.Range(0, 2);
            switch (index)
            {
                case 1:
                    MagicOrbs();
                    break;
                case 2:
                    Lightning();
                    break;
            }
        }
    }
    

    // Shoot 3 magic orbs aiming at target, with angle of 15 degrees 
    public void MagicOrbs()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject orbObject = Instantiate(_magicOrb, transform.position, Quaternion.identity);
            ProjectileEntity projEtt = orbObject.GetComponent<ProjectileEntity>();
            projEtt.Attack = Entity.Attack;
            projEtt.Range = Entity.AttackRange;

        }
    }

    // Animation: Magic
    public void Lightning()
    {
        Animator.SetTrigger("Magic");
    }

    // Animation: Wand
    public void Fire()
    {
        Animator.SetTrigger("Wand");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
