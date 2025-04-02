using UnityEngine;
using UnityEngine.InputSystem; 

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float thrustStrength = 100f;
    [SerializeField] float rotationStrength = 100f;

   
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
            rb.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else 
        {
            audioSource.Stop();
        }
    }

    private void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();
        if (rotationInput < 0)
        {
            ApplyRotation(rotationStrength);
        }
        if (rotationInput > 0)
        {
            ApplyRotation(-rotationStrength);  
        }
    }

    private void ApplyRotation(float ratationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * ratationThisFrame * Time.fixedDeltaTime);
        rb.freezeRotation = false;
    }
}
