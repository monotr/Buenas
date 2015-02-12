using UnityEngine;
using System.Collections;

public class clickToShowBanner : MonoBehaviour {
	AdmobVNTIS x;
	// Use this for initialization
	void Start(){
		x = (AdmobVNTIS)GameObject.Find ("AdmobVNTISObject").GetComponent ("AdmobVNTIS");
	}
	void OnMouseDown(){
		x.showBanner ();
		guiText.text = 0.3 * Screen.width + " " + 0.3 * Screen.height;
	}
}
