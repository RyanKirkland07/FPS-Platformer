
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;

    public Camera Cam;

    public AudioSource Gunshot;
    public AudioListener Pow;
    public AudioClip sound;

    public ParticleSystem Flash;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        void Shoot()
        {
            Flash.Play();
            Gunshot.Play();
            RaycastHit hit;
            if(Physics.Raycast(Cam.transform.position, Cam.transform.forward, out hit, range))
            {
                Debug.Log(hit.transform.name);

                Enemies target = hit.transform.GetComponent<Enemies>();
                if (target != null)
                {
                    target.TakeDamage(damage);
                }
            }

        }
    }
}
