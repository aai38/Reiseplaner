using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using System.IO;
using TMPro;
using UnityEngine.UI;
using System.Net;
using System;

/// <summary>
/// class initializes the overviewscreen and controlls every action in it
/// </summary>
public class OverviewController : MonoBehaviour {

    private List<Journey> journeys;
    private TextMeshProUGUI nameJourney;
    public TextMeshProUGUI total;
    public TextMeshProUGUI spent;
    public TextMeshProUGUI left;
    public TMP_InputField nameItem;
    public Transform BudgetBar;
    public RectTransform checkBox;
    public RectTransform Packingcontent;
    private Journey actualJourney;
    private string journeyName;
    private float totalSpent;

    public GameObject overview;
    public GameObject addItem;


    // Use this for initialization
    /// <summary>
    /// method initializes the tabs in the app, sets the heading
    /// </summary>
    void Start () {

        journeys = JsonConvert.DeserializeObject<List<Journey>>(File.ReadAllText(Path.Combine(Application.persistentDataPath, "journey.json")));
        journeyName = PlayerPrefs.GetString("journeyclicked");
        actualJourney = journeys.Find(x => x.Name.Equals(journeyName));
        nameJourney = GameObject.FindWithTag("Name").GetComponent<TextMeshProUGUI>();
        nameJourney.SetText(journeyName);
        totalSpent = 0;

        SetPackingList();

        if (actualJourney.Budget.Expenditures != null)
        {
            foreach (KeyValuePair<string, int> budget in actualJourney.Budget.Expenditures)
            {
                totalSpent += budget.Value;

            }
            spent.SetText("Spent: " + totalSpent + "€");
            left.SetText("Left: " + (actualJourney.Budget.Total - totalSpent) + "€");
            BudgetBar.GetComponent<Image>().fillAmount = (totalSpent / actualJourney.Budget.Total);
        }
        else
        {
            spent.SetText("Spent: 0€");
            left.SetText("Left: " + actualJourney.Budget.Total + "");
            BudgetBar.GetComponent<Image>().fillAmount = 0;

        }
        total.SetText("Total: " + actualJourney.Budget.Total + "€");

       
    }

    /// <summary>
    /// method to come back to the mainscreen
    /// </summary>
    public void GoBack()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }


    /// <summary>
    /// method for opening the whole route in googlemaps
    /// </summary>
    public void OpenOnMap() {
        journeyName = PlayerPrefs.GetString("journeyclicked");
        string origin = PlayerPrefs.GetString("firstadded" + journeyName);
        string destination = PlayerPrefs.GetString("lastadded" + journeyName);
        string url = string.Format("https://www.google.com/maps/dir/?api=1&origin={1}&destination={0}&travelmode=transit", destination, origin);
        Application.OpenURL(url);
        //Application.OpenURL("https://www.google.com/maps/dir/?api=1&origin=Hamburg&destination=London&travelmode=transit"); this does work but without variables

    }

    /// <summary>
    /// method changes the boolean-value in the .json if user clicks on checkbox
    /// </summary>
    public void CheckToggle(GameObject changedToggle, bool isChecked) {
        if(isChecked) {
            journeys.Find(x => x.Name.Equals(journeyName)).PackingList.item[changedToggle.GetComponentInChildren<TextMeshProUGUI>().text] = true;
        } else {
            journeys.Find(x => x.Name.Equals(journeyName)).PackingList.item[changedToggle.GetComponentInChildren<TextMeshProUGUI>().text] = false;

        }
        SaveToJSON saveToJSON = new SaveToJSON();
        saveToJSON.SaveJourney(journeys);
    }

    /// <summary>
    /// method should use the db api for getting more information about trains, doesn't work so
    /// </summary>
    /* public void ShowMoreTrains() {

             HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://api.deutschebahn.com/freeplan/v1/location/berlin");
             request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

             using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
             using (Stream stream = response.GetResponseStream())
             using (StreamReader reader = new StreamReader(stream))
             {
                 result = reader.ReadToEnd();

             }

     }*/

    /// <summary>
    /// this method adds a new checkbox to the packinglist and saves the item in the .json-file
    /// </summary>
    public void AddItem()
    {
        if (string.IsNullOrEmpty(nameItem.text)) {
            SSTools.ShowMessage("name empty", SSTools.Position.bottom, SSTools.Time.twoSecond);
        } else {
            journeyName = PlayerPrefs.GetString("journeyclicked");
            journeys.Find(x => x.Name.Equals(journeyName)).PackingList.item.Add(nameItem.text, false);
            SaveToJSON save = new SaveToJSON();
            save.SaveJourney(journeys);
            GameObject toggle = (GameObject)Instantiate(checkBox.gameObject) as GameObject;
            toggle.transform.SetParent(Packingcontent, false);
            toggle.GetComponentInChildren<TextMeshProUGUI>().text = nameItem.text;
            toggle.GetComponentInChildren<Toggle>().onValueChanged.AddListener(delegate (bool b) { CheckToggle(toggle, b); });
            toggle.GetComponentInChildren<Toggle>().isOn = false;

            addItem.SetActive(false);
            overview.SetActive(true);
        }
    }

    /// <summary>
    /// method to initialize the packinglist. So looks if the boolean-value is true or false and generates toogles like that.
    /// </summary>
    private void SetPackingList ()
    {
        foreach(KeyValuePair<string, bool> packing in actualJourney.PackingList.item) {

            GameObject toggle = (GameObject)Instantiate(checkBox.gameObject) as GameObject;
            toggle.transform.SetParent(Packingcontent, false);
            toggle.GetComponentInChildren<TextMeshProUGUI>().text = packing.Key;
            toggle.GetComponentInChildren<Toggle>().onValueChanged.AddListener(delegate (bool b) { CheckToggle(toggle, b); });

            if (packing.Value) {
                toggle.GetComponentInChildren<Toggle>().isOn = true;
            }
            else {
                toggle.GetComponentInChildren<Toggle>().isOn = false;
            }

        } 
    }
}

