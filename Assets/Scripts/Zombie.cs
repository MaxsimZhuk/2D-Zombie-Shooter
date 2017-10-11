using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    private GameObject Player;

    enum Direction
    {
        Run,
        Beat

    }
 private Direction _myDirection;
    public GameObject Head;
    public GameObject Body;
    public GameObject leg;
    public GameObject Myhealth;
    public GameObject MyAnimation;
    public GameObject AnimationDie;
    public GameObject AnimationHit;
    public GameObject AnimationRun;

    public int fireDelays;
    private float health;
    private bool alive;
    private int deadtimer;
    private int myfireDelays;

    // Use this for initialization
    void Start()
    {
        alive = true;
        health = 12.0f;
        deadtimer = 45;
       _myDirection = Direction.Run;
        myfireDelays = fireDelays;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (alive)
        {

            if (Player != null)
            {
                if (transform.position.x > Player.transform.position.x + 3 && _myDirection == Direction.Run)
                {
                    transform.position = transform.position - Vector3.right*(float) 0.1;
                }

                if (transform.position.x <= Player.transform.position.x + 3 && _myDirection == Direction.Run)
                {
                    _myDirection = Direction.Beat;
                    Destroy(MyAnimation);
                    MyAnimation = Instantiate(AnimationHit, transform.position, transform.rotation);
                    MyAnimation.transform.SetParent(transform);

                }
                if ( _myDirection == Direction.Beat)
                {
                    myfireDelays--;
                    if (myfireDelays < 0)
                    {
                        if (transform.position.x > Player.transform.position.x + 4 && _myDirection == Direction.Beat)
                        {
                            _myDirection = Direction.Run;
                            Destroy(MyAnimation);
                            MyAnimation = Instantiate(AnimationRun, transform.position, transform.rotation);
                            MyAnimation.transform.SetParent(transform);
                        }
                        else
                        {
                            Player.BroadcastMessage("HealthDamage");
                        }
                        myfireDelays = fireDelays;
                    }
                    

                }

            }
            else
            {
                transform.position = transform.position - Vector3.right * (float)0.1;
            }
        }
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
    public void KickOff()
    {
        transform.position = transform.position + Vector3.right*3;

    }

    void AmHere(GameObject P)
    {
        Player = P;
    }
}