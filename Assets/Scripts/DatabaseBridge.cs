using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using Firebase;
using TMPro;

public class DatabaseBridge : MonoBehaviour
{
    public TextMeshProUGUI emailInput;
    public TextMeshProUGUI passwordInput;

    public DatabaseReference dbReference;

    private string userID;
    // Start is called before the first frame update
    void Start()
    {
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
        userID = SystemInfo.deviceUniqueIdentifier;
    }

    public void CreateUser(){
        User user = new User(emailInput.text, passwordInput.text);
        string json = JsonUtility.ToJson(user);
        dbReference.Child("User").Child(userID + Random.Range(1,1000000)).SetRawJsonValueAsync(json);

    }
}
