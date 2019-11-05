using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// class in the model that holds a bus
/// </summary>
public class Bus {

    public string Place { get; set; }
    public string Date { get; set; }
    public string Line { get; set; }
    public string TimeDeparture { get; set; }
    public string TimeArrival { get; set; }
    public string Departure { get; set; }
    public string Destination { get; set; }
    private List<Bus> BusList { get; set; }

    /// <summary>
    /// constructor
    /// </summary>
    public Bus(string place, string date, string line, string timeDeparture, string timeArrival, string departure, string destination)
    {
       
        this.Place = place;
        this.Date = date;
        this.Line = line;
        this.TimeDeparture = timeDeparture;
        this.TimeArrival = timeArrival;
        this.Departure = departure;
        this.Destination = destination;

    }

    /// <summary>
    /// method that adds a bus to the list
    /// </summary>
    public void AddBus(Bus bus)
    {
        BusList.Add(bus);
    }
}
