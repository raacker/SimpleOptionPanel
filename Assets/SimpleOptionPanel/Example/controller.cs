using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class controller : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Button button = GameObject.Find("Button").GetComponent<Button>();
        button.onClick.AddListener(() => { OpenPartyOptionPanel(); });
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OpenPartyOptionPanel()
    {
        OptionPanel.GetInstance().Open(
            "Party attend RSVP", "Are you going to the party?", OptionPanel.eButtonNums.THREE,
            "OK", () => { Debug.Log("OK. I will go"); },
            "Well...", () => { Debug.Log("Well... I'm not sure"); },
            "Cancel", () => { Debug.Log("No."); }
        );
    }
}
