using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunChange : MonoBehaviour
{
    //Variables to see if changing guns is ready
    public bool ChangeReady;
    public float ChangeReadyTime;

    //Bool to check which gun is equipped
    public bool GlockEquipped;
    public bool AREquipped;
    public bool ShotgunEquipped;

    //The Gun script and the GunBarrel Transform
    public Gun GunScript;
    public Transform GunBarrel;
    // Start is called before the first frame update
    void Start()
    {
        //Finds the GunBarrel Transform
        GameObject Barrel = GameObject.Find("GunBarrel");
        GunBarrel = Barrel.GetComponent<Transform>();

        //Finds the gun script
        GameObject Player = GameObject.Find("Capsule");
        GunScript = Player.GetComponent<Gun>();

        //Sets variables
        ChangeReady = true;
        ChangeReadyTime = 1.5f;
        GlockEquipped = true;
    }

    // Update is called once per frame

    private void OnTriggerStay(Collider GunInfo)
    {
        //When collision with guns triggers the function detects which gun it is and if you are able to switch
        Debug.Log(GunInfo.transform.position);
        if(GunInfo.tag == "Glock" && ChangeReady && !GlockEquipped)
        {
            Glock();
            ChangeReady = false;
            StartCoroutine(ChangeCooldown());
            GlockEquipped = true;
            ShotgunEquipped = false;
            AREquipped = false;
        }
        else if(GunInfo.tag == "AR" && ChangeReady && !AREquipped)
        {
            AR();
            ChangeReady = false;
            StartCoroutine(ChangeCooldown());
            AREquipped = true;
            ShotgunEquipped = false;
            GlockEquipped = false;
        }
        else if(GunInfo.tag == "Shotgun" && ChangeReady && !ShotgunEquipped)
        {
            Shotgun();
            ChangeReady = false;
            StartCoroutine(ChangeCooldown());
            ShotgunEquipped = true;
            GlockEquipped = false;
            AREquipped = false;
        }
    }

    IEnumerator ChangeCooldown()
    {
        //Countdown the timer for being able to switch guns again
        yield return new WaitForSeconds(ChangeReadyTime);
        ChangeReady = true;
    }

    //Calls upon the Gun script to change the variables for the gun and also scales the gun to change model
    public void AR()
    {
        GunBarrel.localScale = new Vector3(0.6f, 6f, 1f);
        GunScript.ARchange();
    }
    public void Glock()
    {
        GunBarrel.localScale = new Vector3(0.6f, 3f, 1);
        GunScript.Glockchange();
    }
    public void Shotgun()
    {
        GunBarrel.localScale = new Vector3(0.8f, 4f, 2.5f);
        GunScript.Shotgunchange();
    }
}
