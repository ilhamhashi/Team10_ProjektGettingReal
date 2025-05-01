using System;
using System.Xml.Linq;

public class Note
{
    public int Id { get; set; }
    public string noteContent { get; set; }
    public Client client { get; set; }
    public Skill skill { get; set; }

    public Note(int id, string noteContent, Client client, Skill skill)
    {
        Id = id;
        this.noteContent = noteContent;
        this.client = client;
        this.skill = skill;
    }
    public string GetNoteInformation(int id)
    {
        return noteContent + client + skill;
    }
    public void DisplayAll()
    {

    }
  
}
