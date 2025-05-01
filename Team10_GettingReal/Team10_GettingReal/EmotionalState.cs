using System;

public class EmotionalState : IDatDataManipulation
{
    public string name {  get; set; }
    public string description { get; set; }

    public EmotionalState(string name, string description)
    {
        this.name = name;
        this.description = description;
    }

    public string GetInformation()
    {
        throw new NotImplementedException();
    }
    public void DisplayAll(string name)
    {
        throw new NotImplementedException();
    }
    public void SortData()
    {
        throw new NotImplementedException();
    }
}
