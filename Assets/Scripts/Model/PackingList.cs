using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// class in the model that holds the packinglist
/// </summary>
public class PackingList {

    //each item has a name and a bool if its already checked
    public SortedDictionary<string, bool> item { get; set; }

    public PackingList(SortedDictionary<string, bool> item) {
        this.item = item;
    }
	
}
