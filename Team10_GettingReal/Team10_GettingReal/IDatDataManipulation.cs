using System;

public interface IDatDataManipulation
{
    string name { get; }
    string description { get; }
    string GetInformation();
    void DisplayAll(string name);
    void SortData();

}
