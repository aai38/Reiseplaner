using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// class in model that holdes a plane
/// </summary>
public class Airplane {
    public string Gate { get; set; }
    public string Airline { get; set; }
    public string Flightnumber { get; set; }
    public string Terminal { get; set; }
    public string Date { get; set; }
    public string Seat { get; set; }
    public string TimeDeparture { get; set; }
    public string TimeArrival { get; set; }
    public string Departure { get; set; }
    public string Destination { get; set; }
    private List <Airplane> AirplaneList { get; set; }


    public Airplane(string gate, string airline, string flightnumber, string terminal, string date, string seat, string timeDeparture, string timeArrival, string departure, string destination) {
        this.Gate = gate;
        this.Airline = airline;
        this.Flightnumber = flightnumber;
        this.Date = date;
        this.TimeDeparture = timeDeparture;
        this.TimeArrival = timeArrival;
        this.Departure = departure;
        this.Destination = destination;

    }

    /// <summary>
    /// method for adding a plane to the list
    /// </summary>
    public void AddAirplane(Airplane airplane) {
        AirplaneList.Add(airplane);
    }


}
