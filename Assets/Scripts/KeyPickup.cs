using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour {
    public AudioClip keyGrab;
    GameObject player;
    PlayerInventory playerInventory;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if(player == null)
        {
            Debug.LogError("没有找到玩家对象");
        }
        else
        {
            Debug.Log("找到玩家对象");
        }
        playerInventory = player.GetComponent<PlayerInventory>();
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("捡钥匙");
        if (other.gameObject==player)
        {
            AudioSource.PlayClipAtPoint(keyGrab, transform.position);
            playerInventory.hasKey = true;
            Destroy(gameObject);
        }
    }
}
