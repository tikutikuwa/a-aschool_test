using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody rbody;
    public float bulletSpeed;

    void Start()
    {
        rbody.AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);
        Destroy(gameObject, 5);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerController>().OnAttacked();
        }
    }
}
