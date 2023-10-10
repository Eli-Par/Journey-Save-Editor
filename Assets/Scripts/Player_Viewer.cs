using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;

public class Player_Viewer : MonoBehaviour
{
    public Save_Edit edit;

    public PlayerUI[] playerDisplays;

    public GameObject copiedText;

    public float copiedDelay = 2f;

    public float copiedEndTime = -1;

    byte[] byteArray;

    public GameObject resetConfirmMenu;

    public bool confirmMenuActive = false;

    [Space]
    public string[] paths;

    public int[] findValue;

    // Start is called before the first frame update
    void Start()
    {
        Startup();
    }

    public void Startup()
    {
        //Get save data
        byteArray = edit.GetAllBytes();
        ASCIIEncoding ascii = new ASCIIEncoding();

		//Get the number of companions from the last game
        int companionNum = edit.GetBytes(5512);

		//Read the data about the 8 companions from the file
        for (int playerNum = 0; playerNum < 8; playerNum++)
        {
            //Set Player Values
            playerDisplays[playerNum].playerName = ascii.GetString(byteArray, 6568 + playerNum * 32, 24);
            playerDisplays[playerNum].id = BitConverter.ToInt64(byteArray, 6568 + 24 + playerNum * 32);
            playerDisplays[playerNum].symbol = edit.GetBytes(4608 + playerNum * 60);

            playerDisplays[playerNum].lastCompanion = playerNum < companionNum;

            //Refresh Player UI
            playerDisplays[playerNum].Refresh();

        }
        //TestFiles
        /*
        byte[,] testBytes = new byte[paths.Length, byteArray.Length];

        for (int i = 0; i < paths.Length; i++)
        {
            byte[] currentFile = edit.GetAllBytes(paths[i]);
            for (int j = 0; j < currentFile.Length; j++)
            {
                testBytes[i, j] = currentFile[j];
            }
        }

        for (int pos = 0; pos < byteArray.Length; pos++)
        {
            bool FoundOnAll = true;
            for (int file = 0; file < paths.Length; file++)
            {
                if (findValue[file] != testBytes[file, pos])
                {
                    FoundOnAll = false;
                    break;
                }
            }
            if (FoundOnAll)
            {
                Debug.Log("Found at " + pos + " | " + int.Parse(pos.ToString("X"), System.Globalization.NumberStyles.HexNumber));
            }
        }
        */

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time < copiedEndTime)
        {
            copiedText.SetActive(true);
        }
        else
        {
            copiedText.SetActive(false);
        }

    }

    public void ResetPlayer()
    {
        resetConfirmMenu.SetActive(true);
        confirmMenuActive = true;
        edit.PlayClick();
    }

	//Resets the byte that stores how many players are in the list to 0
    public void ConfirmResetPlayer()
    {
        resetConfirmMenu.SetActive(false);
        confirmMenuActive = false;
        edit.WriteBytes(5512, 0);
        Startup(); //Refresh companion list
        edit.PlayClick();
    }

    public void CancelResetPlayer()
    {
        resetConfirmMenu.SetActive(false);
        confirmMenuActive = false;
        edit.PlayClick();
    }
}
