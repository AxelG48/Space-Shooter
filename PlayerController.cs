using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}
public class PlayerController : MonoBehaviour
{
    public float speed;
    public float tilt;
    public Boundary boundary;
    public Transform shotSpawn;
    public GameObject shot;

    private Rigidbody rb;
    public float fireRate;
    private float nextFire;
    public AudioSource sound;
    public bool powerup;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            if(powerup == false)
            {
                nextFire = Time.time + fireRate;
                // GameObject clone = 
                Instantiate(shot, shotSpawn.position, shotSpawn.rotation);// as GameObject
                sound.Play();
            }
            else
            {
                nextFire = Time.time + fireRate;
                Instantiate(shot, shotSpawn.position + new Vector3(1,0,0), shotSpawn.rotation);
                Instantiate(shot, shotSpawn.position + new Vector3(-1, 0, 0), shotSpawn.rotation);
                Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
                nextFire = Time.time + fireRate;
            }

        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.velocity = movement * speed;

        rb.position = new Vector3
        (
             Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
             0.0f,
             Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        );

        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    }
    public IEnumerator PowerUp()
    {
        powerup = true;
        yield return new WaitForSeconds(3);
        powerup = false;
    }
}

