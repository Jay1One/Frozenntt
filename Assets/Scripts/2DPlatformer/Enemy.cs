using System;
using UnityEngine;

public enum EnemyState
{
    Sleep,
    Wait,
    StartWalk,
    Walk,
    StartAttack,
    Attack,
}
    public class Enemy : MonoBehaviour,IEnemy, IHitBox
    {
        [SerializeField] private int health=1;
        [SerializeField] private Animator animator;
        [SerializeField] private Transform checkGroundPoint;
        [SerializeField] private Transform checkAttackPoint;
        [SerializeField] private Transform graphics;

        private GameManager gameManager;
        private EnemyState currentEnemyState;
        private float wakeUpTimer;
        private EnemyState nextState;
        private float waitTimer;
        private float attackTimer;
        private float currentDirection = 1f;
        

        public void RegisterEnemy()
        {
            gameManager = FindObjectOfType<GameManager>();
            gameManager.Enemies.Add(this);
         
        }

        private void Awake()
        {
            RegisterEnemy();
            wakeUpTimer = Time.time + 1f;
           
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
            animator.SetTrigger("Die");
            Destroy(gameObject,0.5f);
        }

        private void Update()
        {
            switch (currentEnemyState)
            {
                case EnemyState.Sleep:
                    Sleep();
                    break;
                
                case EnemyState.Wait:
                    Wait();
                    break;
                
                case EnemyState.StartWalk:
                    animator.SetInteger("Walking",1);
                    currentEnemyState = EnemyState.Walk;
                    break;
                case EnemyState.Walk:
                    Walk();
                    break;
                
                case EnemyState.StartAttack:
                    animator.SetTrigger("Attack");
                    ((IHitBox) gameManager.Player).Hit(1);
                    currentEnemyState = EnemyState.Attack;
                    break;
                case EnemyState.Attack:
                    Attack();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void Sleep()
        {
            if (Time.time >= wakeUpTimer)
                WakeUp();
        }

        private void WakeUp()
        {
            var playerPosition = ((MonoBehaviour) gameManager.Player).transform.position;
        }
        private void Wait()
        {
            
        }

        private void Walk()
        {
            
        }

        private void Attack()
        {
            
        }
    }

