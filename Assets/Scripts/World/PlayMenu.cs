using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayMenu : MonoBehaviour {
    public string selectedMod="Survival";

    private GameObject ModeText;
    private Button NButton;
    public Sprite surv;
    public Sprite dm;
    public Sprite team_dm;

    public Sprite L_Disabled;
    public Sprite R_Disabled;

    public Sprite L_Enabled;
    public Sprite R_Enabled;
    Image img;

    public void NextMode()
    {
        ModeText = GameObject.Find("Mode");
        img = ModeText.GetComponent<Image>();
        switch (selectedMod)
        {
            
            case "Survival":
                selectedMod = "Deathmatch";
                img.sprite = dm;
                NButton = GameObject.Find("L_Scroll").GetComponent<Button>();
                NButton.enabled = true;
                NButton.image.sprite = L_Enabled;
                break;
            case "Deathmatch":
                NButton = GameObject.Find("R_Scroll").GetComponent<Button>();
                NButton.enabled = false;
                NButton.image.sprite = R_Disabled;
                img.sprite = team_dm;
                selectedMod = "Team_Deathmatch";
                break;
            default:
                
                break;
        }

    }
    public void PreviousMode()
    {
        ModeText = GameObject.Find("Mode");
        img = ModeText.GetComponent<Image>();
        switch (selectedMod)
        {

            case "Team_Deathmatch":
                selectedMod = "Deathmatch";
                img.sprite = dm;
                NButton = GameObject.Find("R_Scroll").GetComponent<Button>();
                NButton.enabled = true;
                NButton.image.sprite = R_Enabled;
                break;
            case "Deathmatch":
                selectedMod = "Survival";
                img.sprite = surv;
                NButton = GameObject.Find("L_Scroll").GetComponent<Button>();
                NButton.enabled = false;
                NButton.image.sprite = L_Disabled;
                break;
            default:
                
                break;
        }

    }

}
