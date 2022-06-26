using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cheats : MonoBehaviour
{
    
    Rigidbody rb;
    bool collisionsDisabled = false;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        CheatKeys();
    }

    void CheatKeys()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            // Load next level.
            loadNextLevel();
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            // Disable collisions.
            if(!collisionsDisabled)
                disableCollisions();
            else
                enableCollisions();
        }
    }

    void loadNextLevel()
    {
        if(SceneManager.GetActiveScene().buildIndex+1 == SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(0);
            return;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    void disableCollisions()
    {
        var watch = System.Diagnostics.Stopwatch.StartNew();
        
        BoxCollider[] boxColliders = GetComponentsInChildren<BoxCollider>();
        CapsuleCollider[] capsuleColliders = GetComponentsInChildren<CapsuleCollider>();
        foreach (BoxCollider boxCollider in boxColliders)
        {
            boxCollider.enabled = false;
        }

        foreach (CapsuleCollider capsuleCollider in capsuleColliders)
        {
            capsuleCollider.enabled = false;
        }

        collisionsDisabled = true;
        
        watch.Stop();
        var elapsedMs = watch.ElapsedMilliseconds;
        Debug.Log("Disable Collisions time: "+elapsedMs);
    }

    void enableCollisions()
    {
        BoxCollider[] boxColliders = GetComponentsInChildren<BoxCollider>();
        CapsuleCollider[] capsuleColliders = GetComponentsInChildren<CapsuleCollider>();
        foreach (BoxCollider boxCollider in boxColliders)
        {
            boxCollider.enabled = true;
        }

        foreach (CapsuleCollider capsuleCollider in capsuleColliders)
        {
            capsuleCollider.enabled = true;
        }

        collisionsDisabled = false;
    }
}
