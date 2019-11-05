using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using System.Runtime.Serialization.Formatters;
using Newtonsoft.Json.Linq;

/// <summary>
/// class to save different objects to the .json file
/// </summary>
public class SaveToJSON  {

    //for android use persistentDataPath!!!

    /// <summary>
    /// Save Journey with Date, Picture and Name, Packing List and Budget to File in JSON-Format
    /// </summary>

    public void SaveJourney (List<Journey> journeys) {
        var setting = new JsonSerializerSettings();
        setting.Formatting = Formatting.Indented;

        var json = JsonConvert.SerializeObject(journeys, setting);
        var path = Path.Combine(Application.persistentDataPath, "journey.json");

        File.WriteAllText(path, json);

    }

    /// <summary>
    /// Save a train connection to the journey it belongs
    /// </summary>
    public void SaveTrainConnection (Train train, List<Journey> journeys) {
        var setting = new JsonSerializerSettings();
        setting.Formatting = Formatting.Indented;

        var json = JsonConvert.SerializeObject(journeys, setting);
        var path = Path.Combine(Application.persistentDataPath, "journey.json");

        File.WriteAllText(path, json);
    }

    /// <summary>
    /// Save a bus connection to the journey it belongs
    /// </summary>
    public void SaveBusConnection(Bus bus, List<Journey> journeys) {
        var setting = new JsonSerializerSettings();
        setting.Formatting = Formatting.Indented;

        var json = JsonConvert.SerializeObject(journeys, setting);
        var path = Path.Combine(Application.persistentDataPath, "journey.json");

        File.WriteAllText(path, json);
    }

    /// <summary>
    /// Save a plane connection to the journey it belongs
    /// </summary>
    public void SaveAirplaneConnection(Airplane airplane, List<Journey> journeys) {
        var setting = new JsonSerializerSettings();
        setting.Formatting = Formatting.Indented;

        var json = JsonConvert.SerializeObject(journeys, setting);
        var path = Path.Combine(Application.persistentDataPath, "journey.json");

        File.WriteAllText(path, json);
    }

    /// <summary>
    /// Save a expenditure to the journey it belongs
    /// </summary>
    public void SaveExpenditure(string key, int value, List<Journey> journeys) {
        string nameJourney = PlayerPrefs.GetString("journeyclicked");
        journeys.Find(x => x.Name.Equals(nameJourney)).Budget.Expenditures.Add(key, value);
        var setting = new JsonSerializerSettings();
        setting.Formatting = Formatting.Indented;

        var json = JsonConvert.SerializeObject(journeys, setting);
        var path = Path.Combine(Application.persistentDataPath, "journey.json");

        File.WriteAllText(path, json);
    }
}
