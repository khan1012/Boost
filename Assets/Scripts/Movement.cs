using System;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rigidbody;
    AudioSource audioSource;
    [SerializeField] int mainThrust = 1000;
    [SerializeField] int tailThrust = 100;
    [SerializeField] AudioClip thrustAudio;
    [SerializeField] ParticleSystem mainThrustParticles;
    [SerializeField] ParticleSystem leftThrustParticles;
    [SerializeField] ParticleSystem rightThrustParticles;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyMainTrust();
        }
        else
        {
            StopMainThrust();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            ApplyRightThrust();
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            ApplyLeftThrust();
            RotateRight();
        }
        else
        {
            StopSideThrust();
        }
    }

    void ApplyMainTrust()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(thrustAudio);
        }
        mainThrustParticles.Play();
        rigidbody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
    }

    void StopMainThrust()
    {
        mainThrustParticles.Stop();
        audioSource.Stop();
    }

    void ApplyRightThrust()
    {
        rightThrustParticles.Play();
    }

    void ApplyLeftThrust()
    {
        leftThrustParticles.Play();
    }

    void StopSideThrust()
    {
        leftThrustParticles.Stop();
        rightThrustParticles.Stop();
    }

    void RotateRight()
    {
        ApplyRotation(-tailThrust);
    }

    void RotateLeft()
    {
        ApplyRotation(tailThrust);
    }

    void ApplyRotation(int rotationThrust)
    {
        rigidbody.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThrust * Time.deltaTime);
        rigidbody.freezeRotation = false;
    }
}
