using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JourneyCountUI : MonoBehaviour
{
    public Save_Edit edit;
    public TextMeshProUGUI counter;

    // Start is called before the first frame update
    void Start()
    {
        Startup();
    }

    public void Startup()
    {
        counter.text = "Journeys Completed: " + edit.GetBytes(76);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
