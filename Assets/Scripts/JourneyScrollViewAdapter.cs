using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;
using System.IO;
using System.Text.RegularExpressions;
using System;
using System.Globalization;

public class JourneyScrollViewAdapter : MonoBehaviour {

    public RectTransform prefab;
    public ScrollRect scrollView;
    public RectTransform content;
    public GameObject overview;
    public GameObject addingjourney;
    private Journey journey;
    public TMP_InputField MName;
    public TMP_InputField mDate;
    public TMP_InputField geld;
    private List<Journey> journeys;
    private PackingList packingList;
    private SortedDictionary<string, bool> sortedDictionaryPacking;
    private SortedDictionary<string, int> sortedDictionaryBudget;
    private Budget budget;
    private List<Airplane> airplane;
    private List<Train> train;
    private List<Bus> bus;

    private string path;
    private WWW www;

    List<ItemView> views = new List<ItemView>();

    // Use this for initialization
    /// <summary>
    /// Initialize the ScrollView in the beginning by reading the .json-File that lies in the persistentDataPath
    /// </summary>
    void Start () {

        journeys = new List<Journey>();
        //File.Delete(Path.Combine(Application.persistentDataPath, "journey.json"));
        try {
            journeys = JsonConvert.DeserializeObject<List<Journey>>(File.ReadAllText(Path.Combine(Application.persistentDataPath, "journey.json")));

            var fromFile = new ItemModel();
            foreach (Journey journ in journeys)
            {

                path = journ.PicturePath;
                fromFile.date = journ.Date;
                fromFile.name = journ.Name;
                OnReceiveNewModel(fromFile);
            }
        } catch (FileNotFoundException) {
            Debug.Log("File is not written yet");
        }
    }

    /// <summary>
    /// The method checks if the fields are empty and if not generates a new itemmodel with the
    /// values from the fields. Also saves a journey in the .json-File and generates the packinglistitems
    /// </summary>
    public void UpdateItem() {
        //DateTime dt;
        int resultInt;
        if (string.IsNullOrEmpty(MName.text))
        {
            SSTools.ShowMessage("Please enter name", SSTools.Position.bottom, SSTools.Time.twoSecond);
        }
        else if (string.IsNullOrEmpty(mDate.text))
        {
            SSTools.ShowMessage("Please enter date", SSTools.Position.bottom, SSTools.Time.twoSecond);
        }
        /*else if (!DateTime.TryParseExact(mDate.text, "dd.MM.yyyy - dd.MM.yyyy", CultureInfo.InvariantCulture,
                                       DateTimeStyles.None, out dt))
        {
            SSTools.ShowMessage("Please enter in format dd.mm.jjjj - dd.mm.jjjj", SSTools.Position.bottom, SSTools.Time.twoSecond);
        }*/
        else if (string.IsNullOrEmpty(geld.text))
        {
            SSTools.ShowMessage("Please enter budget", SSTools.Position.bottom, SSTools.Time.twoSecond);
        }
        else if (!int.TryParse(geld.text, out resultInt))
        {
            SSTools.ShowMessage("budget in numbers", SSTools.Position.bottom, SSTools.Time.twoSecond);
        }
        else
        {
            sortedDictionaryPacking = new SortedDictionary<string, bool>();
            sortedDictionaryPacking.Add("Auslandswährung", false);
            sortedDictionaryPacking.Add("Bargeld", false);
            sortedDictionaryPacking.Add("Bauch-/Gürteltasche", false);
            sortedDictionaryPacking.Add("Handtasche", false);
            sortedDictionaryPacking.Add("Rucksack", false);
            sortedDictionaryPacking.Add("EC-Karte", false);
            sortedDictionaryPacking.Add("Geldversteck", false);
            sortedDictionaryPacking.Add("Kreditkarte", false);
            sortedDictionaryPacking.Add("Portmonee", false);

            sortedDictionaryPacking.Add("Apres Lotion", false);
            sortedDictionaryPacking.Add("Haarbürste/Kamm", false);
            sortedDictionaryPacking.Add("Deodorant", false);
            sortedDictionaryPacking.Add("Shampoo", false);
            sortedDictionaryPacking.Add("Haarkur", false);
            sortedDictionaryPacking.Add("Haargummi", false);
            sortedDictionaryPacking.Add("Handcreme", false);
            sortedDictionaryPacking.Add("Kontaktlinsen + Zubehör", false);
            sortedDictionaryPacking.Add("Kulturtasche", false);
            sortedDictionaryPacking.Add("Labello", false);
            sortedDictionaryPacking.Add("Nagelpflegeset", false);
            sortedDictionaryPacking.Add("Wattestäbchen", false);
            sortedDictionaryPacking.Add("Rasierer", false);
            sortedDictionaryPacking.Add("Make-up/Make-up-Entferner", false);
            sortedDictionaryPacking.Add("Sonnencreme", false);
            sortedDictionaryPacking.Add("Damenhygieneartikel (Tampons etc.)", false);
            sortedDictionaryPacking.Add("Taschentücher", false);
            sortedDictionaryPacking.Add("Verhütungsmittel", false);
            sortedDictionaryPacking.Add("Zahnbürste", false);
            sortedDictionaryPacking.Add("Zahnpasta", false);

            sortedDictionaryPacking.Add("Badesachen", false);
            sortedDictionaryPacking.Add("Fleecepullover", false);
            sortedDictionaryPacking.Add("Gürtel", false);
            sortedDictionaryPacking.Add("Hosen/Shorts, kurz", false);
            sortedDictionaryPacking.Add("Hosen/Jeans, lang", false);
            sortedDictionaryPacking.Add("Kleid", false);
            sortedDictionaryPacking.Add("Rock", false);
            sortedDictionaryPacking.Add("Mütze/Cap/Hüte", false);
            sortedDictionaryPacking.Add("Pullover/Sweatshirt", false);
            sortedDictionaryPacking.Add("Regenjacke/Regencape", false);
            sortedDictionaryPacking.Add("Schlafanzug", false);
            sortedDictionaryPacking.Add("Socken", false);
            sortedDictionaryPacking.Add("Schuhe", false);
            sortedDictionaryPacking.Add("Sonnenbrille", false);
            sortedDictionaryPacking.Add("Trekkinghose", false);
            sortedDictionaryPacking.Add("T-Shirts", false);
            sortedDictionaryPacking.Add("Unterwäsche", false);
            sortedDictionaryPacking.Add("Wander-/Funktionssocken", false);

            sortedDictionaryPacking.Add("Medikamente", false);

            sortedDictionaryPacking.Add("ADAC Unterlagen", false);
            sortedDictionaryPacking.Add("Adressliste für Postkarten", false);
            sortedDictionaryPacking.Add("Auslandskrankenversicherung", false);
            sortedDictionaryPacking.Add("Führerschein", false);
            sortedDictionaryPacking.Add("Hotel-/Hostelunterlagen", false);
            sortedDictionaryPacking.Add("Impfpass", false);
            sortedDictionaryPacking.Add("Karte", false);
            sortedDictionaryPacking.Add("Krankenversichertenkarte", false);
            sortedDictionaryPacking.Add("Mietwagenunterlagen", false);
            sortedDictionaryPacking.Add("Personalausweis", false);
            sortedDictionaryPacking.Add("Reiseführer", false);
            sortedDictionaryPacking.Add("Reisepass", false);
            sortedDictionaryPacking.Add("Reisetagebuch", false);
            sortedDictionaryPacking.Add("Studentenausweis", false);
            sortedDictionaryPacking.Add("Tauchnachweis/Segelschein/etc.", false);
            sortedDictionaryPacking.Add("Visum", false);
            sortedDictionaryPacking.Add("Wegbeschreibung", false); 
            sortedDictionaryPacking.Add("Zugticket/Bahncard/Flugticket", false);

            sortedDictionaryPacking.Add("aufblasbares Kopfkissen", false);
            sortedDictionaryPacking.Add("Brille + Etui", false);
            sortedDictionaryPacking.Add("Buch/Zeitschrift", false);
            sortedDictionaryPacking.Add("Kofferanhänger", false);
            sortedDictionaryPacking.Add("Luftpumpe", false);
            sortedDictionaryPacking.Add("Ohrstöpsel", false);
            sortedDictionaryPacking.Add("Regenschirm", false);
            sortedDictionaryPacking.Add("Reisehandtücher", false);
            sortedDictionaryPacking.Add("Schreibzeug", false); 
            sortedDictionaryPacking.Add("Spiele", false);
            sortedDictionaryPacking.Add("Taschenlampe", false);

            sortedDictionaryPacking.Add("Liege", false);
            sortedDictionaryPacking.Add("Schnorchelausrüstung", false);
            sortedDictionaryPacking.Add("Sonnenschirm", false);
            sortedDictionaryPacking.Add("Strandmuschel", false);
            sortedDictionaryPacking.Add("Strandtuch", false);
            sortedDictionaryPacking.Add("Windschutz", false);

            sortedDictionaryPacking.Add("Kamera", false);
            sortedDictionaryPacking.Add("eBook-Reader", false);
            sortedDictionaryPacking.Add("Handy + Ladekabel", false);
            sortedDictionaryPacking.Add("wasserdichte Handyhülle", false);
            sortedDictionaryPacking.Add("Kopfhörer", false);
            sortedDictionaryPacking.Add("Ladegeräte", false);
            sortedDictionaryPacking.Add("Powerbank", false);
            sortedDictionaryPacking.Add("Speicherkarten", false);
            sortedDictionaryPacking.Add("Selfie-Stick", false);
            sortedDictionaryPacking.Add("Steckdosenadapter", false);
            sortedDictionaryPacking.Add("Boxen", false);


            packingList = new PackingList(sortedDictionaryPacking);



            sortedDictionaryBudget = new SortedDictionary<string, int>();
            budget = new Budget(int.Parse(geld.text), sortedDictionaryBudget);
            airplane = new List<Airplane>();
            train = new List<Train>();
            bus = new List<Bus>();

            path = PlayerPrefs.GetString("path");

            journey = new Journey(MName.text, mDate.text, path, packingList, budget, airplane, train, bus);
            var result = new ItemModel();
            result.date = mDate.text;
            result.name = MName.text;
            //result.name = journey.GetName();
            OnReceiveNewModel(result);
            SaveToJSON save = new SaveToJSON();
            journeys.Add(journey);
            save.SaveJourney(journeys);


        }

    }

    /// <summary>
    /// Generates an instance in Unity from the journeyprefab with the values given from the UpdateItem() method.
    /// </summary>
    void OnReceiveNewModel (ItemModel model) {
       

        views.Clear();

        var instance = Instantiate(prefab.gameObject) as GameObject;

        Debug.Log(path);

        if (path != null)
        {
            //StartCoroutine(ReadImage(path));
            www = new WWW("file://" + path);

        }
        instance.GetComponent<RawImage>().texture = www.texture;

        instance.transform.SetParent(content, false);
        var view = InitializeItemView(instance, model);
        views.Add(view);

    }

    /// <summary>
    /// Initializes the ItemView and sets the values
    /// </summary>
    ItemView InitializeItemView (GameObject viewGameObject, ItemModel itemModel) {
        ItemView view = new ItemView(viewGameObject.transform);

        view.name.text = itemModel.name;
        view.date.text = itemModel.date;
        view.texture = itemModel.texture;

        MName.text = "";
        mDate.text = "";
        geld.text = "";
        addingjourney.SetActive(false);
        overview.SetActive(true);
        

        return view;
    }

    /// <summary>
    /// class that holdes the values from the prefab and the itemmodel
    /// </summary>
    public class ItemView {

        public TextMeshProUGUI name;
        public TextMeshProUGUI date;
        public Texture texture;
        public ItemModel itemModel {
            set{
                if(value != null) {
                    name.text = value.name;
                }
            }
        }

        public ItemView (Transform rootView) {
            name = rootView.Find("AimText").GetComponent<TextMeshProUGUI>();
            date = rootView.Find("DateText").GetComponent<TextMeshProUGUI> ();
            texture = rootView.GetComponent<RawImage>().texture;
        }
    }

    /// <summary>
    /// class for the itemmodel that is adjusted to the prefab given
    /// </summary>
    public class ItemModel {
        public string name;
        public string date;
        public Texture texture;
    }


    IEnumerator ReadImage(string path)
    {
        www = new WWW("file://" + path);
        yield return www;

    }

   

}
