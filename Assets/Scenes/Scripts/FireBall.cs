using UnityEngine;

namespace Scenes.Scripts
{
    public class FireBall : MonoBehaviour
    {
        public int damage = 1;                    

        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))     
            {
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                if (enemy != null)     
                {
                    enemy.TakeDamage(damage);     
                }

               
                Destroy(gameObject);
            }
        }
    }
}