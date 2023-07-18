using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Capsule;
    public Transform CameraPos;
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.Find("Capsule");
        Capsule = player.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Capsule.position;
    }
}
