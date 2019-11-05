using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// class from an asset that changes settings so that you have acces to the sdcard
/// </summary>
[ExecuteInEditMode]
public class ChangeSettings : MonoBehaviour {


	void Awake()
	{
		#if UNITY_EDITOR
		UnityEditor.PlayerSettings.Android.forceSDCardPermission = true;
		#endif
	}

}
