using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;


/// <summary>
/// class to open the next screen
/// </summary>
public class OpenOverview : MonoBehaviour, IPointerClickHandler{

    /// <summary>
    /// method for opening the next scene after clicking to a journey
    /// </summary>
    public void Open () {
        TextMeshProUGUI text = GetComponentInChildren<TextMeshProUGUI>();
        PlayerPrefs.SetString("journeyclicked", text.text);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// method works as a listener for Click-Events
    /// </summary>
    public void OnPointerClick(PointerEventData eventData)
    {
        TextMeshProUGUI text = GetComponentInChildren<TextMeshProUGUI>();
        PlayerPrefs.SetString("journeyclicked", text.text);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
