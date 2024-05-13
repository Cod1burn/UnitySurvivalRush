using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using DefaultNamespace;
using Entities;
using ObjectManager;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    public EnemyType type;
    public EnemyEntity Entity;

    private GameObject _numberTemplate;

    /// <summary>
    /// Loot table contains all the possible loots by an enemy by a dictionary.
    /// Key is the item type.
    /// Value is a tuple of 1. loot probability 2. value of the loot.
    /// Each type of loot is dropped individually.
    /// </summary>
    public Dictionary<ItemType, (float, float[])> LootTable;

    public bool IsMelee;
    protected GameObject AtkTarget;
    
    protected Animator Animator;
    
    private Rigidbody2D _rigidbody;
    private Vector2 _direction;
    private Vector2 _aimDirection;

    protected float AttackTimer;
    
    private SpriteRenderer _sr;
    private Color _spriteColor;
    private float _hurtAnimTimer;
    private const float HurtAnimTime = 0.2f;
    // Start is called before the first frame update
    protected void Awake()
    {
        Animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _direction = new Vector2(0.0f, 0.0f);

        _hurtAnimTimer = 0.0f;
        AttackTimer = 0.0f;

        LootTable = new Dictionary<ItemType, (float, float[])>();
        
        _numberTemplate = Resources.Load("Text/FloatingNumberEnemy") as GameObject;
    }

    // Update is called once per frame
    protected void FixedUpdate()
    {
        Entity.FixedUpdate();
        if (Entity.Health <= 0.0f)
        {
            Animator.SetBool("Dead", true);
            gameObject.tag = "DeadEnemy";
            gameObject.layer = 12; // Layer 12: Dead
            return;
        }
        
        if (_hurtAnimTimer <= 0.0f) _spriteColor = Color.white;
        else
        {
            _hurtAnimTimer -= Time.deltaTime;
            _spriteColor = new Color(0.7f, 0.7f, 0.7f, 1.0f);
        }
        _sr.color = _spriteColor;
    }

    public void ChaseTarget()
    {
        MoveDirection();
        if (_direction.x != 0.0f)
        {
            float horizontal = _direction.x > 0.0f ? 1 : -1;
            Animator.SetFloat("Horizontal", horizontal);
        }

        MoveFor(Time.deltaTime * Entity.MoveSpeed * _direction);
        if (AttackTimer > 0.0f) AttackTimer -= Time.deltaTime;
    }

    /// <summary>
    /// Move the object for a vector distance.
    /// </summary>
    /// <param name="distance">The distance to be moved in vector</param>
    public void MoveFor(Vector2 distance)
    {
        Vector2 position = _rigidbody.position;
        position += distance;
        _rigidbody.MovePosition(position);
    }

    public void SetEntity(EnemyEntity template)
    {
        Entity = new EnemyEntity(template, this);
    }

    void MoveDirection()
    {
        GameObject target = EnemyManager.Instance.target;
        if (target != null)
        {
            _direction = target.transform.position - gameObject.transform.position;
            _direction = _direction.normalized;
        }
    }

    public void AttackAnimation(GameObject gameObject, String animationName)
    {
        Animator.SetTrigger(animationName);
        AttackTimer = Entity.AtttackInterval;
        AtkTarget = gameObject;
    }

    public void AttackTarget()
    {
        PlayerController player = AtkTarget.GetComponent<PlayerController>();
        if (player != null)
        {
            player.GetHurt();
            player.Entity.TakeDamage(Entity.Attack);
        }
        AtkTarget = null;
    }

    public void GetHurt()
    {
        _hurtAnimTimer = HurtAnimTime;
    }

    public void Die()
    {
        DropLoot();
        Destroy(gameObject);
    }

    public void AddLoot(ItemType it, float prob, params float[] values)
    {
        LootTable[it] = (prob, values);
    }

    public void DropLoot()
    {
        Vector2 positionAlign = Vector2.zero;
        foreach (var pair in LootTable)
        {
            (float prob, float[] values) tuple = LootTable[pair.Key];
            if (Random.value >= tuple.prob) continue;
            ItemManager.Instance.GenerateItem(pair.Key, tuple.values, (Vector2)transform.position + positionAlign);
            // Make sure that everything dose not stack together
            if (positionAlign.x > positionAlign.y) positionAlign.y += 0.3f;
            else positionAlign.x += 0.3f;
        }
    }
    
    public void FloatingText(string text, Color color)
    {
        GameObject floatingText = Instantiate(_numberTemplate, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
        FloatingNumberController controller = floatingText.GetComponent<FloatingNumberController>();
        controller.SetTracjectory(gameObject, Vector2.up, 1.0f, 1.5f);
        controller.SetText(text, color);
    }
}
