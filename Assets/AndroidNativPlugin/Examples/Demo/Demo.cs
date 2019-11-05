using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using waqashaxhmi.AndroidNativePlugin;
using System.IO;
using System;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

/// <summary>
/// Class to show a dialog, confirmation button, progress bar, toasts, open gallery
/// </summary>
public class Demo : MonoBehaviour {

    private List<Journey> journeys = new List<Journey>();
	public RawImage image;
	// Use this for initialization
	void Start () {

		AndroidNativeController.OnFileSelectSuccessEvent = OnSuccess;
		AndroidNativeController.OnFileSelectFailureEvent = OnFailure;
		AndroidNativeController.OnPositiveButtonPressEvent = (message) => {
            journeys = JsonConvert.DeserializeObject<List<Journey>>(File.ReadAllText(Path.Combine(Application.persistentDataPath, "journey.json")));
            name = PlayerPrefs.GetString("journeyclicked");
            journeys.RemoveAll(x => x.Name.Equals(name));
            SaveToJSON save = new SaveToJSON();
            save.SaveJourney(journeys);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        };
		AndroidNativeController.OnNegativeButtonPressEvent = (message) => {
			// Code whatever you want on click "NO" Button.
		};

	}

	public void OnShowDialougeBoxButtonClick(){

		AndroidNativePluginLibrary.Instance.ShowMessage("Level 1","You have complete Level 1");
	}
	public void OnShowConfirmationButtonClick(){
		// On Yes Button Click OnPositiveButtonPressEvent fire, and On "NO" button click OnNegativeButtonPressEventFire.
		AndroidNativePluginLibrary.Instance.ShowConfirmationDialouge ("Delete", "Do You Want to Delete the Journey.","YES","NO");
	}
	public void OnProgressBarButtonClick(){
		AndroidNativePluginLibrary.Instance.ShowProgressBar ("Loading Data", "Loading . . .",true);
	}
	public void OnShowToastButtonClick(){
		AndroidNativePluginLibrary.Instance.ShowToast ("Click On Toast Button");
	}
	public void OnOpenGallaryButtonClick(){
		// after selecting file success OnSelectFile event fire
		AndroidNativePluginLibrary.Instance.OpenGallary ();
	}

	private void OnSuccess(string path){
		AndroidNativePluginLibrary.Instance.ShowToast ("File Selected:"+ path);
        PlayerPrefs.SetString("path", path);
        StartCoroutine (ReadImage (path));

	}
	private void OnFailure(string err){
		AndroidNativePluginLibrary.Instance.ShowToast (err);
	}
	IEnumerator ReadImage(string path){
		WWW www = new WWW("file://"+path);

		yield return www;
		image.texture = www.texture;

	}

	IEnumerator DismissProgressBar(){
		yield return new WaitForSeconds (5f);
		AndroidNativePluginLibrary.Instance.DismissProgressBar ();
	}

   
}
