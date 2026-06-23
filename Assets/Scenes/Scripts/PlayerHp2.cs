using UnityEngine;
using UnityEngine.SceneManagement;
    public class PlayerHp2 : MonoBehaviour
    {
        public int maxHP = 3;                   
        public int currentHP;                   
        public float knockbackForce = 10f;      
        Rigidbody2D rb;                 
        Color _originalColor;            
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            currentHP = maxHP;
            _originalColor = GetComponent<SpriteRenderer>().color;
        }
        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))   
            {
                TakeDamage();                   
                Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;
                rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
            }
        }
        void TakeDamage()
        {
            currentHP--;

            if (currentHP == 2)         
            {
                GetComponent<SpriteRenderer>().color = new Color(1f, 0.5f, 0f);   
            }
            else if (currentHP == 1)    
            {
                GetComponent<SpriteRenderer>().color = Color.red;
            }
            if (currentHP <= 0)         
            {
                Destroy(gameObject);
                SceneManager.LoadScene("Game Over 2");
            }
        }
    }
