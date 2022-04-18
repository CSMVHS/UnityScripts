using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchProjectile : MonoBehaviour
{
    public GameObject projectile;
    public float launchVelocity = 700f;

    Vector3 launcher;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject ball = Instantiate(projectile, transform.position, transform.rotation);
            ball.GetComponent<Rigidbody>().AddRelativeForce(new Vector3 (0, launchVelocity, 0));
        }

        launcher = transform.localPosition;
        launcher.x += Input.GetAxis("Horizontal") * Time.deltaTime * 10;  
        launcher.y += Input.GetAxis("Vertical") * Time.deltaTime * 10;  
        transform.localPosition = launcher;
    }
}
