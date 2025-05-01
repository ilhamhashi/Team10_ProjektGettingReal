using System;
public enum Level
{
	None = 0,
	Low = 1,
	Medium = 2,
	High = 3
}

public class Skill : IDatDataManipulation
{
	public string name {  get; set; }
	public string description { get; set; }
	public string goal { get; set; }
	public string validationText { get; set; }
	public string emotionMatch { get; set; }
	public Level level { get; set; }
	public Note note { get; set; }
	public Module module { get; set; }

    public Skill(string name, string description, string goal, string validationText, string emotionMatch, Level level, Note note, Module module)
    {
        this.name = name;
        this.description = description;
        this.goal = goal;
        this.validationText = validationText;
        this.emotionMatch = emotionMatch;
        this.level = level;
        this.note = note;
        this.module = module;
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
