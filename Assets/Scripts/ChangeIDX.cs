using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeIDX : MonoBehaviour
{

    public int buildidx;
    void Awake(){
        //Find Auth script on AuthControl object and calls GetIndex function when new scene is loaded
        GameObject Auth = GameObject.Find("AuthControl");
        Auth authScript = Auth.GetComponent<Auth>();
        authScript.GetIndex(SceneManager.GetActiveScene().buildIndex);
    }
}
