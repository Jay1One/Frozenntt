using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Player : MonoBehaviour,IPlayer, IHitBox
{
    [SerializeField] private int health=1;
    [SerializeField] private Animator animator;
    private PlayerWeapon[] weapons;
    public void RegisterPlayer()
    
    {
        GameManager manager = FindObjectOfType<GameManager>();
        if (manager.Player == null)
        {
            manager.Player = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Awake()
    {
        RegisterPlayer();
    }

    private void Start()
    {
        weapons = GetComponents<PlayerWeapon>();
        Inputmanager.FireAction += OnAttack;
    }

    private void OnDestroy()
    {
        Inputmanager.FireAction -= OnAttack;
    }

    public int Health
    {
        get => health;
        private set
        {
            health = value;
            if (health <= 0f)
            {
                Die();
            }
        }
    }

    public void Hit(int damage)
    {
        Health -= damage;
    }

    public void Die()
    {
        print("player is died");
    }

    private void OnAttack(string axis)
    {
        foreach (var weapon in weapons)
        {
            if (weapon.Axis==axis)
            {
                Debug.Log("attack");
                weapon.SetDamge();
                animator.SetTrigger("Attack");
                break;
            }
        }
    }
}
