using System;

public class Admin : User
{
    private string _role {  get; set; }

    public Admin(string name, string email, string password, string userName, bool isAdmin, string role) : base(name, email, password, userName, isAdmin)
    {
        _role = role;
    }

    public override string GetUserInformation()
    {
        throw new NotImplementedException();
    }

    public override bool IsUserLoggedIn()
    {
        throw new NotImplementedException();
    }

    public void AddSkill()
    {
        throw new NotImplementedException();
    }
    public void UpdateSkill(string name)
    {
        throw new NotImplementedException();
    }

    public void DeleteSkill(string name)
    {
        throw new NotImplementedException();
    }
    public void AddClient()
    {
        throw new NotImplementedException();
    }
    public void UpdateClient(string userName)
    {
        throw new NotImplementedException();
    }
    public void DeleteClient(string userName)
    {
        throw new NotImplementedException();
    }
    public void ViewClientProgress(string userName)
    {
        throw new NotImplementedException();
    }


}
