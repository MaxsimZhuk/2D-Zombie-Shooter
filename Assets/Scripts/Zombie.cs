using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public GameObject Head;
    public GameObject Body;
    public GameObject leg;
    public GameObject Myhealth;
    public GameObject MyAnimation;
    public GameObject AnimationDie;
    public GameObject AnimationHit;
    public GameObject AnimationRun;

    private float health;
    private bool alive;
    private int deadtimer;
   // Use this for initialization
   void Start()
    {
        alive = true;
        health = 12.0f;
        deadtimer = 50;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(alive)transform.position = transform.position - Vector3.right*(float) 0.1;
        else
        {
            deadtimer --;
            if (deadtimer < 0)
            {
                Destroy(gameObject);
            }
        }
        
    }

    void HealthDamage(int damage)
    {

        health = health - damage;
        Myhealth.transform.localScale = new Vector3(health/4, 0.2f, 0.2f);
        if (health < 1 && alive)
        {   
            alive = false;
            Destroy(MyAnimation);
            MyAnimation = Instantiate(AnimationDie, transform.position, transform.rotation) ;
            MyAnimation.transform.SetParent(transform);
            Destroy(Head);
            Destroy(Body);
            Destroy(leg);
        }
    }
}