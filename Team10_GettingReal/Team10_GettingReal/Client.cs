using System;

public class Client : User
{
    public Client(string name, string email, string password, string userName, bool isAdmin) : base(name, email, password, userName, isAdmin)
    {
    }
    public override string GetUserInformation()
    {
        throw new NotImplementedException();
    }

    public override bool IsUserLoggedIn()
    {
        throw new NotImplementedException();
    }
    public DateTime LastLoginDate(Client client)
    { 
        throw new NotImplementedException();
    }
    public Note AddNoteToSkill(Skill skill)
    { 
        throw new NotImplementedException(); 
    }
    public void EditNoteToSkill(Note note)
    {
        throw new NotImplementedException();
    }
    public void DeleteNoteToSkill(Note note)
    {
        throw new NotImplementedException();
    }
}
