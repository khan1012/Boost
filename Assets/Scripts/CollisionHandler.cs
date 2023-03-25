using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    AudioSource audioSource;
    [SerializeField] AudioClip crashAudio;
    [SerializeField] AudioClip successAudio;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    float delayInSeconds = 2f;
    bool isTransitioning = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning)
        {
            return;
        }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                HandleFriendlyCollision();
                break;
            case "Finish":
                HandleFinish();
                break;
            default:
                HandleCrash();
                break;

        }
    }

    void HandleFriendlyCollision()
    {
        //Do Nothing
    }

    void HandleFinish()
    {
        isTransitioning = true;
        PlaySuccessSound();
        PlaySuccessParticles();
        DisableMovement();
        Invoke("LoadNextLevel", delayInSeconds);
    }

    void HandleCrash()
    {
        isTransitioning = true;
        DisableMovement();
        PlayCrashSound();
        PlayCrashParticles();
        Invoke("ReloadLevel", delayInSeconds);
    }

    void PlaySuccessSound()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(successAudio);
    }

    void PlayCrashSound()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(crashAudio);
    }

    void PlaySuccessParticles()
    {
        successParticles.Play();
    }

    void PlayCrashParticles()
    {
        crashParticles.Play();
    }

    void DisableMovement()
    {
        GetComponent<Movement>().enabled = false;
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);
    }
}
