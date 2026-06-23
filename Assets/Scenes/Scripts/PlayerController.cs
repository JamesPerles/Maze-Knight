using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public float knockbackForce = 10f;         
    public float knockbackDuration = 0.5f;    
    public float invincibleDuration = 2f;      
    Rigidbody2D rb;                    
    bool isInvincible = false;          
    float invincibleTimer = 0f;         
    bool isKnockedBack = false;         
    float knockbackTimer = 0f;          
    Vector2 knockbackDirection;         
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (isInvincible)
        {
            invincibleTimer += Time.deltaTime;
            if (invincibleTimer >= invincibleDuration)
            {
                isInvincible = false;
                invincibleTimer = 0f;
                GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                GetComponent<SpriteRenderer>().enabled = !GetComponent<SpriteRenderer>().enabled;
            }
        }
        if (isKnockedBack)
        {
            knockbackTimer += Time.deltaTime;

            if (knockbackTimer >= knockbackDuration)
            {
                isKnockedBack = false;
                knockbackTimer = 0f;
            }
            else
            {
                rb.linearVelocity = knockbackDirection * knockbackForce;
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))   
        {
            if (!isInvincible)                           
            {
                isInvincible = true;                   
                knockbackDirection = (transform.position - collision.transform.position).normalized; 
                isKnockedBack = true;                    
            }
        }
    }
}

