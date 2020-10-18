using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    // config
    [SerializeField] float reloadDelay = 1f;

    private void OnCollisionEnter(Collision other) 
    {
        switch(other.gameObject.tag)
        {
            case "Friendly":
                print("Friendly...");
                break;
            
            case "Finish":
                DisableMovementAndActionSequence("LoadNextLevel");
                break;

            default:
                DisableMovementAndActionSequence("ReloadLevel");
                break;
        }    
    }

    private void DisableMovementAndActionSequence(string action)
    {
        DisableMovement();
        Invoke(action, reloadDelay);
    }

    private void DisableMovement()
    {
        GetComponent<Movement>().enabled = false;
    }

    private void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        
        SceneManager.LoadScene(nextSceneIndex == SceneManager.sceneCountInBuildSettings ? 0 : nextSceneIndex);
    }

    private void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
