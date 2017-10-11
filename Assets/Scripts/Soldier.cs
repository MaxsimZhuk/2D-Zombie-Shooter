using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour {



    enum Weapons
    {
        MachineGun,
        MachineGunShoot,
        MachineGunReload,
        Pistol

    }
    private  Weapons _myWeapons;
    enum Direction
    {
        Right,
        Left,
        Stop,
        KickOff

    }
    private Direction _myDirection;

    public GUISkin MoneySkin;
    public GameObject Target;        //Point of the player's touch
    public GameObject HeadSoldier;
    public GameObject BodySoldier;
    public GameObject HeadAnimation;
    public GameObject AnimationGoLeft;
    public GameObject AnimationGoRight;
    public GameObject AnimationStop;
    public GameObject AnimationKickOff;
    public GameObject AnimationGun;
    public GameObject AnimationGunShoot;
    public GameObject AnimationGunRelod;
    public GameObject RedRay;
    public GameObject ZombieGameObject;

    public Texture2D RightBatton;
    public Texture2D LeftBatton;
    public Texture2D ReloadBatton;
    public Texture2D KickOffBatton;
    public Texture2D ChangeBatton;
    public Texture2D HealesTexture2D;
    public Texture2D AmmoTexture2D;
    public Texture2D youareDed;

    public int fireDelays;
    public int healse;
    public int ammoMachineGun;

    private int myAmmo;
    private int myfireDelays;
    public int DelayReload;
    private int timerReload;
    public List<GameObject> ZombiesArmy = new List<GameObject>();
    // Use this for initialization
    void Start () {
        myAmmo = ammoMachineGun;
        myfireDelays = fireDelays;
        timerReload = 0;
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
        
	    if (5 == Random.Range(1, 250))
	    {
	        GameObject NewSoldier;
            NewSoldier = Instantiate(ZombieGameObject,
                transform.position + Vector3.right * 30 - Vector3.forward * 2 - Vector3.up * (float)Random.Range( 1, 27)/9, transform.rotation);
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

	    if (Input.touchCount == 0)
	    {
            if (_myDirection != Direction.Stop)
            {
                Destroy(BodySoldier);
                BodySoldier = Instantiate(AnimationStop, transform.position - Vector3.up * 1 + Vector3.right * (float)0.5, transform.rotation);
                BodySoldier.transform.SetParent(transform);
                _myDirection = Direction.Stop;
            }
            if (_myWeapons == Weapons.MachineGunShoot)
            {

                Destroy(HeadAnimation);
                HeadAnimation = Instantiate(AnimationGun,
                HeadAnimation.transform.position, HeadAnimation.transform.rotation);
                HeadAnimation.transform.SetParent(HeadSoldier.transform);
                _myWeapons = Weapons.MachineGun;

            }
        }
	    if (_myWeapons == Weapons.MachineGunReload)
	    {
	        timerReload++;
            
            if (timerReload > DelayReload)
	        {
	            myAmmo = ammoMachineGun;
	            
	            timerReload = 0;
                Destroy(HeadAnimation);
                HeadAnimation = Instantiate(AnimationGun,
                HeadAnimation.transform.position, HeadAnimation.transform.rotation);
                HeadAnimation.transform.SetParent(HeadSoldier.transform);
                _myWeapons = Weapons.MachineGun;
            }
        }
	    for (int i = 0; i < Input.touchCount; i++) 
	    {
	        if (Input.GetTouch(i).position.x > 0 && Input.GetTouch(i).position.x < Screen.height/5
	            && Input.GetTouch(i).position.y > (Screen.height/5)*3 && Input.GetTouch(i).position.y < (Screen.height/5)*4)
	        {
                Destroy(HeadAnimation);
                HeadAnimation = Instantiate(AnimationGunRelod,
                HeadAnimation.transform.position, HeadAnimation.transform.rotation);
                HeadAnimation.transform.SetParent(HeadSoldier.transform);
                _myWeapons = Weapons.MachineGunReload;
            }

	        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);






            if (Input.GetTouch(i).position.x > 0 && Input.GetTouch(i).position.x < Screen.height / 5
                && Input.GetTouch(i).position.y > (Screen.height / 5) * 2 && Input.GetTouch(i).position.y < (Screen.height / 5) * 3)
            {
                if (_myDirection != Direction.KickOff)
                {
                    if (Physics.Raycast(HeadSoldier.transform.position, HeadSoldier.transform.forward, out info, 3))
                    {
                        info.collider.gameObject.BroadcastMessage("KickOffForMe");

                    }
                    Destroy(BodySoldier);
                    BodySoldier = Instantiate(AnimationKickOff,
                        transform.position - Vector3.up * 1 + Vector3.right * (float)0.5, transform.rotation);
                    BodySoldier.transform.SetParent(transform);
                    _myDirection = Direction.KickOff;

                }
            }
            else
            {


                //button to move right
                if (Input.GetTouch(i).position.x > Screen.height / 5 && Input.GetTouch(i).position.x < (Screen.height / 5) * 2
                    && Input.GetTouch(i).position.y > 0 && Input.GetTouch(i).position.y < Screen.height / 5)
                {
                    transform.position = transform.position + Vector3.right * (float)0.1;
                    if (_myDirection != Direction.Right)
                    {
                        Destroy(BodySoldier);
                        BodySoldier = Instantiate(AnimationGoRight,
                            transform.position - Vector3.up * 1 + Vector3.right * (float)0.5, transform.rotation);
                        BodySoldier.transform.SetParent(transform);
                        _myDirection = Direction.Right;
                    }
                }
                else
                {
                    //button to move left
                    if (Input.GetTouch(i).position.x > 0 && Input.GetTouch(i).position.x < Screen.height / 5
                        && Input.GetTouch(i).position.y > 0 && Input.GetTouch(i).position.y < Screen.height / 5)
                    {
                        transform.position = transform.position - Vector3.right * (float)0.1;
                        if (_myDirection != Direction.Left)
                        {

                            Destroy(BodySoldier);
                            BodySoldier = Instantiate(AnimationGoLeft,
                                transform.position - Vector3.up * 1 + Vector3.right * (float)0.5, transform.rotation);
                            BodySoldier.transform.SetParent(transform);
                            _myDirection = Direction.Left;

                        }
                    }
                    else
                    {


                        if (_myDirection != Direction.Stop)
                        {
                            Destroy(BodySoldier);
                            BodySoldier = Instantiate(AnimationStop,
                                transform.position - Vector3.up * 1 + Vector3.right * (float)0.5, transform.rotation);
                            BodySoldier.transform.SetParent(transform);
                            _myDirection = Direction.Stop;
                        }
                    }
                }
            }
            if (_myWeapons != Weapons.MachineGunReload)
	        {
	            if (pos.x > transform.position.x + 3 && myfireDelays > fireDelays)
	            {
	                myfireDelays = 0;
	                myAmmo--;

	                if (Physics.Raycast(HeadSoldier.transform.position, HeadSoldier.transform.forward, out info, 30))
	                {
	                    info.collider.gameObject.BroadcastMessage("HitForMe");

	                }
	                Target.transform.position = pos;
	                HeadSoldier.transform.LookAt(Target.transform);
	                if (_myWeapons != Weapons.MachineGunShoot)
	                {

	                    Destroy(HeadAnimation);
	                    HeadAnimation = Instantiate(AnimationGunShoot,
	                    HeadAnimation.transform.position, HeadAnimation.transform.rotation);
	                    HeadAnimation.transform.SetParent(HeadSoldier.transform);
	                    _myWeapons = Weapons.MachineGunShoot;

	                }
                    if (myAmmo < 1)
                    {
                        Destroy(HeadAnimation);
                        HeadAnimation = Instantiate(AnimationGunRelod,
                        HeadAnimation.transform.position, HeadAnimation.transform.rotation);
                        HeadAnimation.transform.SetParent(HeadSoldier.transform);
                        _myWeapons = Weapons.MachineGunReload;

                    }

                }
	        }

	        
	    }

	}

    void HealthDamage()
    {
        healse --;
    }
    void OnGUI()
    {
      

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
        if (healse < 1)
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), youareDed);
        }
       
    }
}

