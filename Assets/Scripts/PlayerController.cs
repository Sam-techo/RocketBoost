using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody playerRb;
    AudioSource audioSource;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem boostParticle;
    [SerializeField] ParticleSystem leftThrusterParticle;
    [SerializeField] ParticleSystem rightThrusterParticle;

    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 100f;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            playerRb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }
            if (!boostParticle.isPlaying)
            {
                boostParticle.Play();
            }

        }
        else
        {
            audioSource.Stop();
            boostParticle.Stop();
        }

    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationThrust);
            if (!leftThrusterParticle.isPlaying)
            {
                leftThrusterParticle.Play();
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationThrust);
            if (!rightThrusterParticle.isPlaying)
            {
                rightThrusterParticle.Play();
            }
        }
        else
        {
            leftThrusterParticle.Stop();
            rightThrusterParticle.Stop();
        }
    }

    void ApplyRotation(float thrust)
    {
        playerRb.freezeRotation = true; 
        transform.Rotate(Vector3.forward * Time.deltaTime * thrust);
        playerRb.freezeRotation = false;
    }
}
