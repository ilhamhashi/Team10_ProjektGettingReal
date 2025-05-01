using System;

public class Module : IDatDataManipulation
{
    public string name {  get; set; }
    public string description { get; set; }

    public Module(string name, string description)
    {
        this.name = name;
        this.description = description;
    }

    public string GetInformation()
    {
        return name;
    }
    public void DisplayAll(string name)
    {

    }
    public void SortData()
    {

    }
}
