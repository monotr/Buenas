using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class GameScript : MonoBehaviour {
	public Sprite[] cartas;
	public GameObject carta;
	private SpriteRenderer cambioCarta;
	private List<int> numeros;
	private	 List<int> numerosSalidos;
	private bool yasta = false;
	public GameObject panelSalidos;
	public RectTransform cartaSalida;

	void Start () {
		numeros = new List<int>();
		numerosSalidos = new List<int>();
		cambioCarta = carta.GetComponent<SpriteRenderer> ();

		for(int i=0; i<cartas.Length; i++){
			numeros.Add(i);
		}
	}
	
	void Update () {
		if (Input.GetKeyDown (KeyCode.A)) {
			if(numeros.Count>0){
				while(!yasta){
					int rando = Random.Range(0,cartas.Length);
					if(numeros.Contains(rando)){
						cambioCarta.sprite = cartas[rando];
						numerosSalidos.Add(rando);
						numeros.Remove(rando);
						yasta = true;

						GameObject salio = Instantiate(cartaSalida, Vector3.zero, Quaternion.identity) as GameObject;
						salio.GetComponent<Image>().sprite = cartas[rando];
						salio.transform.SetParent (panelSalidos.transform);
						salio.GetComponent<RectTransform>().anchorMax = new Vector2(-58,0);
						salio.GetComponent<RectTransform>().anchorMin = new Vector2(0,0);
					}
				}
				yasta = false;
			}
			else
				print ("no mas cartas");
		}
	}
}
