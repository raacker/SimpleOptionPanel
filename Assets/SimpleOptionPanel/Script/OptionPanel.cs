using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionPanel : MonoBehaviour {
    public enum eButtonNums { ONE, TWO, THREE };
    
    private static OptionPanel optionPanel;
    private static Vector3 button1DefaultPosition;
    private static Vector3 button2DefaultPosition;
    private static Vector3 button3DefaultPosition;

    public static OptionPanel GetInstance()
    {
        if(!optionPanel)
        {
            optionPanel = FindObjectOfType(typeof (OptionPanel)) as OptionPanel;
            Button closeButton = optionPanel.transform.Find("CloseButton").GetComponent<Button>();
            closeButton.onClick.AddListener(CloseOptionPanel);
            button1DefaultPosition = optionPanel.transform.Find("OptionButton1").GetComponent<Button>().transform.position;
            button2DefaultPosition = optionPanel.transform.Find("OptionButton2").GetComponent<Button>().transform.position;
            button3DefaultPosition = optionPanel.transform.Find("OptionButton3").GetComponent<Button>().transform.position;
        }
        return optionPanel;
    }

    public void Open(string titleData, string messageData, eButtonNums buttonNums,
        string button1Name, UnityEngine.Events.UnityAction button1Action,
        string button2Name = null, UnityEngine.Events.UnityAction button2Action = null,
        string button3Name = null, UnityEngine.Events.UnityAction button3Action = null)
    {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;

        Text title = transform.Find("Title").GetComponent<Text>();
        title.text = titleData;

        Text message = transform.Find("Message").GetComponent<Text>();
        message.text = messageData;

        Button button1 = transform.Find("OptionButton1").GetComponent<Button>();
        Button button2 = transform.Find("OptionButton2").GetComponent<Button>();
        Button button3 = transform.Find("OptionButton3").GetComponent<Button>();
        
        if (buttonNums == eButtonNums.ONE)
        {
            button1.transform.position = button3DefaultPosition;
            SetButton(button1, button1Name, button1Action);
            button2.gameObject.SetActive(false);
            button3.gameObject.SetActive(false);
        }
        else if (buttonNums == eButtonNums.TWO)
        {
            if (button2Name == null)
            {
                Debug.Log("Wrong number of buttons");
                return;
            }
            button1.transform.position = button2DefaultPosition;
            SetButton(button1, button1Name, button1Action);
            button2.transform.position = button3DefaultPosition;
            SetButton(button2, button2Name, button2Action);
            button3.gameObject.SetActive(false);
        }
        else if (buttonNums == eButtonNums.THREE)
        {
            if (button2Name == null || button3Name == null)
            {
                Debug.Log("Wrong number of buttons");
                return;
            }
            button1.transform.position = button1DefaultPosition;
            button2.transform.position = button2DefaultPosition;
            button3.transform.position = button3DefaultPosition;
            SetButton(button1, button1Name, button1Action);
            SetButton(button2, button2Name, button2Action);
            SetButton(button3, button3Name, button3Action);
        }
        else
        {
            Debug.Log("OptionPanel supports 1 to 3 buttons only");
            return;
        }

        optionPanel.gameObject.transform.SetAsLastSibling();
    }

    private void SetButton(Button button, string name, UnityEngine.Events.UnityAction action)
    {
        button.onClick.RemoveAllListeners();
        if (action != null)
        {
            button.onClick.AddListener(action);
        }
        button.onClick.AddListener(CloseOptionPanel);
        Text buttonText = button.transform.GetChild(0).GetComponent<Text>();
        buttonText.text = name;
        button.gameObject.SetActive(true);
    }

    public static void CloseOptionPanel()
    {
        CanvasGroup canvasGroup = optionPanel.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }
}
