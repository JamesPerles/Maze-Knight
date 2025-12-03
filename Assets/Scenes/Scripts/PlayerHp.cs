using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes.Scripts
{
    public class PlayerHp : MonoBehaviour
    {
        public int maxHP = 3;
        public int currentHP;
        public float knockbackForce = 10f;
        public string gameOverScene;  // Expose scene name in Inspector

        private Rigidbody2D rb;
        private Color _originalColor;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            currentHP = maxHP;
            _originalColor = GetComponent<SpriteRenderer>().color;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                TakeDamage();
                Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;
                rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
            }
        }

        private void TakeDamage()
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
                if (!string.IsNullOrEmpty(gameOverScene))  // Check if scene name is set
                {
                    SceneManager.LoadScene(gameOverScene);
                }
                else
                {
                    Debug.LogWarning("Game over scene is not set in PlayerHp script!");
                }
            }
        }
    }
}

