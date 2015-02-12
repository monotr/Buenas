using UnityEngine;
using System.Collections;

public class DLCScript : MonoBehaviour {
	public int packNum = 0;
	
	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
