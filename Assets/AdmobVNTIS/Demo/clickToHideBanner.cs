using UnityEngine;
using System.Collections;

public class clickToHideBanner : MonoBehaviour {
	AdmobVNTIS x;
	// Use this for initialization
	void Start(){
		x = (AdmobVNTIS)GameObject.Find ("AdmobVNTISObject").GetComponent ("AdmobVNTIS");
	}
	void OnMouseDown(){
		x.hideBanner ();
	}
}
