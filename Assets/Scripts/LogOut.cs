using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase.Auth;

public class LogOut : MonoBehaviour
{
    //Log out signed in User
    public void LogOutUser(){
        FirebaseAuth.DefaultInstance.SignOut();
        SceneManager.LoadScene("User");
    }
}
