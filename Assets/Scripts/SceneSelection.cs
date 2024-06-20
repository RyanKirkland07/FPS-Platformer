using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneSelection : MonoBehaviour
{
    public Auth authscript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SelectScene(){
        switch(this.gameObject.name){
            case "toLevel0":
                SceneManager.LoadScene("level 0");
                break;
            case "toLevel1":
                SceneManager.LoadScene("level 1");
                break;
            case "toLevel2":
                SceneManager.LoadScene("level 2");
                break;
            case "toLevel3":
                SceneManager.LoadScene("level 3");
                break;
            case "toLevel4":
                SceneManager.LoadScene("level 4");
                break;
        }

    }
}
