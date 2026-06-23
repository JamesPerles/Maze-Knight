using UnityEngine;
public class SniperEnemy : MonoBehaviour
{
    public float detectionRange = 10f;
    public float shootingRange = 7f;
    public GameObject projectilePrefab;
    public Transform shootPoint;
    public float shootingInterval = 2f;
    public float moveSpeed = 3f;
    public float stoppingDistance = 5f;
    public float projectileSpeed = 10f;
    public float projectileLifetime = 3f;
    public Animator animator;
    public AudioClip shootSound;
    static readonly int Shoot = Animator.StringToHash("Shoot");
    static readonly int ShooterEnemyRun = Animator.StringToHash("ShooterEnemyRun");
    Transform playerTransform;
    bool isShooting = false;
    AudioSource audioSource;
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
        Vector2 directionToPlayer = (playerTransform.position - transform.position).normalized;
        if (distanceToPlayer < stoppingDistance)
        {
            transform.Translate(-directionToPlayer * (moveSpeed * Time.deltaTime));
            if (animator != null)
            {
                animator.SetBool(ShooterEnemyRun, true);
            }
        }
        else
        {
            if (animator != null)
            {
                animator.SetBool(ShooterEnemyRun, false);
            }
            if (directionToPlayer.x > 0)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z); // Face right
            }
            else
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z); // Face left
            }
            if (distanceToPlayer <= detectionRange)
            {
                if (distanceToPlayer <= shootingRange && !isShooting)
                {
                    isShooting = true;
                    ShootAtPlayer(distanceToPlayer);
                }
                else
                {
                    if (distanceToPlayer > stoppingDistance)
                    {
                        transform.Translate(directionToPlayer * (moveSpeed * Time.deltaTime));
                    }
                }
            }
        }
    }
    void ShootAtPlayer(float distanceToPlayer)
    {
        if (animator != null)
        {
            animator.SetTrigger(Shoot);
        }
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
        Vector2 direction = (playerTransform.position - shootPoint.position).normalized;
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction * projectileSpeed;
        }
        Destroy(projectile, projectileLifetime); 
        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
        Invoke("ResetShooting", shootingInterval);
    }
    void ResetShooting()
    {
        isShooting = false;
    }
}
