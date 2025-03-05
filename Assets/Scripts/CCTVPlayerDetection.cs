using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCTVPlayerDetection : MonoBehaviour
{

    private GameObject player;
    private LastPlayerSighting lastPlayerSighting;
    // Start is called before the first frame update
    void Start()
    {
        player=GameObject.FindGameObjectWithTag(Tags.Player);
        lastPlayerSighting=GameObject.FindGameObjectWithTag(Tags.GameController).GetComponent<LastPlayerSighting>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject==player)
        {
            Vector3 relPlayerPos=player.transform.position-transform.position;
            RaycastHit hit;
            if(Physics.Raycast(transform.position,relPlayerPos,out hit))
            {
                if(hit.collider.gameObject==player)
                {
                    lastPlayerSighting.position=player.transform.position;
                }
            }
        }
    }
}
