using UnityEngine;
using UnityEngine.SceneManagement;
public class Restart2 : MonoBehaviour
{
    public string sceneToLoad;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!string.IsNullOrEmpty(sceneToLoad))
            {
                SceneManager.LoadScene(sceneToLoad);
            }
            else
            {
                Debug.LogWarning("Scene name is not set in Restart script!");
            }
        }
    }
}