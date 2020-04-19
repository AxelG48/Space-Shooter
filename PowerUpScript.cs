using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScript : MonoBehaviour
{
    PlayerController playerscript;
    private void Start()
    {
        playerscript = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerscript.StartCoroutine("PowerUp");
            Destroy(this.gameObject);
        }
    }
}
