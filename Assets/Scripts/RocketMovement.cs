using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 80;
    [SerializeField] float rotatingSpeed = 100;
    [SerializeField] AudioClip engineSound;
    [SerializeField] float engineSoundVolume = 0.4f;
    [SerializeField] ParticleSystem boosterParticles;

    Rigidbody rb;
    AudioSource audiosrc;
    float deltaTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audiosrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        deltaTime = Time.deltaTime;
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            startThursting();
        }
        else
        {
            stopThrusting();
        }
    }

    private void startThursting()
    {
        boosterParticles.Play();
        rb.AddRelativeForce(new Vector3(0, 1, 0) * movementSpeed * deltaTime); //new Vector3(0,1,0)
        if (!audiosrc.isPlaying)
            audiosrc.PlayOneShot(engineSound, engineSoundVolume);
    }

    private void stopThrusting()
    {
        audiosrc.Stop();
    }

    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
            ApplyZRotation(rotatingSpeed);
        if(Input.GetKey(KeyCode.D))
            ApplyZRotation(-1*rotatingSpeed);
        if(Input.GetKey(KeyCode.W))
            ApplyXRotation(rotatingSpeed);
        if(Input.GetKey(KeyCode.S))
            ApplyXRotation(-1*rotatingSpeed);
        // if(Input.GetKey(KeyCode.E))
        //     ApplyYRotation(rotatingSpeed);
        // if(Input.GetKey(KeyCode.Q))
        //     ApplyYRotation(-1*rotatingSpeed);
        // if(Input.GetKeyDown(KeyCode.R))
        //     ;//transform.GetComponent<Position>();
    }

    private void ApplyZRotation(float rotation)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward*rotation*deltaTime);
        rb.freezeRotation = false;
    }

    private void ApplyXRotation(float rotation)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.right*rotation*deltaTime);
        rb.freezeRotation = false;
    }

    private void ApplyYRotation(float rotation)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.up*rotation*deltaTime);
        rb.freezeRotation = false;
    }
}
