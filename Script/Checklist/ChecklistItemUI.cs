using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChecklistItemUI : MonoBehaviour
{

    public Toggle toggle;
    public TextMeshProUGUI label;
    [SerializeField] private Button _noteButton;
    [SerializeField] private GameObject _noteInputArea;
    [SerializeField] private TMP_InputField _noteInputField;
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
       
        noteInputArea.SetActive(false);
        _noteInputField.text = data.note ?? string.Empty;

        _noteButton.onClick.RemoveAllListeners();
        _noteButton.onClick.AddListener(OnNoteButtonClicked);

        _noteInputField.onValueChanged.RemoveAllListeners();
        _noteInputField.onValueChanged.AddListener(OnNoteChanged);
    }

    private void OnToggleChanged(bool value)
    {
        boundData.currentstatus = value;
    }

    private void OnNoteButtonClicked()
    {
        _noteInputArea.SetActive(!_noteInputArea.activeSelf);
    }

    private void OnNoteChanged(string value)
    {
        if (boundData == null) return;
        boundData.note = value;
    }

    private void OnDisable()
    {
        _noteButton.onClick.RemoveAllListeners();
        _noteInputField.onValueChanged.RemoveAllListeners();
    }

}





