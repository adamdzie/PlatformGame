using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_penguin : MonoBehaviour
{
    public float movement_speed = 15.0f;
    public int health = 75;
    public float attackRange = 0.5f;
    public int attackDamage = 20;
    public float attackRate = 2f;
    public float nextAttackTime = 0f;

    public Transform attackPoint;
    private Animator animator;

    public Transform CenterOfMap;
    public Transform hero;
    public bool isMoving;
    public Text PenguinText;

    // Start is called before the first frame update
    void Start()
    {
        CenterOfMap = GameObject.FindGameObjectWithTag("MapCenter").transform.GetComponent<Transform>();
        hero = GameObject.Find("Hero").transform.GetComponent<Transform>();

        animator = GetComponent<Animator>();
        if (gameObject.transform.position.x < CenterOfMap.position.x)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else transform.localRotation = Quaternion.Euler(0, 180, 0);

        isMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (isMoving)
        {
            move();
        }*/
        isPlayerNearby();
    }



    public void Attack()
    {
        animator.SetTrigger("Attack");
        hero.GetComponent<Player_controller>().ReceiveDamage(attackDamage);
    }

    private void isPlayerNearby()
    {
        if (hero.transform.position.x + 15f <= gameObject.transform.position.x  || hero.transform.position.x - 15f >= gameObject.transform.position.x)
        {
            
            move();
        }
        else
        {
            isMoving = false;
            if (Time.time >= nextAttackTime)
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
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
    }

    public void receiveDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
            int T = int.Parse(PenguinText.text);
            T = T + 1;
            string T1 = T.ToString();
            PenguinText.text = T1;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}