using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;
using System.IO;

public class ConnectionsScrollViewAdapter : MonoBehaviour
{

    public GameObject overview;
    public GameObject addTrainObject;
    public GameObject addBusObject;
    public GameObject addPlaneObject;

    public RectTransform trainprefab;
    public RectTransform busprefab;
    public RectTransform planeprefab;
    public ScrollRect scrollView;
    public RectTransform content;

    public TMP_InputField trainDeparture;
    public TMP_InputField trainDestination;
    public TMP_InputField trainDate;
    public TMP_InputField trainTimeDeparture;
    public TMP_InputField trainTimeArrival;
    public TMP_InputField trainTrack;
    public TMP_InputField trainWagon;
    public TMP_InputField trainPlace;
    public TMP_InputField trainClass;


    public TMP_InputField busDeparture;
    public TMP_InputField busDestination;
    public TMP_InputField busDate;
    public TMP_InputField busline;
    public TMP_InputField busTimeDeparture;
    public TMP_InputField busTimeArrival;
    public TMP_InputField busPlace;

    public TMP_InputField airplaneDeparture;
    public TMP_InputField airplaneDestination;
    public TMP_InputField airplaneDate;
    public TMP_InputField airplaneTimeDeparture;
    public TMP_InputField airplaneTimeArrival;
    public TMP_InputField airplaneGate;
    public TMP_InputField airplaneAirplane;
    public TMP_InputField airplaneSeat;
    public TMP_InputField airplaneFlightnumber;
    public TMP_InputField airplaneTerminal;

    private List<Journey> journeys;
    private List<Airplane> airplanes;
    private List<Train> trains;
    private List<Bus> buses;
    private SaveToJSON save;
    private string nameJourney;

    List<ItemView> views = new List<ItemView>();

    // Use this for initialization
    /// <summary>
    /// Initialize the ScrollView in the beginning by reading the .json-File that lies in the persistentDataPath
    /// </summary>
    void Start()
    {
        nameJourney = PlayerPrefs.GetString("journeyclicked");
        if (PlayerPrefs.GetString("firstadded" + nameJourney).Equals(""))
        {
            PlayerPrefs.SetString("firstadded" + nameJourney, "-1");
        }
        journeys = new List<Journey>();
        save = new SaveToJSON();

        try
        {


            journeys = JsonConvert.DeserializeObject<List<Journey>>(File.ReadAllText(Path.Combine(Application.persistentDataPath, "journey.json")));
            var fromFile = new ItemModel();

            Journey actualJourney = journeys.Find(x => x.Name.Equals(nameJourney));
            airplanes = actualJourney.Airplane;
            trains = actualJourney.Train;
            buses = actualJourney.Bus;
            if (trains != null)
            {
                foreach (Train train in trains)
                {
                    fromFile.date = train.Date;
                    fromFile.name = train.Departure + " - " + train.Destination;
                    fromFile.time = train.TimeDeparture;

                    OnReceiveNewModelTrain(fromFile);
                }
            }

            if (buses != null)
            {
                foreach (Bus bus in buses)
                {
                    fromFile.date = bus.Date;
                    fromFile.name = bus.Departure + " - " + bus.Destination;
                    fromFile.time = bus.TimeDeparture;
                    OnReceiveNewModelBus(fromFile);
                }
            }

            if (airplanes != null)
            {
                foreach (Airplane plane in airplanes)
                {
                    fromFile.date = plane.Date;
                    fromFile.name = plane.Departure + " - " + plane.Destination;
                    fromFile.time = plane.TimeDeparture;
                    OnReceiveNewModelAirplane(fromFile);
                }
            }
        }
        catch (FileNotFoundException)
        {
            Debug.Log("No Connection is available yet");
        }
    }



    /// <summary>
    /// The method checks if the fields are empty and if not generates a new itemmodel with the
    /// values from the fields. Also saves an plane in the .json-File
    /// </summary>
    public void UpdateAirplaneItem()
    {
        if (string.IsNullOrEmpty(airplaneDeparture.text))
        {
            SSTools.ShowMessage("departure empty", SSTools.Position.bottom, SSTools.Time.twoSecond);
        }
        else if (string.IsNullOrEmpty(airplaneDestination.text))
        {
            SSTools.ShowMessage("destination empty", SSTools.Position.bottom, SSTools.Time.twoSecond);
        }
        else if (string.IsNullOrEmpty(airplaneDate.text))
        {
            SSTools.ShowMessage("date empty", SSTools.Position.bottom, SSTools.Time.twoSecond);
        }
        else if (string.IsNullOrEmpty(airplaneTimeDeparture.text))
        {
            SSTools.ShowMessage("departuretime empty", SSTools.Position.bottom, SSTools.Time.twoSecond);
        }
        else if (string.IsNullOrEmpty(airplaneTimeArrival.text))
        {
            SSTools.ShowMessage("arrivaltime empty", SSTools.Position.bottom, SSTools.Time.twoSecond);
        }
        else if (string.IsNullOrEmpty(airplaneGate.text))
        {
            SSTools.ShowMessage("gate empty", SSTools.Position.bottom, SSTools.Time.twoSecond);
        }
        else if (string.IsNullOrEmpty(airplaneTerminal.text))
        {
            SSTools.ShowMessage("terminal empty", SSTools.Position.bottom, SSTools.Time.twoSecond);
        }
        else if (string.IsNullOrEmpty(airplaneSeat.text))
        {
            SSTools.ShowMessage("seat empty", SSTools.Position.bottom, SSTools.Time.twoSecond);
        }
        else if (string.IsNullOrEmpty(airplaneFlightnumber.text))
        {
            SSTools.ShowMessage("flightnumber empty", SSTools.Position.bottom, SSTools.Time.twoSecond);
        }
        else if (string.IsNullOrEmpty(airplaneAirplane.text))
        {
            SSTools.ShowMessage("airline empty", SSTools.Position.bottom, SSTools.Time.twoSecond);
        }


        else
        {
            Airplane newAirplane = new Airplane(airplaneGate.text, airplaneAirplane.text, airplaneFlightnumber.text, airplaneTerminal.text,
                                                airplaneDate.text, airplaneSeat.text, airplaneTimeDeparture.text, airplaneTimeArrival.text,
                                                airplaneDeparture.text, airplaneDestination.text);
            airplanes.Add(newAirplane);
            save.SaveAirplaneConnection(newAirplane, journeys);

            var result = new ItemModel();
            result.date = airplaneDate.text;
            result.name = airplaneDeparture.text + " - " + airplaneDestination.text;
            result.time = airplaneTimeDeparture.text;
            if (PlayerPrefs.GetString("firstadded" + nameJourney).Equals("-1"))
            {
                PlayerPrefs.SetString("firstadded" + nameJourney, airplaneDeparture.text);
            }
            PlayerPrefs.SetString("lastadded" + nameJourney, airplaneDestination.text);

            airplaneDate.text = "";
            airplaneGate.text = "";
            airplaneAirplane.text = "";
            airplaneFlightnumber.text = "";
            airplaneTerminal.text = "";
            airplaneSeat.text = "";
            airplaneTimeArrival.text = "";
            airplaneTimeDeparture.text = "";
            airplaneDeparture.text = "";
            airplaneDestination.text = "";

            OnReceiveNewModelAirplane(result);
        }
    }

    /// <summary>
    /// The method checks if the fields are empty and if not generates a new itemmodel with the
    /// values from the fields. Also saves a bus in the .json-File
    /// </summary>

    public void UpdateBusItem()
    {
        if (string.IsNullOrEmpty(busDeparture.text))
        {
            SSTools.ShowMessage("departure empty", SSTools.Position.bottom, SSTools.Time.twoSecond);
        }
        else if (string.IsNullOrEmpty(busDestination.text))
        {
            SSTools.ShowMessage("destination empty", SSTools.Position.bottom, SSTools.Time.twoSecond);
        }
        else if (string.IsNullOrEmpty(busDate.text))
        {
            SSTools.ShowMessage("date empty", SSTools.Position.bottom, SSTools.Time.twoSecond);
        }
        else if (string.IsNullOrEmpty(busTimeDeparture.text))
        {
            SSTools.ShowMessage("departuretime empty", SSTools.Position.bottom, SSTools.Time.twoSecond);
        }
        else if (string.IsNullOrEmpty(busTimeArrival.text))
        {
            SSTools.ShowMessage("arrivaltime empty", SSTools.Position.bottom, SSTools.Time.twoSecond);
        }
        else
        {
            Bus newBus = new Bus(busPlace.text, busDate.text, busline.text, busTimeDeparture.text, busTimeArrival.text, busDeparture.text, busDestination.text);
            buses.Add(newBus);
            save.SaveBusConnection(newBus, journeys);

            var result = new ItemModel();
            result.date = busDate.text;
            result.name = busDeparture.text + " - " + busDestination.text;
            result.time = busTimeDeparture.text;
            if (PlayerPrefs.GetString("firstadded" + nameJourney).Equals("-1"))
            {
                PlayerPrefs.SetString("firstadded" + nameJourney, busDeparture.text);
            }
            PlayerPrefs.SetString("lastadded" + nameJourney, busDestination.text);

            busDeparture.text = "";
            busDestination.text = "";
            busDate.text = "";
            busTimeDeparture.text = "";
            busTimeArrival.text = "";
            busline.text = "";
            busPlace.text = "";


            OnReceiveNewModelBus(result);
        }
    }

    /// <summary>
    /// The method checks if the fields are empty and if not generates a new itemmodel with the
    /// values from the fields. Also saves a train in the .json-File
    /// </summary>

    public void UpdateTrainItem()
    {
        int resultInt;

        if (string.IsNullOrEmpty(trainDeparture.text))
        {
            SSTools.ShowMessage("departure empty", SSTools.Position.bottom, SSTools.Time.twoSecond);
        }
        else if (string.IsNullOrEmpty(trainDestination.text))
        {
            SSTools.ShowMessage("destination empty", SSTools.Position.bottom, SSTools.Time.twoSecond);
        }
        else if (string.IsNullOrEmpty(trainDate.text))
        {
            SSTools.ShowMessage("date empty", SSTools.Position.bottom, SSTools.Time.twoSecond);
        }
        else if (string.IsNullOrEmpty(trainTimeDeparture.text))
        {
            SSTools.ShowMessage("departuretime empty", SSTools.Position.bottom, SSTools.Time.twoSecond);
        }
        else if (string.IsNullOrEmpty(trainTimeArrival.text))
        {
            SSTools.ShowMessage("arrivaltime empty", SSTools.Position.bottom, SSTools.Time.twoSecond);
        }

        else if (!int.TryParse(trainClass.text, out resultInt)) {
            SSTools.ShowMessage("class in numbers", SSTools.Position.bottom, SSTools.Time.twoSecond);
        } else {

            Train newTrain = new Train(trainTrack.text, trainWagon.text, trainPlace.text, int.Parse(trainClass.text),
                                       trainDate.text, trainTimeDeparture.text, trainTimeArrival.text, trainDeparture.text, trainDestination.text);
            trains.Add(newTrain);


            save.SaveTrainConnection(newTrain, journeys);

            var result = new ItemModel();
            result.date = trainDate.text;
            result.name = trainDeparture.text + " - " + trainDestination.text;
            result.time = trainTimeDeparture.text;
            if (PlayerPrefs.GetString("firstadded" + nameJourney).Equals("-1"))
            {
                PlayerPrefs.SetString("firstadded" + nameJourney, trainDeparture.text);
            }
            PlayerPrefs.SetString("lastadded" + nameJourney, trainDestination.text);

            trainDeparture.text = "";
            trainDestination.text = "";
            trainDate.text = "";
            trainTimeDeparture.text = "";
            trainTimeArrival.text = "";
            trainPlace.text = "";
            trainTrack.text = "";
            trainWagon.text = "";
            trainClass.text = "";

            OnReceiveNewModelTrain(result);
        }
    }

    /// <summary>
    /// Generates an instance in Unity from the trainprefab with the values given from the UpdateItem() method.
    /// </summary>
    void OnReceiveNewModelTrain(ItemModel model)
    {
        views.Clear();

        var instance = Instantiate(trainprefab.gameObject) as GameObject;
        instance.transform.SetParent(content, false);
        overview.SetActive(true);
        addTrainObject.SetActive(false);

        var view = InitializeItemView(instance, model);
        views.Add(view);
    }

    /// <summary>
    /// Generates an instance in Unity from the busprefab with the values given from the UpdateItem() method.
    /// </summary>
    void OnReceiveNewModelBus(ItemModel model)
    {
        views.Clear();

        var instance = Instantiate(busprefab.gameObject) as GameObject;
        instance.transform.SetParent(content, false);
        overview.SetActive(true);
        addBusObject.SetActive(false);
        var view = InitializeItemView(instance, model);
        views.Add(view);
    }

    /// <summary>
    /// Generates an instance in Unity from the airplaneprefab with the values given from the UpdateItem() method.
    /// </summary>
    void OnReceiveNewModelAirplane(ItemModel model)
    {
        views.Clear();

        var instance = Instantiate(planeprefab.gameObject) as GameObject;
        instance.transform.SetParent(content, false);

        overview.SetActive(true);
        addPlaneObject.SetActive(false);

        var view = InitializeItemView(instance, model);
        views.Add(view);
    }

    /// <summary>
    /// Initializes the ItemView and sets the values
    /// </summary>
    ItemView InitializeItemView(GameObject viewGameObject, ItemModel itemModel)
    {
        ItemView view = new ItemView(viewGameObject.transform);



        view.name.text = itemModel.name;
        view.date.text = itemModel.date;
        view.time.text = itemModel.time;

        return view;
    }

    /// <summary>
    /// class that holdes the values from the prefab and the itemmodel
    /// </summary>
    public class ItemView
    {

        public TextMeshProUGUI name;
        public TextMeshProUGUI date;
        public TextMeshProUGUI time;
        public ItemModel itemModel
        {
            set
            {
                if (value != null)
                {
                    name.text = value.name;
                }
            }
        }

        public ItemView(Transform rootView)
        {
            name = rootView.Find("AimText").GetComponent<TextMeshProUGUI>();
            date = rootView.Find("DateText").GetComponent<TextMeshProUGUI>();
            time = rootView.Find("TimeText").GetComponent<TextMeshProUGUI>();


        }
    }

    /// <summary>
    /// class for the itemmodel that is adjusted to the prefab given
    /// </summary>
    public class ItemModel
    {
        public string name;
        public string date;
        public string time;
    }






}
