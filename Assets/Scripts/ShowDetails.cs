using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// class that opens the details for a connection
/// </summary>
public class ShowDetails : MonoBehaviour, IPointerClickHandler {
    private GameObject showDetailedPlaneInformation;
    private GameObject showDetailedTrainInformation;
    private GameObject showDetailedBusInformation;


    // Use this for initialization
    /// <summary>
    /// finds in the beginning the objects that shows later the details
    /// </summary>
    void Start () {
       

        //Find inactive Gameobjects
        Transform[] trs = GameObject.FindWithTag("Kategorie1").GetComponentsInChildren<Transform>(true);
        foreach (Transform t in trs)
        {
            if (t.name == "ShowDetailedPlaneInformation")
            {
                showDetailedPlaneInformation = t.gameObject;
            } else if (t.name == "ShowDetailedTrainInformation") {
                showDetailedTrainInformation = t.gameObject;
            } else if (t.name == "ShowDetailedBusInformation") {
                showDetailedBusInformation = t.gameObject;
            }
        }
    }



    /// <summary>
    /// method works as a listener by a pointerclick-event and shows the appropriate detailsscreen
    /// </summary>
    public void OnPointerClick(PointerEventData eventData)
    {
        TextMeshProUGUI text = eventData.pointerCurrentRaycast.gameObject.transform.Find("AimText").GetComponent<TextMeshProUGUI>();
        Image image = eventData.pointerCurrentRaycast.gameObject.transform.Find("ConectionImage").GetComponent<Image>(); ;


        //BudgetPane-Tag is here Overview
        Debug.Log(image.sprite.name);
        Debug.Log(text.text);
        GameObject.FindWithTag("BudgetPane").SetActive(false);
        PlayerPrefs.SetString("nameconnectionclicked", text.text);
        if (image.sprite.name.Equals("train"))
        {
            PlayerPrefs.SetString("connectionclicked", "train");
            showDetailedTrainInformation.SetActive(true);
        }
        else if (image.sprite.name.Equals("bus"))
        {
            PlayerPrefs.SetString("connectionclicked", "bus");
            showDetailedBusInformation.SetActive(true);
        }
        else if (image.sprite.name.Equals("airplane"))
        {
            showDetailedPlaneInformation.SetActive(true);
            PlayerPrefs.SetString("connectionclicked", "plane");

        }

    }
}
