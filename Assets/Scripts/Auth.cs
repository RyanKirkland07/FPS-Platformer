using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using TMPro;
using Firebase.Database;
using UnityEngine.SceneManagement;
using static Firebase.Extensions.TaskExtension;


public class Auth : MonoBehaviour
{
    public TextMeshProUGUI emailInput;
    public TextMeshProUGUI passwordInput;
    public TextMeshProUGUI usernameInput;
    public DatabaseReference dbReference;

    public string STime0, STime1, STime2, STime3, STime4;
    public float UTime0, UTime1, UTime2, UTime3, UTime4;
    public string UID;

    public int bidx;

    private Firebase.Auth.FirebaseAuth auth;
    private Firebase.Auth.FirebaseUser player;
    private Firebase.Auth.AuthResult result;

    private bool LogInFailed;
    //private DataSnapshot snapshot;

    private string DATA_URL = "https://fpsplatformer-default-rtdb.asia-southeast1.firebasedatabase.app/";

    void Start(){
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
        DontDestroyOnLoad(gameObject);
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
                var player = FirebaseAuth.DefaultInstance.CurrentUser;
                Firebase.Auth.AuthResult result = task.Result;
                Debug.Log("Firebase User Registered Successfully "+ result.User.UserId.ToString());
                User user = new User(0f, 0f, 0f, 0f, 0f);
                string json = JsonUtility.ToJson(user);
                dbReference.Child("User").Child(player.UserId).SetRawJsonValueAsync(json);
            }
            

        });
    }
    public async void LogInUser(){
        await FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(emailInput.text, passwordInput.text).ContinueWith( task => {
            if(task.IsCanceled) {
                Debug.LogError("Sign In Was Canceled");
                LogInFailed = true;
                return;
            }
            if(task.IsFaulted){
                Debug.LogError("Sign In Encountered An Error");
                LogInFailed = true;
                return;
            }
            if (task.IsCompleted){
                Firebase.Auth.AuthResult result = task.Result;
                Debug.LogFormat("User Signed In Successfully", result.User.DisplayName, result.User.UserId);
                LogInFailed = false;
                var player = FirebaseAuth.DefaultInstance.CurrentUser;
                UID = player.UserId;
            }
                });
            if(!LogInFailed){
                GetTimeValues();
            }
        
    }
    public void LogOutUser(){
        FirebaseAuth.DefaultInstance.SignOut();
    }
    private async void GetTimeValues(){
        var level0T = await dbReference.Child("User").Child(UID).Child("level0Time").GetValueAsync();
        var level1T = await dbReference.Child("User").Child(UID).Child("level1Time").GetValueAsync();
        var level2T = await dbReference.Child("User").Child(UID).Child("level2Time").GetValueAsync();
        var level3T = await dbReference.Child("User").Child(UID).Child("level3Time").GetValueAsync();
        var level4T = await dbReference.Child("User").Child(UID).Child("level4Time").GetValueAsync();
        bool worked = true;
        if(worked){
            UTime0 = float.Parse(level0T.Value.ToString());
            UTime1 = float.Parse(level1T.Value.ToString());
            UTime2 = float.Parse(level2T.Value.ToString());
            UTime3 = float.Parse(level3T.Value.ToString());
            UTime4 = float.Parse(level4T.Value.ToString());
        }
        SceneManager.LoadScene("StartMenu");
    }

    public void GetIndex(int idx){
        bidx = idx;
    }
}
