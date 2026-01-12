using UnityEngine;

// Working scipt for floating checklist - JSON loading from Resources folder

public class ChecklistJSONLoader : MonoBehaviour
{
    public ChecklistRoot LoadChecklist(string checklistId)
    {
        TextAsset json = Resources.Load<TextAsset>($"Checklists/{checklistId}");

        if (json == null)
        {
            Debug.LogError($"Checklist JSON not found: {checklistId}");
            return null;
        }

        return JsonUtility.FromJson<ChecklistRoot>(json.text);
    }
}


