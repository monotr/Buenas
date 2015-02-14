using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;


/// <summary>
/// <author>Jefferson Reis</author>
/// <explanation>Works only on Android, or platform that supports mp3 files. To test, change the platform to Android.</explanation>
/// </summary>

public class GoogleTextToSpeech : MonoBehaviour
{
	public string words = "Hello";

	public IEnumerator PlayTexttoVoice ()
	{;
		// Remove the "spaces" in excess
		Regex rgx = new Regex ("\\s+");
		// Replace the "spaces" with "% 20" for the link Can be interpreted
		string result = rgx.Replace (words, "%20");

		/*rgx = new Regex ("á");
		result = rgx.Replace (result, "%C3%A1");
		rgx = new Regex ("é");
		result = rgx.Replace (result, "%C3%A9");
		rgx = new Regex ("í");
		result = rgx.Replace (result, "%C3%AD");
		rgx = new Regex ("ó");
		result = rgx.Replace (result, "%C3%B3");
		rgx = new Regex ("ú");
		result = rgx.Replace (result, "%C3%BA");*/

		rgx = new Regex ("á");
		result = rgx.Replace (result, "aa");
		rgx = new Regex ("é");
		result = rgx.Replace (result, "ee");
		rgx = new Regex ("í");
		result = rgx.Replace (result, "ii");
		rgx = new Regex ("ó");
		result = rgx.Replace (result, "oo");
		rgx = new Regex ("ú");
		result = rgx.Replace (result, "uu");
		rgx = new Regex ("ñ");
		result = rgx.Replace (result, "ni");

		print (result);

		string url = "http://translate.google.com/translate_tts?tl=es&q=" + result;
		WWW www = new WWW (url);
		yield return www;
		audio.clip = www.GetAudioClip (false, false, AudioType.MPEG);
		audio.Play ();
	}
	
	/*void OnGUI ()
	{
		words = GUI.TextField (new Rect (Screen.width / 2 - 200 / 2, 10, 200, 30), words);
		if (GUI.Button (new Rect (Screen.width / 2 - 150 / 2, 40, 150, 50), "Speak")) {
			StartCoroutine (PlayTexttoVoice ());
		}
	}*/
	
	
}//closes the class