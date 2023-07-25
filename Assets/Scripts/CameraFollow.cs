using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //Player transform
    public Transform Capsule;
    // Start is called before the first frame update
    void Start()
    {
        //Finds player and player transform
        GameObject player = GameObject.Find("Capsule");
        Capsule = player.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //Positions camera on player object
        transform.position = Capsule.position;
    }
}
