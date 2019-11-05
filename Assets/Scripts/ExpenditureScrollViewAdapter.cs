using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;
using System.IO;
using System;

/// <summary>
/// Class for Adding elements to the ScrollView
/// </summary>

public class ExpenditureScrollViewAdapter : MonoBehaviour {

    public RectTransform prefab;
    public ScrollRect scrollView;
    public RectTransform content;
    public TMP_InputField expenditure;
    public TMP_InputField amount;

    public TextMeshProUGUI total;
    public TextMeshProUGUI spent;
    public TextMeshProUGUI left;

    public GameObject overview;
    public GameObject addExpenditure;

    private float totalSpent;
    public Transform BudgetBar;

    private List<Journey> journeys;
    List<ItemView> views = new List<ItemView>();

    // Use this for initialization
    /// <summary>
    /// Initialize the ScrollView in the beginning by reading the .json-File that lies in the persistentDataPath
    /// </summary>
    void Start () {
        journeys = new List<Journey>();
        totalSpent = 0;
        try
        {
            journeys = JsonConvert.DeserializeObject<List<Journey>>(File.ReadAllText(Path.Combine(Application.persistentDataPath, "journey.json")));
            var fromFile = new ItemModel();
            string nameJourney = PlayerPrefs.GetString("journeyclicked");
            Journey actualJourney = journeys.Find(x => x.Name.Equals(nameJourney));
            if (actualJourney.Budget.Expenditures != null)
            {
                foreach (KeyValuePair<string, int> budget in actualJourney.Budget.Expenditures)
                {
                    totalSpent += budget.Value;
                    fromFile.expenditure = budget.Key;
                    fromFile.amount = budget.Value.ToString();
                    OnReceiveNewModel(fromFile);
                }
                //spent.SetText("Spent: " + totalSpent + "€");
                //left.SetText("Left: " + (actualJourney.Budget.Total - totalSpent) + "€");
                //BudgetBar.GetComponent<Image>().fillAmount = (totalSpent / actualJourney.Budget.Total);
            }
            /*else
            {
                spent.SetText("Spent: 0€");
                left.SetText("Left: " + actualJourney.Budget.Total + "");
                BudgetBar.GetComponent<Image>().fillAmount = 0;

            }*/
        }
        catch (FileNotFoundException)
        {
            Debug.Log("File is not written yet");
        }
    }




    /// <summary>
    /// The method checks if the fields are empty and if not generates a new itemmodel with the
    /// values from the fields. Also saves an expenditure in the .json-File
    /// </summary>
    public void UpdateItem()
    {
        int resultInt;
        if (string.IsNullOrEmpty(expenditure.text))
        {
            SSTools.ShowMessage("name empty", SSTools.Position.bottom, SSTools.Time.twoSecond);
        }
        else if (string.IsNullOrEmpty(amount.text))
        {
            SSTools.ShowMessage("amount empty", SSTools.Position.bottom, SSTools.Time.twoSecond);
        }
        else if (!int.TryParse(amount.text, out resultInt))
        {
            SSTools.ShowMessage("amount in numbers", SSTools.Position.bottom, SSTools.Time.twoSecond);
        }
        else {

            var result = new ItemModel();
            result.expenditure = expenditure.text;
            result.amount = amount.text;
            totalSpent += Int32.Parse(amount.text);
            spent.SetText("Spent: " + totalSpent + "€");

            name = PlayerPrefs.GetString("journeyclicked");
            Journey actualJourney = journeys.Find(x => x.Name.Equals(name));
            Debug.Log(journeys);
            Debug.Log(actualJourney);
            left.SetText("Left: " + (actualJourney.Budget.Total - totalSpent) + "€");
            BudgetBar.GetComponent<Image>().fillAmount = (totalSpent / actualJourney.Budget.Total);
            //result.name = journey.GetName();

            SaveToJSON save = new SaveToJSON();
            save.SaveExpenditure(result.expenditure, int.Parse(result.amount), journeys);
            Debug.Log("save");

            OnReceiveNewModel(result);
        }

    }

    /// <summary>
    /// Generates an instance in Unity from the expenditureprefab with the values given from the UpdateItem() method.
    /// </summary>
    void OnReceiveNewModel(ItemModel model)
    {
        views.Clear();

        Debug.Log("receive");
        var instance = Instantiate(prefab.gameObject) as GameObject;
        instance.transform.SetParent(content, false);
        var view = InitializeItemView(instance, model);
        views.Add(view);

    }

    /// <summary>
    /// Initializes the ItemView and sets the values
    /// </summary>

    ItemView InitializeItemView(GameObject viewGameObject, ItemModel itemModel)
    {
        Debug.Log("itemview");
        ItemView view = new ItemView(viewGameObject.transform);

        view.expenditure.text = itemModel.expenditure ;
        view.amount.text = itemModel.amount + " €";
        name = PlayerPrefs.GetString("journeyclicked");
        Journey actualJourney = journeys.Find(x => x.Name.Equals(name));

        spent.SetText("Spent: " + totalSpent + "€");
        left.SetText("Left: " + (actualJourney.Budget.Total - totalSpent) + "€");
        BudgetBar.GetComponent<Image>().fillAmount = (totalSpent / actualJourney.Budget.Total);


        Debug.Log("active");
        overview.SetActive(true);
        addExpenditure.SetActive(false);

        return view;
    }

    /// <summary>
    /// class that holdes the values from the prefab and the itemmodel
    /// </summary>
    public class ItemView
    {

        public TextMeshProUGUI expenditure;
        public TextMeshProUGUI amount;
        public ItemModel itemModel
        {
            set
            {
                if (value != null)
                {
                    expenditure.text = value.expenditure;
                }
            }
        }

        public ItemView(Transform rootView)
        {
            expenditure = rootView.Find("ExpenditureText").GetComponent<TextMeshProUGUI>();
            amount = rootView.Find("AmountText").GetComponent<TextMeshProUGUI>();
        }
    }

    /// <summary>
    /// class for the itemmodel that is adjusted to the prefab given
    /// </summary>
    public class ItemModel
    {
        public string expenditure;
        public string amount;
    }
}
