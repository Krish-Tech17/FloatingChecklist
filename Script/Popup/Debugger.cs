using UnityEngine;
using System.Collections.Generic;


public class Debugger : MonoBehaviour
{
    void Start()
    {
        //Pop-up
        /*string longMessage =
              "mod tempor incididunt ut labore et dolore magna aliqua. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.";

          // Call the popup controller directly
          //PopUpHandler.Instance.ShowPopup( "Information", longMessage, ProceedToNextStep,OnCloseButtonClicked, false); //condition 1
          //PopUpHandler.Instance.ShowPopup( "Information", longMessage, ProceedToNextStep,OnCloseButtonClicked, true ); //condition 2
          PopUpHandler.Instance.ShowYesNoPopup( "Information", longMessage, OnYesButtonClicked, OnNoButtonClicked, OnCloseButtonClicked ); // condition 3
          Debug.Log("Long message sent to PopUpController.");*/



        //==checklist test==

        //List<ChecklistData> testList = new List<ChecklistData>()
        //{
        //    new ChecklistData { id="C1", label="Check Battery Status", required=true, alreadychecked=false },
        //    new ChecklistData { id="C2", label="Clean Camera Lens", required=false, alreadychecked=true },
        //    new ChecklistData { id="C2", label="Check the door", required=true, alreadychecked=true },
        //    new ChecklistData { id="C2", label="Check Battery Status.", required=false, alreadychecked=true },
        //    new ChecklistData { id="C2", label="Clean Camera Lens", required=false, alreadychecked=true },
        //    new ChecklistData { id="C2", label="Submit the report", required=false, alreadychecked=true },
        //    new ChecklistData { id="C2", label="Clean Camera Lens", required=false, alreadychecked=true },
        //    new ChecklistData { id="C2", label="Clean Camera Lens", required=false, alreadychecked=true },
        //    new ChecklistData { id="C2", label="Clean Camera Lens", required=false, alreadychecked=true },
        //    new ChecklistData { id="C2", label="Clean Camera Lens", required=false, alreadychecked=true },
        //    new ChecklistData { id="C2", label="Clean Camera Lens", required=false, alreadychecked=true },
        //    new ChecklistData { id="C2", label="Clean Camera Lens", required=false, alreadychecked=true },
        //    new ChecklistData { id="C2", label="Clean Camera Lens", required=false, alreadychecked=true }
        //};

        //ChecklistController.Instance.Show(
        //    "Preflight Checklist",
        //    testList,
        //    OnChecklistSubmitted,
        //    OnChecklistClosed
        //);


        //var checklistRoot = ChecklistJSONLoader.LoadChecklist("MaintenanceChecklist");

        //ChecklistController.Instance.Show(
        //    checklistRoot.title,
        //    checklistRoot.checklist,
        //    OnChecklistSubmitted,
        //    OnChecklistClosed
        //);

        //var checklist = ChecklistJSONLoader.LoadChecklist("MaintenanceChecklist");
        //var checklist = ChecklistJSONLoader.LoadChecklist("InspectionChecklist");


        //ChecklistController.Instance.Show(
        //    checklist,
        //    OnChecklistSubmitted
        //);


    }

    private void ProceedToNextStep() //popup with ok button
    {
        Debug.Log("OK pressed: Proceeding...");
    }

    private void OnCloseButtonClicked() //popup close button login
    {
        Debug.Log("Close pressed: Popup closed.");
    }

    private void OnYesButtonClicked() // Yes No popup - On Yes Button clicked logic
    {
        Debug.Log("Yes pressed: Popup closed.");
    }

    private void OnNoButtonClicked() // Yes No popup - On No Button clicked logic
    {
        Debug.Log("No pressed: Popup closed.");
    }


    void OnChecklistSubmitted(List<ChecklistData> result)
    {
        Debug.Log("Checklist Submitted:");

        // foreach (var item in result)
        // {
        //     Debug.Log($"{item.id} - {item.label} - Checked: {item.alreadychecked}");
        // }
    }

    void OnChecklistClosed()
    {
        Debug.Log("Checklist Closed Without Submit.");
    }
}
