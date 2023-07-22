using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunChange : MonoBehaviour
{
    public bool ChangeReady;
    public float ChangeReadyTime;

    public bool GlockEquipped;
    public bool AREquipped;
    public bool ShotgunEquipped;

    public Gun GunScript;
    public Transform GunBarrel;
    // Start is called before the first frame update
    void Start()
    {
        GameObject Barrel = GameObject.Find("GunBarrel");
        GunBarrel = Barrel.GetComponent<Transform>();

        GameObject Player = GameObject.Find("Capsule");
        GunScript = Player.GetComponent<Gun>();
        Debug.Log(gameObject);
        ChangeReady = true;
        ChangeReadyTime = 1.5f;
        GlockEquipped = true;
    }

    // Update is called once per frame

    private void OnTriggerStay(Collider GunInfo)
    {
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
        yield return new WaitForSeconds(ChangeReadyTime);
        ChangeReady = true;
    }

    void Update()
    {
        if(Input.GetKeyDown("q"))
        {
            AR();
        }
        if(Input.GetKeyDown("e"))
        {
            Glock();
        }
        if(Input.GetKeyDown("l"))
        {
            Shotgun();
        }
    }
    public void AR()
    {
        Debug.Log("Method works");
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
