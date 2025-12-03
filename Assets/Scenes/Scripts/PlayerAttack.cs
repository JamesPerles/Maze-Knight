
using UnityEngine;

namespace Scenes.Scripts
{
    public class PlayerAttack : MonoBehaviour
    {
        public Sprite attackSprite;            
        public float attackDuration = 0.5f;    
        public float attackForce = 10f;         
        public LayerMask enemyLayer;            

        private Sprite originalSprite;         
        private bool isAttacking;              
        private float attackTimeRemaining;    
        private bool isInvincible;              
        public bool IsAttacking { get; set; }

        
        private void Start()
        {
            originalSprite = GetComponent<SpriteRenderer>().sprite;
        }

        
        private void Update()
        {
            
            if (Input.GetKeyDown(KeyCode.Space) && !isAttacking)
            {
                isAttacking = true;
                isInvincible = true; 
                attackTimeRemaining = attackDuration;
                GetComponent<SpriteRenderer>().sprite = attackSprite;

                
                Physics2D.IgnoreLayerCollision(gameObject.layer, enemyLayer);

               
                Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, 1f);
                foreach (Collider2D enemy in enemies)
                {
                    if (enemy.CompareTag("Enemy"))
                    {
                        Enemy enemyScript = enemy.GetComponent<Enemy>();
                        if (enemyScript != null)
                        {
                            enemyScript.TakeDamage(0);
                            Vector2 knockbackDirection = (enemy.transform.position - transform.position).normalized;
                            enemyScript.rb.AddForce(knockbackDirection * attackForce, ForceMode2D.Impulse);
                        }
                    }
                }
            }
            else if (isAttacking)
            {
                
                attackTimeRemaining -= Time.deltaTime;
                if (attackTimeRemaining <= 0f)
                {
                    
                    isAttacking = false;
                    isInvincible = false; 
                    GetComponent<SpriteRenderer>().sprite = originalSprite;

                    
                    Physics2D.IgnoreLayerCollision(IceBlock, enemyLayer, false);
                }
            }
        }

        public int IceBlock { get; set; }

        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 1f);
        }

     
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (isInvincible) 
            {
                return;
            }
        
        }
    }
}


