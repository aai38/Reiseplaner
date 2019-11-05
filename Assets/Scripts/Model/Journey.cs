using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// class in the model for holding a journey
/// </summary>
public class Journey
{
    public string Name { get; set; }
    public string Date { get; set; }
    public string PicturePath { get; set; }
    public PackingList PackingList { get; set; }
    public Budget Budget { get; set; }
    public List<Airplane> Airplane { get; set; }
    public List<Bus> Bus { get; set; }
    public List<Train> Train { get; set; }
    public Journey()
    {

    }
    public Journey(string name, string date, string picturePath, PackingList packingList, Budget budget, List<Airplane> airplane, List<Train> train, List<Bus> bus)
    {
        this.Name = name;
        this.Date = date;
        this.PicturePath = picturePath;
        this.PackingList = packingList;
        this.Budget = budget;
        this.Airplane = airplane;
        this.Bus = bus;
        this.Train = train;
    }

    /// <summary>
    /// setter for name
    /// </summary>
    public void SetName(string name)
    {
        this.Name = name;
    }

    /// <summary>
    /// getter for name
    /// </summary>
    public string GetName()
    {
        return Name;
    }

    /// <summary>
    /// getter for date
    /// </summary>
    public string GetDate()
    {
        return Name;
    }

    /// <summary>
    /// getter for picturepath
    /// </summary>
    public string GetPicturePath()
    {
        return Name;
    }

    /// <summary>
    /// getter for packinglist
    /// </summary>
    public PackingList GetPackingList() {
        return PackingList;
    }

    /// <summary>
    /// getter for budget
    /// </summary>
    public Budget GetBudget() {
        return Budget;
    }

    /// <summary>
    /// getter for airplanes
    /// </summary>
    public List<Airplane> GetAirplanes()
    {
        return Airplane;
    }

    /// <summary>
    /// getter for buses
    /// </summary>
    public List<Bus> getBuses() {
        return Bus;
    }

    /// <summary>
    /// getter for trains
    /// </summary>
    public List<Train> GetTrains() {
        return Train;
    }

  


}