using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// class in the model that holds a train
/// </summary>
public class Train {

    public string Track { get; set; }
    public string Wagon { get; set; }
    public string Place { get; set; }
    public int TrainClass { get; set; }
    public string Date { get; set; }
    public string TimeDeparture { get; set; }
    public string TimeArrival { get; set; }
    public string Departure { get; set; }
    public string Destination { get; set; }
    private List<Train> TrainList { get; set; }
	
    public Train(string track, string wagon, string place, int trainClass, string date, string timeDeparture, string timeArrival, string departure, string destination)
    {
        this.Track = track;
        this.Wagon = wagon;
        this.Place = place;
        this.TrainClass = trainClass;
        this.Date = date;
        this.TimeDeparture = timeDeparture;
        this.TimeArrival = timeArrival;
        this.Departure = departure;
        this.Destination = destination;

    }

    /// <summary>
    /// method that adds a train to the list
    /// </summary>
    public void AddTrain(Train train)
    {
        TrainList.Add(train);
    }

}
