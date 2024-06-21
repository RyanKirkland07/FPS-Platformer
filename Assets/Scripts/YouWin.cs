using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class YouWin : MonoBehaviour
{
    public Auth authScript;
    public GameObject Winner;
    public static bool done;
    // Start is called before the first frame update
    void Awake()
    {
        done = false;
    }

    // Update is called once per frame
    void Start()
    {
        Debug.Log("hi");
    }
    public void OnTriggerStay(Collider Info)
    {
        if(Info.tag == "Win")
        {           
            Winner.SetActive(true);
            if(!done){
                done = true; 
                authScript.Transition();
            }
        }
    }
    public void LastScene(){
        Debug.LogError("Active? "+gameObject.activeInHierarchy);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("StartMenu");    
    }
}
