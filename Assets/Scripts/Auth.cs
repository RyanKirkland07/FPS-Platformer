using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using TMPro;
using Firebase.Database;


public class Auth : MonoBehaviour
{
    public TextMeshProUGUI emailInput;
    public TextMeshProUGUI passwordInput;
    public TextMeshProUGUI usernameInput;
    public DatabaseReference dbReference;

    void Start(){
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void RegisterUser(){
        FirebaseAuth.DefaultInstance.CreateUserWithEmailAndPasswordAsync(emailInput.text, passwordInput.text).ContinueWith(task => {
            if(task.IsCanceled){
                Debug.LogError("Registering User Was Canceled");
                return;
            }
            if(task.IsFaulted) {
                Debug.LogError("Registering User Encountered An Error" + task.Exception);
                return;
            }
            if(task.IsCompleted){
                Firebase.Auth.AuthResult result = task.Result;
                string ID = result.User.UserId.ToString();
                Debug.Log("Firebase User Registered Successfully "+ ID);
                User user = new User(usernameInput.text, passwordInput.text, emailInput.text, ID);
                string json = JsonUtility.ToJson(user);
                dbReference.Child("User").Child(usernameInput.text).SetRawJsonValueAsync(json);
            }
            

        });
    }
    public void LogInUser(){
        FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(emailInput.text, passwordInput.text).ContinueWith( task => {
            if(task.IsCanceled) {
                Debug.LogError("Sign In Was Canceled");
                return;
            }
            if(task.IsFaulted){
                Debug.LogError("Sign In Encountered An Error");
                return;
            }

            Firebase.Auth.AuthResult result = task.Result;
            Debug.LogFormat("User Signed In Successfully", result.User.DisplayName, result.User.UserId);
        });
    }
    public void LogOutUser(){
        FirebaseAuth.DefaultInstance.SignOut();
    }

    public void CreateUser(){

        dbReference.Child("User").Child(usernameInput.text).GetValueAsync().ContinueWith(task =>
        {
            if(task.IsFaulted){
                Debug.LogError("Failed to read username:" + task.Exception);
                return;
            }

            if (task.IsCompleted){
                DataSnapshot snap = task.Result;
                if(snap.Exists)
                {
                    Debug.LogError("Yes");
                    return;
                }
                else
                {
                    RegisterUser();
                }
            }
        });
    }
}
