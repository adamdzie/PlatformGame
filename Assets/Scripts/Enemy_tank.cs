using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_tank : MonoBehaviour
{
    public float movement_speed = 25.0f;
    public int health = 100;
    public Transform CenterOfMap;
    public Transform hero;
    public Bullet_enemy bullet_enemy;
    public Bullet_enemy bullet_enemy2;
    public Transform shooting_pos;
    public Transform shooting_pos2;
    private float state_time;
    private Animator animator;
    private int state;
    private bool istrig;
    private bool istrig2;
    private float last_state_time;
    private float shoot_time_1;
    private float last_shoot_time_1;
    private float shoot_time_2;
    private float last_shoot_time_2;
    public Text TankText;




    // Start is called before the first frame update
    void Start()
    {
       
        CenterOfMap = GameObject.FindGameObjectWithTag("MapCenter").transform.GetComponent<Transform>();
        hero = GameObject.Find("Hero").transform.GetComponent<Transform>();

        last_state_time = Time.time;
        state_time = 5;
        istrig = false;
        istrig2 = false;
        last_shoot_time_1 = Time.time;
        shoot_time_1 = randomizeShootTime(3f, 6f);
        last_shoot_time_2 = Time.time;
        shoot_time_2 = randomizeShootTime(1f, 4f);
        state = 0;
        animator = GetComponent<Animator>();
        if(gameObject.transform.position.x < CenterOfMap.position.x)
        {
            
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else transform.localRotation = Quaternion.Euler(0, 180, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (last_state_time + state_time <= Time.time)
        {
            last_state_time = Time.time;
            state = randomizeState();
            Debug.Log(state);
        }
        if(state == 1)
        {
            move();
        }
        if(state == 2)
        {
            animator.SetBool("Is_Moving", false);
        }

        if (last_shoot_time_1 + shoot_time_1 <= Time.time)
        {
            last_shoot_time_1 = Time.time;
            shoot_time_1 = randomizeShootTime(5f, 10f);
            istrig = true;
        }
        if (last_shoot_time_2 + shoot_time_2 <= Time.time)
        {
            last_shoot_time_2 = Time.time;
            shoot_time_2 = randomizeShootTime(1f, 4f);
            istrig2 = true;
        }

        if (istrig)
        {
            shoot1();
            istrig = false;
        }
        if (istrig2)
        {
            shoot2();
            istrig2 = false;
        }

    }
    private int randomizeState()
    {
        return Random.Range(1, 3);
    }
    private float randomizeShootTime(float down,float up)
    {
        return Random.Range(down, up);
    }
    private void shoot1()
    {
        Instantiate(bullet_enemy, shooting_pos.position, shooting_pos.rotation);
        
        Debug.Log("INSTANTIATED");
    }
    private void shoot2()
    {
        Instantiate(bullet_enemy2, shooting_pos2.position, shooting_pos2.rotation);
    }
    private void move()
    {
        float movement;
        if (gameObject.transform.position.x <= hero.position.x)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            movement = 1f;
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            movement = -1f;
        }
        
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * movement_speed;
        animator.SetBool("Is_Moving", true);
    }
    public void receiveDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
            int T = int.Parse(TankText.text);
            T = T + 1;
            string T1 = T.ToString();
            TankText.text =T1;


        }
    }
}
