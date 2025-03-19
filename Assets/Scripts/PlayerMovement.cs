using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public AudioClip shoutingClip;
    public float turnSmoothing = 15f;//转向平滑度
    public float speedDampTime = 0.1f;
    private Animator animator;
    private HashIDs hash;
    void Awake()
    {
        animator = GetComponent<Animator>();
        hash = GameObject.FindGameObjectWithTag("GameController").GetComponent<HashIDs>();
        animator.SetLayerWeight(1, 1f);
    }

    void Update()
    {
        bool shout = Input.GetButtonDown("Attract");
        animator.SetBool(hash.shoutingBool, shout);
        AudioManagement(shout);
    }
    void Rotating(float h, float v)
    {
        Vector3 targetDirection = new Vector3(h, 0f, v);
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        
        Rigidbody r = GetComponent<Rigidbody>();
        Quaternion newRotation = Quaternion.Lerp(r.rotation, targetRotation, turnSmoothing * Time.deltaTime);
        r.MoveRotation(newRotation); //这里是直接改变角色的旋转角度
    }
    
    void MovementManagement(float h,float v, bool sneaking)
    {
        // 先设置动画的参数
        // 如果存在水平或者垂直输入，那么就旋转角色，然后设置动画的速度参数
        // 不存在水平或者垂直输入，那么就设置动画的速度参数为0
       animator.SetBool(hash.sneakingBool, sneaking);
       if(h != 0f || v != 0f)
       {
           Rotating(h, v);
           animator.SetFloat(hash.speedFloat, 5.5f, speedDampTime, Time.deltaTime);
       }
       else
       {
           animator.SetFloat(hash.speedFloat, 0f);
       }
    }

    void FixedUpdate()
    {
    
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        bool sneak = Input.GetButton("Sneak");
        Debug.Log("h: " + h + " v: " + v + " sneak: " + sneak);
        MovementManagement(h, v, sneak);
    }

    void AudioManagement(bool shout)
    {
        AudioSource audioSource = GetComponent<AudioSource>();

        // 如果当前的动画状态是在跑步，那么就播放声音 animator.GetCurrentAnimatorStateInfo(0).nameHash == hash.locomotionState这个过时了
        if(animator.GetCurrentAnimatorStateInfo(0).fullPathHash == hash.locomotionState)
        {
            if(!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
        if(shout)
        {
            // 开始叫的声音
            AudioSource.PlayClipAtPoint(shoutingClip, transform.position);
        }
    }

}

