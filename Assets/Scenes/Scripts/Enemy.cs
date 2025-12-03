using Scenes.Scripts;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHP = 3;                   
    public int currentHP;                   
    public float knockbackForce = 10f;      

    public Rigidbody2D rb;                 
    private SpriteRenderer spriteRenderer;  

   
    private Color originalColor;
    private Color damagedColor;
    private Color criticallyDamagedColor;

  
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHP = maxHP;
        spriteRenderer = GetComponent<SpriteRenderer>();

       
        originalColor = spriteRenderer.color;
        damagedColor = new Color(1f, 0.5f, 0f);   // Orange
        criticallyDamagedColor = Color.red;
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("FireBall"))   
        {
            FireBall fireBall = collision.gameObject.GetComponent<FireBall>();
            if (fireBall != null)     
            {
                TakeDamage(fireBall.damage);                    
                Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;
                rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
            }
        }
    }

   
    public void TakeDamage(int damageAmount)
    {
        currentHP -= damageAmount;

        if (currentHP == 2)        
        {
            spriteRenderer.color = damagedColor;
        }
        else if (currentHP == 1)    
        {
            spriteRenderer.color = criticallyDamagedColor;
        }

        if (currentHP <= 0)        
        {
            Destroy(gameObject);
        }
    }
}
