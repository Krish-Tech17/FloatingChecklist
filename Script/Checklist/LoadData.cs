using UnityEngine;
using System.Collections.Generic;


[System.Serializable]
public class ChecklistRoot
{
    public string title;
    public bool showCloseButton;
    public List<ChecklistData> checklist;
}


[System.Serializable]
public class ChecklistData
{
    public string id;
    public string label;
    public bool required;
    public bool alreadychecked;
    public bool currentstatus;
}

