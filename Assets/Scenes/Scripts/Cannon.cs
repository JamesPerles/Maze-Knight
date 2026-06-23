using UnityEngine;
    public class Cannon : MonoBehaviour
    {
        public GameObject iceBlockPrefab;   
        public float spawnRange = 5f;       
        public float spawnInterval = 10f;   
        public float iceBlockSpeed = 5f;    
        Transform _player;           
        float _spawnTimer;      
        void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player").transform;   
        }
        void Update()
        {
            if (Vector2.Distance(transform.position, _player.position) <= spawnRange && _spawnTimer >= spawnInterval)
            {
                SpawnIceBlock();    
                _spawnTimer = 0f;   
            }
            _spawnTimer += Time.deltaTime;   
        }
        void SpawnIceBlock()
        {
            var position = transform.position;
            GameObject iceBlock = Instantiate(iceBlockPrefab, position, Quaternion.identity);
            Vector2 direction = (_player.position - position).normalized;
            iceBlock.GetComponent<Rigidbody2D>().linearVelocity = direction * iceBlockSpeed;
        }
        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Destroy(gameObject);  
            }
        }
    }
