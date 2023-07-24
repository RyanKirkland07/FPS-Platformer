using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPowerup : MonoBehaviour
{
    public GameObject player;
    public PlayerMovement MoveScript;
    public Transform JumpBoostLocationTransform;
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.Find("Capsule");
        PlayerMovement MoveScript = player.GetComponent<PlayerMovement>();
        GameObject JumpBoostLocation = GameObject.Find("JumpBoostLocation");
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
            MoveScript.Jumpboost();
        }
    }
}
