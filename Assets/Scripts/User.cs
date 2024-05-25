using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{
    public string Username;
    public string Password;
    public string userID;
    public string Email;

    public User(string Username, string Password, string Email, string userID){
        this.Username = Username;
        this.Password = Password;
        this.userID = userID;
        this.Email = Email;
    }
}
