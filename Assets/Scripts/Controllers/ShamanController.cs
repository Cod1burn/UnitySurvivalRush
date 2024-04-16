using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShamanController : EnemyController
{
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
    }
    

    public void MagicOrbs()
    {
        
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
