using System;
using UnityEngine;
using UnityEngine.UI;

public class LaserSwitchDeactivation : MonoBehaviour
{
    public GameObject laser;
    public Material unlockedMat;
    public GameObject player;

    void LaserDeactivation()
    {
        laser.SetActive(false);
        Renderer screen = transform.Find("prop_switchUnit_screen").GetComponent<Renderer>();
        screen.material = unlockedMat;
        GetComponent<AudioSource>().Play();
        Debug.Log("Laser deactivated.");
    }


    void OnTriggerStay(Collider other)
    {
        if (other.gameObject== player)
        {
            Debug.Log("Player entered the trigger zone.");
            if (Input.GetButton("Switch"))
            {
                LaserDeactivation();
                Debug.Log("Laser deactivated.");
            }
        }
    }
}
