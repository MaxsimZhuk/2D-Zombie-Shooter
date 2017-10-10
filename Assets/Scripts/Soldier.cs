using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour {
    public GameObject Target;        //Point of the player's touch
    public GameObject HeadSoldier;  
    public GameObject RedRay;
    public GameObject ZombieGameObject;

    public Texture2D RightBatton;
    public Texture2D LeftBatton;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate() {
        RaycastHit info;
        float lengthRedRay;

	    if (5 == Random.Range(1, 200))
	    {
	        GameObject NewSoldier;
            
            NewSoldier = Instantiate(ZombieGameObject, transform.position + Vector3.right * 30 - Vector3.forward * 2 - Vector3.up * (float)Random.Range( 1, 27)/9, transform.rotation);
	    }
	    if (Physics.Raycast(HeadSoldier.transform.position, HeadSoldier.transform.forward, out info, 30))
	    {

            lengthRedRay= Vector3.Distance(HeadSoldier.transform.position, info.collider.gameObject.transform.position);
           // info.collider.gameObject.BroadcastMessage("HitForMe");
        }
        else
        {
            lengthRedRay = 30;
        }
        RedRay.transform.localScale = new Vector3(lengthRedRay, 0.1f, 0.1f);
        RedRay.transform.position = HeadSoldier.transform.position+ HeadSoldier.transform.forward * (float)lengthRedRay/2 + HeadSoldier.transform.right * (float)0.5; 
        

        for (int i = 0; i < Input.touchCount; i++) 
	    {
	        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);

	        if (pos.x > transform.position.x + 3)  
	        {
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
	

    void OnGUI()
    {
        if (GUI.Button(new Rect(0,0, Screen.height / 5, Screen.height / 5), "exit"))
        {
            Application.Quit();
        }

        GUI.DrawTexture(new Rect(Screen.height / 5, Screen.height- Screen.height /5 , Screen.height / 5, Screen.height / 5), RightBatton);
        GUI.DrawTexture(new Rect(0, Screen.height - Screen.height / 5, Screen.height / 5, Screen.height / 5), LeftBatton);
       // if (GUI.Button(new Rect(Screen.height / 5, Screen.height - Screen.height / 5, Screen.height / 5, Screen.height / 5), RightBatton)
       // if (GUI.Button(new Rect(0, Screen.height - Screen.height / 5, Screen.height / 5, Screen.height / 5), LeftBatton)
    }
}

