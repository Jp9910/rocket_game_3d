using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delay = 2.5f;
    [SerializeField] AudioClip crash;
    [SerializeField] float crashVolume = 0.2f;
    [SerializeField] AudioClip finish;
    [SerializeField] float finishVolume = 0.3f;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem finishParticles;

    AudioSource audsrc;

    bool crashed = false;
    bool won = false;

    // called zero
    void Awake()
    {
        //Debug.Log("Awake");
    }

    // called first
    void OnEnable()
    {
        //Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        audsrc = GetComponent<AudioSource>();
        //Debug.Log(this.name);
        //Debug.Log("OnSceneLoaded: " + scene.name);
        //Debug.Log(mode);
    }

    // called third
    void Start()
    {
        //Debug.Log("Start");
    }

    // called when the game is terminated
    void OnDisable()
    {
        //Debug.Log("OnDisable");
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("this: "+this.name);
        Debug.Log("other: "+other.gameObject.name);
        switch (other.gameObject.tag)
        {
            case "Untagged":
                if(!crashed && !won){
                    crashed = true;
                    startCrashSequence();
                }
                break;
            case "Finish":
                if(!won && !crashed){
                    won = true;
                    finishLevel();
                }
                break;
            default:
                break;
        }
    }

    private void startCrashSequence()
    {
        //audsrc = GetComponent<AudioSource>();
        GetComponent<RocketMovement>().enabled = false;
        audsrc.Stop();
        audsrc.PlayOneShot(crash,crashVolume);
        Debug.Log(crashParticles);
        crashParticles.Play();
        Invoke("reloadLevel",delay);
    }

    private void finishLevel()
    {
        //audsrc = GetComponent<AudioSource>();
        GetComponent<RocketMovement>().enabled = false;
        audsrc.Stop();
        audsrc.PlayOneShot(finish,finishVolume);
        finishParticles.Play();
        Invoke("loadNextLevel",delay);
    }
    
    private void reloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //scene name (string) or scene buildindex (int)
    }

    private void loadNextLevel()
    {
        if(SceneManager.GetActiveScene().buildIndex+1 == SceneManager.sceneCountInBuildSettings){
            Debug.Log("GG!");
            SceneManager.LoadScene(0);
            return;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
