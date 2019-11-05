using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Newtonsoft.Json;
using System.IO;
using System;

/// <summary>
/// controlls the behaviour of the detailsscreen
/// </summary>
public class DetailsController : MonoBehaviour {

    public TextMeshProUGUI trainName;
    public TextMeshProUGUI trainDate;
    public TextMeshProUGUI trainDepart;
    public TextMeshProUGUI trainArriv;
    public TextMeshProUGUI trainTrackArriv;
    public TextMeshProUGUI trainTrackDepart;
    public TextMeshProUGUI trainPlace;
    public TextMeshProUGUI trainDuration;
    public TextMeshProUGUI trainWagon;
    public TextMeshProUGUI trainClass;
    public TextMeshProUGUI trainTimeArriv;
    public TextMeshProUGUI trainTimeDepart;


    public TextMeshProUGUI planeName;
    public TextMeshProUGUI planeAirline;
    public TextMeshProUGUI planeDate;
    public TextMeshProUGUI planeDepart;
    public TextMeshProUGUI planeArriv;
    public TextMeshProUGUI planeTerminalArriv;
    public TextMeshProUGUI planeTerminalDepart;
    public TextMeshProUGUI planeGateDepart;
    public TextMeshProUGUI planeGateArriv;
    public TextMeshProUGUI planeSeat;
    public TextMeshProUGUI planeDuration;
    public TextMeshProUGUI planeFlightnumber;
    public TextMeshProUGUI planeTimeArriv;
    public TextMeshProUGUI planeTimeDepart;

    public TextMeshProUGUI busName;
    public TextMeshProUGUI busDate;
    public TextMeshProUGUI busLine;
    public TextMeshProUGUI busDepart;
    public TextMeshProUGUI busArriv;
    public TextMeshProUGUI busPlace;
    public TextMeshProUGUI busDuration;
    public TextMeshProUGUI busTimeArriv;
    public TextMeshProUGUI busTimeDepart;

    private string nameconnectionclicked;
    private List<Journey> journeys;
    private Journey actualJourney;
    private Train actualTrain;
    private Bus actualBus;
    private Airplane actualPlane;



    /// <summary>
    /// looks which connection is clicked
    /// </summary>
    // Use this for initialization
    void Start () {
        nameconnectionclicked = PlayerPrefs.GetString("nameconnectionclicked");
        journeys = JsonConvert.DeserializeObject<List<Journey>>(File.ReadAllText(Path.Combine(Application.persistentDataPath, "journey.json")));
        name = PlayerPrefs.GetString("journeyclicked");
        actualJourney = journeys.Find(x => x.Name.Equals(name));
        Debug.Log("start");

        string connectionclicked = PlayerPrefs.GetString("connectionclicked");
        if(connectionclicked.Equals("train")) {

            actualTrain = actualJourney.Train.Find(x => (x.Departure + " - " + x.Destination).Equals(nameconnectionclicked));
            Debug.Log(nameconnectionclicked);
            Debug.Log(actualTrain);
            ShowTrainInformation();
        } else if(connectionclicked.Equals("bus")) {
            actualBus = actualJourney.Bus.Find(x => (x.Departure + " - " + x.Destination).Equals(nameconnectionclicked));
            Debug.Log(nameconnectionclicked);

            ShowBusInformation();
        } else if (connectionclicked.Equals("plane")){
            actualPlane= actualJourney.Airplane.Find(x => (x.Departure + " - " + x.Destination).Equals(nameconnectionclicked));
            Debug.Log(nameconnectionclicked);

            ShowPlaneInformation();
        }
	}


    /// <summary>
    /// Fills the different texts from the detailsscreen from the trains with the right information
    /// </summary>
    public void ShowTrainInformation() {
        trainName.SetText(nameconnectionclicked);
        trainDate.SetText(actualTrain.Date);
        trainDepart.SetText(actualTrain.Departure);
        trainArriv.SetText(actualTrain.Destination);
        trainTrackArriv.SetText(actualTrain.Track);
        trainTrackDepart.SetText("");
        trainPlace.SetText(actualTrain.Place);
        DateTime ending = DateTime.Parse(actualTrain.TimeArrival);
        DateTime begining = DateTime.Parse(actualTrain.TimeDeparture);

        trainDuration.SetText((ending - begining).Hours.ToString() + "h");
        trainWagon.SetText(actualTrain.Wagon);
        trainClass.SetText(actualTrain.TrainClass.ToString());
        trainTimeArriv.SetText(actualTrain.TimeArrival);
        trainTimeDepart.SetText(actualTrain.TimeDeparture);
    }

    /// <summary>
    /// Fills the different texts from the detailsscreen from the bus with the right information
    /// </summary>
    public void ShowBusInformation() {
        busName.SetText(nameconnectionclicked);
        busDate.SetText(actualBus.Date);
        busLine.SetText("Busline: " + actualBus.Line);
        busArriv.SetText(actualBus.Destination);
        busPlace.SetText(actualBus.Place);
        busDepart.SetText(actualBus.Departure);
        busTimeArriv.SetText(actualBus.TimeArrival);
        busTimeDepart.SetText(actualBus.TimeDeparture);

        DateTime ending = DateTime.Parse(actualBus.TimeArrival);
        DateTime begining = DateTime.Parse(actualBus.TimeDeparture);
        busDuration.SetText((ending - begining).Hours.ToString() + "h");

    }

    /// <summary>
    /// Fills the different texts from the detailsscreen from the plane with the right information
    /// </summary>
    public void ShowPlaneInformation() {
        planeName.SetText(nameconnectionclicked);
        planeDate.SetText(actualPlane.Date);
        planeSeat.SetText(actualPlane.Seat);
        planeArriv.SetText(actualPlane.Destination);
        planeDepart.SetText(actualPlane.Departure);
        planeAirline.SetText("Airline: " + actualPlane.Airline);

        DateTime ending = DateTime.Parse(actualPlane.TimeArrival);
        DateTime begining = DateTime.Parse(actualPlane.TimeDeparture);
        planeDuration.SetText((ending - begining).Hours.ToString() + "h");
        planeGateArriv.SetText(actualPlane.Gate);
        planeGateDepart.SetText("-");
        planeFlightnumber.SetText(actualPlane.Flightnumber);
        planeTerminalArriv.SetText(actualPlane.Terminal);
        planeTerminalDepart.SetText("-");
        planeTimeArriv.SetText(actualPlane.TimeArrival);
        planeTimeDepart.SetText(actualPlane.TimeDeparture);


    }
}
