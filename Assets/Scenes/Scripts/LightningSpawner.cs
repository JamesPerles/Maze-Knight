using UnityEngine;
    public class LightningSpawner : MonoBehaviour
    {
        public GameObject fireballPrefab;           
        public float fireballSpeed = 10f;          
        public float fireballLifetime = 3f;         
        public string playerTag = "Player";         
        public string enemyTag = "Enemy";
        public float enemyRange = 5f;               
        public float spawnDelay = 0.5f;             
        float _lastSpawnTime;               
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Z) && Time.time >= _lastSpawnTime + spawnDelay)
            {
                SpawnFireball();                    
                _lastSpawnTime = Time.time;
            }
        }
        void SpawnFireball()
        {
            GameObject fireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity);  
            Rigidbody2D fireballRb = fireball.GetComponent<Rigidbody2D>();
            if (fireballRb != null)
            {
                float directionX = Input.GetAxisRaw("Horizontal");
                float directionY = Input.GetAxisRaw("Vertical");
                Vector2 direction = new Vector2(directionX, directionY).normalized;
                if (directionX != 0 || directionY != 0)
                {
                    fireball.transform.right = direction;
                }
                fireballRb.linearVelocity = direction * fireballSpeed;
                GameObject player = GameObject.FindGameObjectWithTag(playerTag);
                if (player != null)
                {
                    Physics2D.IgnoreCollision(fireball.GetComponent<Collider2D>(), player.GetComponent<Collider2D>());
                }
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, enemyRange);
                foreach (Collider2D collider in colliders)
                {
                    if (collider.CompareTag(enemyTag))
                    {
                        Vector2 enemyDirection = (collider.transform.position - transform.position).normalized;
                        fireball.transform.right = enemyDirection;
                        fireballRb.linearVelocity = enemyDirection * fireballSpeed;
                        break;
                    }
                }
                Destroy(fireball, fireballLifetime);
            }
            fireball.AddComponent<FireballDamage>(); 
        }
    }
    public class FireballDamage : MonoBehaviour
    {
        public string enemyTag = "Enemy";

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(enemyTag))
            {
            }
        }
    }

