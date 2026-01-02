using UnityEngine;

public class ChecklistJSONLoader
{
    public static ChecklistRoot LoadChecklist(string jsonFileName)
    {
        TextAsset json = Resources.Load<TextAsset>($"Checklists/{jsonFileName}");

        if (json == null)
        {
            Debug.LogError($"Checklist JSON not found: {jsonFileName}");
            return null;
        }

        return JsonUtility.FromJson<ChecklistRoot>(json.text);
    }
}
