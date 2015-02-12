using UnityEngine;
using System.Collections;

public class AdmobVNTIS_Interstitial : MonoBehaviour {

	public string InterstitialAdUnitID = "YOUR_AD_UNIT_ID";
	public string TestDeviceID = "";
	public bool ShowInterstitialOnLoad = false;

	private static AndroidJavaObject jo;

	public float retryTimeOut = 1.0f;

	[Range(0,100)]
	public int extendTimeOutPercent = 0;

	public int timeOutMax = 90;

	float retryTime;
	float current;
	bool isFail = false;
	float totalretryTime = 0.0f;
	// Dont destroy on load and prevent duplicate
	private static bool created = false;
	void Awake() {
		if (!created) {
			DontDestroyOnLoad(this.gameObject);
			created = true;
			initializeInterstitial ();
		} else {
			Destroy(this.gameObject);
		}
		retryTime = retryTimeOut;
		current = 0.0f;
	}

	void initializeInterstitial(){
		jo = new AndroidJavaObject ("admob.admob",InterstitialAdUnitID,TestDeviceID,ShowInterstitialOnLoad);
	}

	/// <summary>
	/// Show the interstitial. Load if ad is not loaded, and show after load.
	/// </summary>
	public void showInterstitial(){
		jo.Call ("showInterstitial");
	}

	/// <summary>
	/// Show the interstitial. Load if ad is not loaded, and NOT show after load.
	/// </summary>
	public void showInterstitialImmediately(){
		jo.Call ("showInterstitialImmediately");
	}

	/// <summary>
	/// Load the interstitial. Ignore if already loaded.
	/// </summary>
	public void prepareInterstitial(){
		jo.Call ("prepareInterstitial");
	}

	public void onAdLoaded(string msg){
		retryTime = retryTimeOut;
		totalretryTime = 0.0f;
		current = 0.0f;
	}

	public void onAdFailedToLoad(string errorCode){
		if (timeOutMax != 0 && timeOutMax < totalretryTime) {
			// do nothing
		} else {
			isFail = true;
			totalretryTime += retryTime;
		}
	}

	void Update(){
		if (isFail) {
			if (current < retryTime) {
				current += Time.deltaTime;
			}else{
				isFail = false;
				current = 0.0f;
				retryTime += (float)(extendTimeOutPercent*retryTime);
				prepareInterstitial();
			}			    
		}
	}
}
