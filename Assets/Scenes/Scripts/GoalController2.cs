using UnityEngine;
using UnityEngine.SceneManagement;
public class GoalController2 : MonoBehaviour
{
    public float range = 5f;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, range);
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Enemy"))
                {
                    this.enabled = false;
                    return;
                }
            }
            SceneManager.LoadScene("WinScene");
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
