using UnityEngine;
using System.Collections;

public class clickToShowRules : MonoBehaviour {
	AdmobVNTIS x;
	// Use this for initialization
	void Start(){
		x = (AdmobVNTIS)GameObject.Find ("AdmobVNTISObject").GetComponent ("AdmobVNTIS");
	}
	void OnMouseDown(){
		AdmobVNTIS.Rules[] rules = new AdmobVNTIS.Rules[2];
		rules [0] = AdmobVNTIS.Rules.ALIGN_PARENT_LEFT;
		rules [1] = AdmobVNTIS.Rules.CENTER_VERTICAL;
		x.setRules (rules,2);
	}
}
