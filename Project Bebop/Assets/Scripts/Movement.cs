using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float thrustPower = 1000.0f;
    [SerializeField] float rotationStrenth = 100.0f;
    // Audio clips
    [SerializeField] AudioClip thrusterClip;
    [SerializeField] ParticleSystem mainBoosterParticles;
    [SerializeField] ParticleSystem leftBooster1Particles;
    [SerializeField] ParticleSystem leftBooster2Particles;
    [SerializeField] ParticleSystem rightBooster1Particles;
    [SerializeField] ParticleSystem rightBooster2Particles;
    Rigidbody rocketRigidbody;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rocketRigidbody = GetComponent<Rigidbody>();
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
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else
        {
            StopRightBoosterParticles();
        }
        if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopLeftBoosterParticles();
        }
    }

    private void StartThrusting()
    {
        // rocketRigidbody.AddForce(Vector3.up);
        rocketRigidbody.AddRelativeForce(Vector3.up * thrustPower * Time.deltaTime);
        if (!mainBoosterParticles.isPlaying)
        {
            mainBoosterParticles.Play();
        }
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(thrusterClip);
        }
    }

    private void StopThrusting()
    {
        audioSource.Stop();
        mainBoosterParticles.Stop();
    }

    private void RotateLeft()
    {
        ApplyRotation(rotationStrenth);
        FireRightBoosterParticles();
    }

    private void RotateRight()
    {
        ApplyRotation(-rotationStrenth);
        FireLeftBoosterParticles();
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rocketRigidbody.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rocketRigidbody.constraints =
            RigidbodyConstraints.FreezeRotationX | // freezing rotation on the X
            RigidbodyConstraints.FreezeRotationY | // freezing rotation on the Y
            RigidbodyConstraints.FreezePositionZ; // freezing rotation on the Z
    }

    private void FireLeftBoosterParticles()
    {
        if (!leftBooster1Particles.isPlaying && !leftBooster2Particles.isPlaying)
        {
            leftBooster1Particles.Play();
            leftBooster2Particles.Play();
        }
    }

    private void StopLeftBoosterParticles()
    {
        leftBooster1Particles.Stop();
        leftBooster2Particles.Stop();
    }

    private void FireRightBoosterParticles()
    {
        if (!rightBooster1Particles.isPlaying && !rightBooster2Particles.isPlaying)
        {
            rightBooster1Particles.Play();
            rightBooster2Particles.Play();
        }
    }

    private void StopRightBoosterParticles()
    {
        rightBooster1Particles.Stop();
        rightBooster2Particles.Stop();
    }

}
