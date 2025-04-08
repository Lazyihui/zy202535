using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100;
    public float resetAfterDeathTime = 5;
    public AudioClip deathClip;
    Animator anim;
    PlayerMovement playerMovement;
    HashIDs hash;
    SceneFadeInOut sceneFadeInOut;
    LastPlayerSighting lastPlayerSighting;
    float timer;
    bool playerDead;

    void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        hash = GameObject.FindWithTag(Tags.GameController).GetComponent<HashIDs>();
        if(hash == null)
        {
            Debug.LogError("HashIDs component not found on " + gameObject.name);
        }
        sceneFadeInOut = GameObject.FindWithTag(Tags.Fader).GetComponent<SceneFadeInOut>();
        if(sceneFadeInOut == null)
        {
            Debug.LogError("SceneFadeInOut component not found on " + gameObject.name);
        }
        lastPlayerSighting = GameObject.FindWithTag(Tags.GameController).GetComponent<LastPlayerSighting>();
        if(lastPlayerSighting == null)
        {
            Debug.LogError("LastPlayerSighting component not found on " + gameObject.name);
        }
    }

    void Update()
    {
        float dt = Time.deltaTime;
        if (health <= 0)
        {
            if (!playerDead)
            {
                PlayerDying();
            }
            else
            {
                PlayerDead();
                LevelReset(dt);
            }
        }
    }

    void PlayerDying()
    {
        playerDead = true;
        anim.SetBool(hash.deadBool, playerDead);
        AudioSource.PlayClipAtPoint(deathClip, transform.position);
    }

    void PlayerDead()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).fullPathHash == hash.dyingState)
        {
            anim.SetBool(hash.deadBool, false);
        }
        anim.SetFloat(hash.speedFloat, 0);
        playerMovement.enabled = false;
        lastPlayerSighting.position = lastPlayerSighting.resetPosition;
        GetComponent<AudioSource>().Stop();
    }

    void LevelReset(float dt)
    {
        timer += dt;
        if (timer >= resetAfterDeathTime)
        {
            sceneFadeInOut.EndScene(dt);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}