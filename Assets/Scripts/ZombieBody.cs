﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBody : MonoBehaviour {
    public GameObject MyZombie;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void HitForMe()
    {
        MyZombie.BroadcastMessage("HealthDamage", 3);

    }
    public void KickOffForMe()
    {
        MyZombie.BroadcastMessage("KickOff");

    }
}
