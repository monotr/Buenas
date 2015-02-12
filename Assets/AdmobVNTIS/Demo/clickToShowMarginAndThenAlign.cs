using UnityEngine;
using System.Collections;

public class clickToShowMarginAndThenAlign : MonoBehaviour {
	AdmobVNTIS x;
	// Use this for initialization
	void Start(){
		x = (AdmobVNTIS)GameObject.Find ("AdmobVNTISObject").GetComponent ("AdmobVNTIS");
	}
	void OnMouseDown(){
		x.setMargins (0.1f, 0.0f);
		AdmobVNTIS.Rules[] rules = new AdmobVNTIS.Rules[1];
		rules [0] = AdmobVNTIS.Rules.ALIGN_PARENT_BOTTOM;
		x.setRules (rules, 1);
	}
}
