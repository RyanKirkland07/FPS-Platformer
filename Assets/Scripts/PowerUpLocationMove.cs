using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpLocationMove : MonoBehaviour
{
    public Transform SpeedBoostTransform;
    // Start is called before the first frame update
    void Start()
    {
        GameObject SpeedBoost = GameObject.Find("Speed Boost");
        Transform SpeedBoostTransform = SpeedBoost.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = SpeedBoostTransform.position;
    }
}
