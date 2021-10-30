using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float thrustPower = 1000.0f;
    [SerializeField] float rotationStrenth = 100.0f;
    Rigidbody rocketRigidbody;
    AudioSource rocketThruster;

    // Start is called before the first frame update
    void Start()
    {
        rocketRigidbody = GetComponent<Rigidbody>();
        rocketThruster = GetComponent<AudioSource>();
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
            // rocketRigidbody.AddForce(Vector3.up);
            rocketRigidbody.AddRelativeForce(Vector3.up * thrustPower * Time.deltaTime);
            if (!rocketThruster.isPlaying)
            {
                rocketThruster.Play();
            }

        }
        else
        {
            rocketThruster.Stop();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationStrenth);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationStrenth);
        }

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

}
