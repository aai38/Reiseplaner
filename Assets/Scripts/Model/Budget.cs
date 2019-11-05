using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// class in model that holds the budget
/// </summary>
public class Budget {

    public float Total { get; set; }

    //each expenditure has a name and the suitable amount
    public SortedDictionary<string, int> Expenditures { get; set; }

    public Budget (float total, SortedDictionary<string, int> expenditures) {
        this.Total = total;
        this.Expenditures = expenditures;
    }

}
