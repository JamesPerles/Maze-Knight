using UnityEngine;

namespace Scenes.Scripts
{
    public class Cannon : MonoBehaviour
    {
        public GameObject iceBlockPrefab;   
        public float spawnRange = 5f;       
        public float spawnInterval = 10f;   
        public float iceBlockSpeed = 5f;    

        private Transform _player;           
        private float _spawnTimer;      

        
        private void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player").transform;   
        }
        
        private void Update()
        {
            if (Vector2.Distance(transform.position, _player.position) <= spawnRange && _spawnTimer >= spawnInterval)
            {
                SpawnIceBlock();    
                _spawnTimer = 0f;   
            }

            _spawnTimer += Time.deltaTime;   
        }

        
        private void SpawnIceBlock()
        {
            var position = transform.position;
            GameObject iceBlock = Instantiate(iceBlockPrefab, position, Quaternion.identity);
            Vector2 direction = (_player.position - position).normalized;
            iceBlock.GetComponent<Rigidbody2D>().linearVelocity = direction * iceBlockSpeed;
        }
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Destroy(gameObject);  
            }
        }
    }
}
