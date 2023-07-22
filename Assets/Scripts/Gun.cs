using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
    public float gundamage;
    public float range;
    public float FireRate;
    public float ReloadSpeed;
    public int Ammo;
    public int LoadedAmmo;
    public bool Empty;
    public float NextTimeToFire;

    public Camera Cam;

    public AudioSource Gunshot;
    public AudioListener Pow;
    public AudioClip GlockShot;
    public AudioClip ARShot;
    public AudioClip ShotgunShot;

    public ParticleSystem Flash;

    public LayerMask EnemyLayerMask;
    // Start is called before the first frame update
    void Start()
    {
        gundamage = 10f;
        range = 15f;
        FireRate = 1f;
        Ammo = 16;
        LoadedAmmo = 16;
        NextTimeToFire = 0f;
        ReloadSpeed = 2.5f;
        Gunshot.clip = GlockShot;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Fire1") && Time.time >= NextTimeToFire && !Empty)
        {
            Flash.Play();
            Gunshot.Play();
            NextTimeToFire = Time.time + 1f/FireRate;
            LoadedAmmo = LoadedAmmo - 1;
            Shoot();
            if(LoadedAmmo == 0)
            {
                Empty = true;
            }
        }
        if(Input.GetButtonDown("Fire1") && Empty)
        {
            StartCoroutine(Reload());
        }

        void Shoot()
        {
            RaycastHit hit;
            if(Physics.Raycast(Cam.transform.position, Cam.transform.forward, out hit, range, EnemyLayerMask))
            {
                Debug.Log(hit.transform.name);

                Enemies target = hit.transform.GetComponent<Enemies>();
                if (target != null)
                {
                    target.TakeDamage(gundamage);
                }
            }

        }
        IEnumerator Reload()
        {
            yield return new WaitForSeconds(ReloadSpeed);
            LoadedAmmo = Ammo;
            Empty = false;
        }

    }
    public void ARchange()
    {
        gundamage = 5f;
        range = 15f;
        FireRate = 2f;
        Ammo = 31;
        LoadedAmmo = 31;
        NextTimeToFire = 0f;
        Empty = false;
        ReloadSpeed = 4f;
        Gunshot.clip = ARShot;
    }
    public void Glockchange()
    {
        gundamage = 10f;
        range = 15f;
        FireRate = 1f;
        Ammo = 16;
        LoadedAmmo = 16;
        NextTimeToFire = 0f;
        Empty = false;
        ReloadSpeed = 2.5f;
        Gunshot.clip = GlockShot;
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
    }
}
