using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerup : MonoBehaviour
{
    public GameObject player;
    public PlayerMovement MoveScript;
    public Transform SpeedBoostLocationTransform;
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.Find("Capsule");
        PlayerMovement MoveScript = player.GetComponent<PlayerMovement>();
        GameObject SpeedBoostLocation = GameObject.Find("SpeedBoostLocation");
        Transform SpeedBoostLocationTransform = SpeedBoostLocation.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerStay(Collider Info)
    {
        if(Info.name == "Capsule")
        {
            Destroy(gameObject);
            MoveScript.Speedboost();
        }
    }
    public void RespawnBoost()
    {
        Instantiate(gameObject, SpeedBoostLocationTransform.position, Quaternion.identity);
    }
}
