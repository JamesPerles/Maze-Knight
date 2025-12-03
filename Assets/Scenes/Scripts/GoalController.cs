using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalController : MonoBehaviour
{
    public float range = 5f;
    public string sceneToLoad;  // Expose scene name in Inspector

    private void OnTriggerEnter2D(Collider2D other)
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

            if (!string.IsNullOrEmpty(sceneToLoad))  // Check if scene name is set
            {
                SceneManager.LoadScene(sceneToLoad);
            }
            else
            {
                Debug.LogWarning("Scene name is not set in GoalController!");
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
