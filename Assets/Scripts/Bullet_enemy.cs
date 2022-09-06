using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_enemy : MonoBehaviour
{
    public float speed = 40f;
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
        if (Time.time - lifetime > 15f)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Debug.Log(hitInfo.name);
        
        
        if (hitInfo.name == "Hero")
        {
            hitInfo.GetComponent<Player_controller>().ReceiveDamage(20);
            Destroy(gameObject);
        }
        if(hitInfo.name == "Ground")
        {
            Destroy(gameObject);
        }

    }
}
