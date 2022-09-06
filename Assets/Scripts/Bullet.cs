using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
 
    public float speed = 20f;
    public Rigidbody2D rb;

    private float lifetime;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
        lifetime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - lifetime > 5f)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D hitInfo)
    {

        if (hitInfo.name.Contains("Tank"))
        {
            hitInfo.GetComponent<Enemy_tank>().receiveDamage(20);
            Destroy(gameObject);
        }
        if (hitInfo.name.Contains("Penguin"))
        {
            hitInfo.GetComponent<Enemy_penguin>().receiveDamage(25);
            Destroy(gameObject);
        }
    }
}
