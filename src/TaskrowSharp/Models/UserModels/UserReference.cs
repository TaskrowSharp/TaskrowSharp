﻿namespace TaskrowSharp.Models.UserModels;

public class UserReference
{
    public int UserID { get; private set; }
    public string UserLogin { get; private set; }
    public string UserHashCode { get; private set; }

    public UserReference()
    {

    }

    public UserReference(int userID, string userLogin, string userHashCode = null)
    {
        UserID = userID;
        UserLogin = userLogin;
        UserHashCode = userHashCode;
    }
}
