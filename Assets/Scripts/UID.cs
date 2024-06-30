using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Firebase.Auth;
public class UID : MonoBehaviour
{
    public TextMeshProUGUI userID;
    // Start is called before the first frame update
    void Awake()
    {
        //Gets and displays current User's UserId in the start menu scene
        userID.text = "UID: " + FirebaseAuth.DefaultInstance.CurrentUser.UserId;
    }
}
