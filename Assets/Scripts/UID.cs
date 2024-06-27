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
        userID.text = "UID: " + FirebaseAuth.DefaultInstance.CurrentUser.UserId;
    }
}
