using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeIDX : MonoBehaviour
{

    public int buildidx;
    void Awake(){
        GameObject Auth = GameObject.Find("AuthControl");
        Auth authScript = Auth.GetComponent<Auth>();
        authScript.GetIndex(SceneManager.GetActiveScene().buildIndex);
    }
}
