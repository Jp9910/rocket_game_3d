using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 150;
    [SerializeField] float rotatingSpeed = 100;
    [SerializeField] AudioClip engineSound;
    [SerializeField] float engineSoundVolume = 0.4f;
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
        if(Input.GetKey(KeyCode.W)){
            rb.AddRelativeForce(Vector3.up*movementSpeed*deltaTime); //new Vector3(0,1,0)
            if(!audiosrc.isPlaying)
                audiosrc.PlayOneShot(engineSound,engineSoundVolume);
        }
        else
            audiosrc.Stop();
    }

    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
            ApplyRotation(rotatingSpeed);
        if(Input.GetKey(KeyCode.D))
            ApplyRotation(-1*rotatingSpeed);
        if(Input.GetKeyDown(KeyCode.R))
            ;//transform.GetComponent<Position>();
    }

    private void ApplyRotation(float rotation)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward*rotation*deltaTime);
        rb.freezeRotation = false;
    }
}
