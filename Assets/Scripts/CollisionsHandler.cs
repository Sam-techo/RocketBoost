using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionsHandler : MonoBehaviour
{

    float delayTimer = 1f;

    AudioSource audioSource;
    [SerializeField] AudioClip death;
    [SerializeField] AudioClip finish;

    [SerializeField] ParticleSystem deathParticle;
    [SerializeField] ParticleSystem finishParticle;

    bool isTransitioning = false;
    bool collisonDisabled = false;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        DebugKeys();
    }

    void DebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisonDisabled = !collisonDisabled;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (isTransitioning || collisonDisabled)
        {
            return;
        }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("friendly");
                break;
            case "Finish":
                NextLevelSequence();
                break;
            default:
                LoadSequence();
                break;
        }
    }


    void LoadSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(death);
        deathParticle.Play();

        GetComponent<PlayerController>().enabled = false;

        Invoke("ReloadLevel", delayTimer);
    }

    void NextLevelSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(finish);
        finishParticle.Play();

        GetComponent<PlayerController>().enabled = false;

        Invoke("LoadNextLevel", delayTimer);

    }

    void LoadNextLevel()
    {
        
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        
        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
