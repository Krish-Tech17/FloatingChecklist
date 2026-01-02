using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChecklistItemUI : MonoBehaviour
{

    public Toggle toggle;
    public TextMeshProUGUI label;
    private ChecklistData boundData;

    public void SetLabel(string text)
    {
        label.text = text;
    }

    public void SetRequired(bool isRequired)
    {
        if (isRequired)
        {
            label.text += " <color=#FF0000>*</color>";
        }
    }

    //public void BindData(ChecklistData data)
    //{
    //    boundData = data;
    //}

    public ChecklistData GetData()
    {
        return boundData;
    }

    public bool IsSelected()
    {
        return toggle.isOn;
    }

    public void BindData(ChecklistData data)
    {
        boundData = data;
        toggle.onValueChanged.RemoveAllListeners();
        toggle.onValueChanged.AddListener(OnToggleChanged);
    }

    private void OnToggleChanged(bool value)
    {
        boundData.currentstatus = value;
    }

}





