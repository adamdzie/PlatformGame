using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Player_controller : MonoBehaviour
{
    public float movement_speed = 25.0f;
    public float jump_force = 25.0f;
    public float stance_time = 2f;
    public float shooting_speed = 0.5f;
    public int max_health = 100;
    public int current_health;

    private Rigidbody2D hero_body;
    private Animator animator;
    private float movement;
    public GameObject pointTrans;
    private Vector2 screenPosition;
    private Vector2 worldPosition;
    private float time_stance;
    private bool is_croach;

    private Vars vars;
    private GameObject wave_info;
    private float time_shoot;
    private bool right_pressed;
    private bool left_pressed;
    private bool fresh;

    public Medical medical;

    public GameObject startMapPos;
    public GameObject endMapPos;
    public GameObject medical_spawn;
    private float medical_time_spawn;
    private float last_medical_spawn;

    private HealthBar health_bar_instance;
    private float startTime;
    public HealthBar healthBar;
    public GameObject panelek;
    public Text GameOver;
    private bool is_dead;
    public static Player_controller instance;
    public void ReceiveDamage(int dmg)
    {
        current_health -= dmg;
        healthBar.SetHealth(current_health);
    }
    void Awake()
    {
        instance = this;
        panelek = GameObject.Find("Panel_essa");
        GameOver = GameObject.Find("GameOver").GetComponent<Text>();
        GameOver.text = "";
        panelek.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        health_bar_instance = HealthBar.instance;
        medical_time_spawn = 10f;
        last_medical_spawn = Time.time;

        startTime = Time.time;
        is_dead = false;
        wave_info = GameObject.Find("Wave_info");
        vars = GameObject.FindGameObjectWithTag("Variables").GetComponent<Vars>();
        hero_body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        time_stance = Time.time;
        is_croach = false;
        fresh = false;
        time_shoot = 0.0f;
        current_health = max_health;
        healthBar.SetMaxHealth(max_health);
    }

    void Update()
    {

        if (last_medical_spawn + medical_time_spawn <= Time.time)
        {
            last_medical_spawn = Time.time;
            float spawn_pos_x = Random.Range(startMapPos.transform.position.x, endMapPos.transform.position.x);
            float spawn_pos_y = 100f;
            float spawn_pos_z = 0;
            medical_spawn.transform.SetPositionAndRotation(new Vector3(spawn_pos_x, spawn_pos_y, spawn_pos_z), Quaternion.identity);
            Instantiate(medical, medical_spawn.transform.position, medical_spawn.transform.rotation);
        }

        if (current_health <= 0)
        {
            wave_info.SetActive(false);
            panelek.SetActive(true);
            GameOver.text = "Game Over";
            if(is_dead == false)
            {
                startTime = Time.time;
                is_dead = true;
            }
            
            if(startTime + 4f < Time.time)
            {
                SceneManager.LoadScene("Menu");
            }
            
        }

        // Horizontal position
        
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            fresh = true;
            left_pressed = true;
            movement = -1.0f;
            
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow) )
        {
            fresh = false;
            left_pressed = false;
            movement = 0.0f;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            fresh = false;
            right_pressed = true;
            movement = 1.0f;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            fresh = true;
            right_pressed = false;
            movement = 0.0f;
        }

        if(right_pressed)
        {
            if (!fresh) movement = 1.0f;
        }
        if (left_pressed)
        {
            if (fresh) movement = -1.0f;
        }
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * movement_speed;

        //Jump 

        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(hero_body.velocity.y) < 0.001f)
        {
            hero_body.AddForce(new Vector2(0, jump_force), ForceMode2D.Impulse);
        }

        //Shooting

        if (Input.GetButtonDown("Fire1") && Time.time - time_shoot > vars.shooting_speed)
        {
            time_shoot = Time.time;
            time_stance = Time.time;
            animator.SetBool("is_shooting_stance", true);
            animator.SetTrigger("is_shooting");
        }
        
        if(Time.time - time_stance > stance_time)
        {
            animator.SetBool("is_shooting_stance", false);
        }

        //Croach

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            
            if(is_croach == false)
            {
                animator.SetBool("is_crouch", true);
                is_croach = true;
                //hero_body.constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            if (is_croach == true)
            {
                animator.SetBool("is_crouch", false);
                is_croach = false;
                //hero_body.constraints = RigidbodyConstraints2D.None;
            }
        }

        //Left/right profile
 
        if (Mathf.Abs(movement) > 0)
        {
            animator.SetBool("is_run", true);
            //Setting object facing
            if (movement < 0)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
            else transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            animator.SetBool("is_run", false);
        }


        if (Mathf.Abs(hero_body.velocity.y) < 0.5f)
        {
            animator.SetBool("is_jump", false);

        }
        else
        {
            animator.SetBool("is_jump", true);
        }

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("To jest to: " + col.gameObject.transform.name);
        if (col.gameObject.transform.name.Contains("Apteczka"))
        {
            current_health = 100;
            health_bar_instance.SetHealth(100);
        }
    }

}
