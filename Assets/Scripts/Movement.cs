using UnityEngine;
using UnityEngine.InputSystem; 

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float thrustStrength = 100f;
    [SerializeField] float rotationStrength = 100f;
    [SerializeField] AudioClip mainEngineSFX;
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem rightThrustParticles;
    [SerializeField] ParticleSystem leftThrustParticles;

   
    Rigidbody rb;
    AudioSource audioSource;

   
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }

    void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }



    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngineSFX);
        }
        if (!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();
        }
    }

    private void StopThrusting()
    {
        audioSource.Stop();
        mainEngineParticles.Stop();
    }

    private void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();
        if (rotationInput < 0)
        {
            RotateRight();
        }
        else if (rotationInput > 0)
        {
            RotateLeft();
        }
        else
        {
            StopRotating();
        }
    }


    private void RotateRight()
    {
        ApplyRotation(rotationStrength);

        if (!rightThrustParticles.isPlaying)
        {
            leftThrustParticles.Stop();
            rightThrustParticles.Play();
        }
    }

    private void RotateLeft()
    {
        ApplyRotation(-rotationStrength);

        if (!leftThrustParticles.isPlaying)
        {
            rightThrustParticles.Stop();
            leftThrustParticles.Play();
        }
    }

    private void StopRotating()
    {
        rightThrustParticles.Stop();
        leftThrustParticles.Stop();
    }


    private void ApplyRotation(float ratationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * ratationThisFrame * Time.fixedDeltaTime);
        rb.freezeRotation = false;
    }
}
