using UnityEngine;
using System.Collections;
using TMPro;

public class Gun : MonoBehaviour
{
    //Gun Variables
    public float gundamage;
    public float range;
    public float FireRate;
    public float ReloadSpeed;
    public int Ammo;
    public int LoadedAmmo;
    public bool Empty;
    public float NextTimeToFire;

    //Camera
    public Camera Cam;

    //Extra stuff to improve the script
    public AudioSource Gunshot;
    public AudioListener Pow;
    public AudioClip GlockShot;
    public AudioClip ARShot;
    public AudioClip ShotgunShot;
    public ParticleSystem Flash;

    //Enemy Mask that is targeted by raycast
    public LayerMask EnemyLayerMask;
    // Start is called before the first frame update

    public TextMeshProUGUI AmmoText;
    void Start()
    {
        //Sets all variables to what they should be at the start of the script
        gundamage = 10f;
        range = 15f;
        FireRate = 1f;
        Ammo = 16;
        LoadedAmmo = 16;
        NextTimeToFire = 0f;
        ReloadSpeed = 2.5f;
        Gunshot.clip = GlockShot;

        AmmoText.text = LoadedAmmo + "/" + Ammo;
    }

    // Update is called once per frame
    void Update()
    {
        //Checks if gun is ready to fire and then fires
        if(Input.GetButton("Fire1") && Time.time >= NextTimeToFire && !Empty)
        {
            //Creates the next time to fire and subtracts from loadedammo
            NextTimeToFire = Time.time + 1f/FireRate;
            LoadedAmmo = LoadedAmmo - 1;
            AmmoUI();
            //Starts shoot function
            Shoot();
            //Detects if ammo is 0
            if(LoadedAmmo == 0)
            {
                Empty = true;
            }
        }
        //If gun is Empty starts reload
        if(Input.GetButtonDown("Fire1") && Empty)
        {
            StartCoroutine(Reload());
        }

        }
        //Reload timer and function
        IEnumerator Reload()
        {
            yield return new WaitForSeconds(ReloadSpeed);
            LoadedAmmo = Ammo;
            Empty = false;
            AmmoUI();
        }

    //Shoot function
    public void Shoot()
    {
        //Extra stuff
        Flash.Play();
        Gunshot.Play();
        //Raycast
        RaycastHit hit;
        if(Physics.Raycast(Cam.transform.position, Cam.transform.forward, out hit, range, EnemyLayerMask))
        {
            //Detects if hit enemy
            Enemies target = hit.transform.GetComponent<Enemies>();
            if (target != null)
            {
                //Calls enemy script to apply damage
                target.TakeDamage(gundamage);
            }
        }
    }
    
    //These functions change the variables when switching guns
    public void ARchange()
    {
        gundamage = 7.5f;
        range = 15f;
        FireRate = 2f;
        Ammo = 31;
        LoadedAmmo = 31;
        NextTimeToFire = 0f;
        Empty = false;
        ReloadSpeed = 4f;
        Gunshot.clip = ARShot;
        AmmoUI();
    }
    public void Glockchange()
    {
        gundamage = 15f;
        range = 15f;
        FireRate = 1f;
        Ammo = 16;
        LoadedAmmo = 16;
        NextTimeToFire = 0f;
        Empty = false;
        ReloadSpeed = 2.5f;
        Gunshot.clip = GlockShot;
        AmmoUI();
    }
    public void Shotgunchange()
    {
        gundamage = 100f;
        range = 5f;
        FireRate = 0.333f;
        Ammo = 2;
        LoadedAmmo = 2;
        NextTimeToFire = 0f;
        Empty = false;
        ReloadSpeed = 6f;
        Gunshot.clip = ShotgunShot;
        AmmoUI();
    }
    public void AmmoUI(){
        AmmoText.text = LoadedAmmo + "/" + Ammo;
    }
}

