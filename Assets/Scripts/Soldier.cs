using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour {

    public GUISkin MoneySkin;
    public GameObject Target;        //Point of the player's touch
    public GameObject HeadSoldier;  
    public GameObject RedRay;
    public GameObject ZombieGameObject;

    public Texture2D RightBatton;
    public Texture2D LeftBatton;
    public Texture2D ReloadBatton;
    public Texture2D KickOffBatton;
    public Texture2D ChangeBatton;
    public Texture2D HealesTexture2D;
    public Texture2D AmmoTexture2D;

    public int fireDelays;
    public int healse;
    public int ammoMachineGun;

    private int myAmmo;
    private int myfireDelays;

    public List<GameObject> ZombiesArmy = new List<GameObject>();
    // Use this for initialization
    void Start () {
        myAmmo = ammoMachineGun;
        myfireDelays = fireDelays;
    }
	
	// Update is called once per frame
	void Update() {
        RaycastHit info;

        float lengthRedRay;

	    for (int i = 0; i < ZombiesArmy.Count; i++)
	    {
	        if (ZombiesArmy[i] != null)
	        {
	            if (ZombiesArmy[i].transform.position.x< transform.position.x +5)
	            {
	                ZombiesArmy[i].BroadcastMessage("AmHere", gameObject);
	            }
            }
        }
        
	    if (5 == Random.Range(1, 200))
	    {
	        GameObject NewSoldier;
            NewSoldier = Instantiate(ZombieGameObject, transform.position + Vector3.right * 30 - Vector3.forward * 2 - Vector3.up * (float)Random.Range( 1, 27)/9, transform.rotation);
            ZombiesArmy.Add(NewSoldier);
        }

	    if (Physics.Raycast(HeadSoldier.transform.position, HeadSoldier.transform.forward, out info, 30))
	    {
            lengthRedRay= Vector3.Distance(HeadSoldier.transform.position, info.collider.gameObject.transform.position);
         
        }
        else
        {
            lengthRedRay = 30;
        }
        RedRay.transform.localScale = new Vector3(lengthRedRay, 0.1f, 0.1f);
        RedRay.transform.position = HeadSoldier.transform.position+ HeadSoldier.transform.forward * (float)lengthRedRay/2 + HeadSoldier.transform.right * (float)0.5;

        if(myfireDelays > fireDelays) {} else myfireDelays++;

        for (int i = 0; i < Input.touchCount; i++) 
	    {
	        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);

            

            if (pos.x > transform.position.x + 3 && myfireDelays > fireDelays)
	        {
                myfireDelays=0;
                   myAmmo--;

                if (Physics.Raycast(HeadSoldier.transform.position, HeadSoldier.transform.forward, out info, 30))
	            {
                    info.collider.gameObject.BroadcastMessage("HitForMe");

                }
	            Target.transform.position = pos;
	            HeadSoldier.transform.LookAt(Target.transform);

	        }
            //button to move right
            if (   Input.GetTouch(i).position.x > Screen.height / 5 && Input.GetTouch(i).position.x < (Screen.height / 5) *2
                && Input.GetTouch(i).position.y > 0 && Input.GetTouch(i).position.y < Screen.height / 5)
	        {
	            transform.position = transform.position + Vector3.right*(float) 0.1;
	        }
            //button to move left
            if (Input.GetTouch(i).position.x > 0 && Input.GetTouch(i).position.x < Screen.height / 5
             && Input.GetTouch(i).position.y > 0 && Input.GetTouch(i).position.y < Screen.height / 5)
            {
                transform.position = transform.position  - Vector3.right * (float)0.1;
            }

        }

	}

    void HealthDamage()
    {
        healse --;
    }
    void OnGUI()
    {
        if (GUI.Button(new Rect(0,0, Screen.height / 5, Screen.height / 5), "exit"))
        {
            Application.Quit();
        }

        GUI.DrawTexture(new Rect(Screen.height / 5, Screen.height - Screen.height / 5, Screen.height / 5, Screen.height / 5), RightBatton);
        

        GUI.DrawTexture(new Rect(0, Screen.height - Screen.height / 5, Screen.height / 5, Screen.height / 5), LeftBatton);
        GUI.DrawTexture(new Rect(0, Screen.height - Screen.height / 5*2, Screen.height / 5, Screen.height / 5), ChangeBatton);
        GUI.DrawTexture(new Rect(0, Screen.height - Screen.height / 5 * 3, Screen.height / 5, Screen.height / 5), KickOffBatton);
        GUI.DrawTexture(new Rect(0, Screen.height - Screen.height / 5 * 4, Screen.height / 5, Screen.height / 5), ReloadBatton);
        for (int i = 0; i < healse; i++)
        {
            GUI.DrawTexture(new Rect(Screen.height / 20*i, 0, Screen.height / 20, Screen.height / 20), HealesTexture2D);
        }
        GUI.skin = MoneySkin;
        GUI.Label(new Rect(0, Screen.height / 20, 700, 70), "AMMO= " + myAmmo);
        // if (GUI.Button(new Rect(Screen.height / 5, Screen.height - Screen.height / 5, Screen.height / 5, Screen.height / 5), RightBatton)
        // if (GUI.Button(new Rect(0, Screen.height - Screen.height / 5, Screen.height / 5, Screen.height / 5), LeftBatton)
    }
}

