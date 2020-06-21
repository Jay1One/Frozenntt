using UnityEngine;
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class StaticObject : MonoBehaviour,IHitBox
    {
        [SerializeField] private LevelObjectData objectData;
        private int health = 1;
        private Rigidbody2D rigidbody;
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
#if UNITY_EDITOR
        [ContextMenu("Rename")]
        private void Rename()
        {
            if (objectData != null)
            {
                gameObject.name = objectData.Name;
            }
        }
        [ContextMenu("MoveRight")]
        private void MoveRight()
        {
            Move(Vector2.right);
        }
        [ContextMenu("MoveLeft")]
        private void MoveLeft()
        {
            Move(Vector2.left);
        }
        [ContextMenu("Move")]
        private void Move(Vector2 direction)
        {
            var collider = GetComponent<Collider2D>();
            var size = collider.bounds.size.x;
            transform.Translate(direction*size);
        }

        private void MoveUp()
        {
            Move(Vector2.up);
        }
        [ContextMenu("MoveUp")]
        private void InstantiateAndMove()
        {
            Instantiate(gameObject, transform.position, Quaternion.identity);
            MoveUp();
        }
#endif
       
        public void Start()
        {
            health = objectData.Health;
            rigidbody = GetComponent<Rigidbody2D>();
            rigidbody.bodyType = objectData.IsStatic? RigidbodyType2D.Static:RigidbodyType2D.Dynamic;
        }
        public void Hit(int damage)
        {
            Health -= damage;
        }

        public void Die()
        {
            Destroy(gameObject);
        }
    }
