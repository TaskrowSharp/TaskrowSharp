namespace TaskrowSharp.Models.UserModels;

public class UserReference
{
    public int UserID { get; private set; }
    public string UserLogin { get; private set; }
    public string FullName { get; set; }
    public string UserHashCode { get; private set; }
    
    public UserReference()
    {

    }

    public UserReference(int userID, string userLogin, string fullName = null, string userHashCode = null)
    {
        UserID = userID;
        UserLogin = userLogin;
        FullName = fullName;
        UserHashCode = userHashCode;
    }
}
