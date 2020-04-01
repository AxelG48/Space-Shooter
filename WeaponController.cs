using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private AudioSource audioSource;
    public Transform shotSpawn;
    public float delay;
    public GameObject shot;
    public float fireRate;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("Fire", delay, fireRate);
    }
    void Update()
    {
        
    }
    void Fire ()
    {
        audioSource.Play();
        Instantiate(shot,shotSpawn.position, shotSpawn.rotation);
    }
}
