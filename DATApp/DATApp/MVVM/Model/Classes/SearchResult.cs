using System;
using System.Collections.Generic;

namespace DATApp.MVVM.Model.Classes
{

public class SearchResult
  { 
public string Category {  get; set; }
public string Description { get; set; }
public int ID { get; set; }
public Skill OriginatingSkill { get; set; }
public Dictionary<string, string> SkillInformation { get; set; }

  }
}

//Anna