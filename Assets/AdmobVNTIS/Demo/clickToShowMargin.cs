using UnityEngine;
using System.Collections;

public class clickToShowMargin : MonoBehaviour {
	AdmobVNTIS x;
	// Use this for initialization
	void Start(){
		x = (AdmobVNTIS)GameObject.Find ("AdmobVNTISObject").GetComponent ("AdmobVNTIS");
	}
	void OnMouseDown(){
		x.setMargins (0.3f, 0.3f);
	}
}
