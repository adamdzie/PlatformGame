using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    
    public Transform firePoint;
    public GameObject bullet;
    private Vars vars;

    private float time_shoot;
    // Update is called once per frame
    void Start()
    {
        time_shoot = 0f;
        vars = GameObject.FindGameObjectWithTag("Variables").GetComponent<Vars>();
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && PauseMenu.gameIsPaused == false && Time.time-time_shoot > vars.shooting_speed)
        {
            time_shoot = Time.time;
            Shoot();
        }
    }
    void Shoot()
    {
        Instantiate(bullet, firePoint.position,firePoint.rotation);
    }
}
