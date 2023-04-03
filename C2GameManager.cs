using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C2GameManager : MonoBehaviour
{
    // [5] Object Name
    public GameObject scanObject;
    public Text talkText;
    // [6] Talk Panel
    public GameObject talkPanel;
    public bool isAction;

    public void Action(GameObject scanObj)
    {
        if(isAction)
        {
            isAction = false;
        }
        else
        {
            isAction = true;
            scanObject = scanObj;
            talkText.text = "Just a " + scanObject.name;
        }
        talkPanel.SetActive(isAction);
    }
}
