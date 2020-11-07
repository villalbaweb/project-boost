using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    // config
    [SerializeField] float reloadDelay = 1f;
    [SerializeField] AudioClip successSfx = null;
    [SerializeField] AudioClip crashSfx = null;

    [SerializeField] ParticleSystem successPartycles = null;
    [SerializeField] ParticleSystem crashPartycles = null;

    // cache 
    AudioSource _audioSource;

    // state
    bool isTransitioning;
    bool areCollisionsDisabled;

    private void Awake() 
    {
        _audioSource = GetComponent<AudioSource>();    
    }

    private void Update() 
    {
        ProcessEntries();
    }

    private void ProcessEntries()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            areCollisionsDisabled = !areCollisionsDisabled;
        }
    }

    private void OnCollisionEnter(Collision other) 
    {
        if(isTransitioning || areCollisionsDisabled) return;

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
        isTransitioning = true;
        PlayParticles(action == "LoadNextLevel" ? successPartycles : crashPartycles);
        PlaySFX(action == "LoadNextLevel" ? successSfx : crashSfx);
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

    private void PlaySFX(AudioClip audioClip)
    {
        _audioSource.Stop();
        _audioSource.PlayOneShot(audioClip);
    }

    private void PlayParticles(ParticleSystem partycleSystem)
    {
        partycleSystem.Play();
    }
}
