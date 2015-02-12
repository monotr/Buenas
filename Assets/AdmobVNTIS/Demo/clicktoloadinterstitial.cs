using UnityEngine;
using System.Collections;

public class clicktoloadinterstitial : MonoBehaviour {
	AdmobVNTIS_Interstitial x;
	// Use this for initialization
	void Start () {
		x = (AdmobVNTIS_Interstitial)GameObject.Find ("AdmobVNTISInterstitialObject").GetComponent ("AdmobVNTIS_Interstitial");
	}
	
	// Update is called once per frame
	void OnMouseDown(){
		x.showInterstitialImmediately ();
		//x.showInterstitial ();
	}
}
