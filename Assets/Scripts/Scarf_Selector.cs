using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Scarf_Selector : MonoBehaviour
{
    private int scarfLength = 0;

    public Save_Edit edit;

    public Slider slider;

    public TextMeshProUGUI scarfText;

    public TextMeshProUGUI whiteLevelText;
    public TextMeshProUGUI redLevelText;

    public LengthData[] whiteScarfLength;
    public LengthData[] redScarfLength;

    // Start is called before the first frame update
    void Start()
    {
        Startup();
    }

    //Load scarf info and set slider value
    public void Startup()
    {
        scarfLength = edit.GetBytes(16);
        slider.value = scarfLength;
    }

    // Update is called once per frame
    void Update()
    {
        //Update scarfLength and scarf length text
        scarfLength = (int) slider.value;
        scarfText.text = "Length: " + scarfLength;

        int whiteLevelIndex = 0;
        int redLevelIndex = 0;

		//Determine the earliest level that the specified scarf length is legitimately obtainable in
        for (int i = 0; i < whiteScarfLength.Length; i++)
        {
            if (scarfLength >= whiteScarfLength[i].levelLength)
            {
                whiteLevelIndex = i;
            }

            if (scarfLength >= redScarfLength[i].levelLength)
            {
                redLevelIndex = i;
            }
        }

		//Output the earliest level that the scarf length is legitimately obtainable in
        whiteLevelText.text = "White Robe in " + whiteScarfLength[whiteLevelIndex].levelName;
        redLevelText.text = "Red Robe in " + redScarfLength[redLevelIndex].levelName;
    }

    public void SaveLength()
    {
        edit.WriteBytes(16, scarfLength);
        edit.PlayClick();
    }
}

[System.Serializable]
public class LengthData
{
    public string levelName;
    public int levelLength;
}
