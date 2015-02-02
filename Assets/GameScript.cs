using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class GameScript : MonoBehaviour
{
    public Sprite[] cartas;
    public GameObject carta;
    private SpriteRenderer cambioCarta;
    private List<int> numeros;
    public List<int> numerosSalidos;
    private bool yasta = false;
    public GameObject panelSalidos;
    public RectTransform cartaSalida;
    public Text buscadorCarta;
    bool colocar;

    void Start()
    {
        numeros = new List<int>();
        numerosSalidos = new List<int>();
        cambioCarta = carta.GetComponent<SpriteRenderer>();

        for (int i = 0; i < cartas.Length; i++)
        {
            numeros.Add(i);
        }
        panelSalidos.GetComponent<RectTransform>().pivot = Vector2.zero;
        panelSalidos.GetComponent<RectTransform>().position = new Vector3(10, 10, 0);
        panelSalidos.GetComponent<RectTransform>().sizeDelta = new Vector3(0, 0, 0);
        colocar = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (numeros.Count > 0)
            {
                while (!yasta)
                {
                    int rando = Random.Range(0, cartas.Length);
                    if (numeros.Contains(rando))
                    {
                        cambioCarta.sprite = cartas[rando];
                        numerosSalidos.Add(rando);
                        numeros.Remove(rando);
                        yasta = true;
                    }
                }
                yasta = false;
                colocar = true;
            }
            else
                print("no mas cartas");
        }

        try
        {
            foreach (GameObject item in GameObject.FindGameObjectsWithTag("Cartas"))
                Destroy(item);
        }
        catch { }

        if (buscadorCarta.text == "" || buscadorCarta.text == "Buscar carta...")
        {
            int iterator = 0;
            panelSalidos.GetComponent<RectTransform>().sizeDelta = new Vector2(54, 80.6f * numerosSalidos.Count);
            if (numerosSalidos.Count > 15 && colocar)
            {
                panelSalidos.GetComponent<RectTransform>().localPosition = new Vector3(panelSalidos.GetComponent<RectTransform>().localPosition.x,
                                                                                       -600 + (numerosSalidos.Count - 15) * -80, 0);
                colocar = false;
            }
            foreach (int item in numerosSalidos)
            {
                GameObject salio = Instantiate(cartaSalida, Vector3.zero, Quaternion.identity) as GameObject;
                salio.GetComponent<Image>().sprite = cartas[item];
                salio.transform.SetParent(panelSalidos.transform);
                salio.transform.localScale = new Vector3(1, 1, 1);
                salio.GetComponent<Image>().transform.localPosition = new Vector3(27, 40.3f + 80.6f * iterator, 0);
                salio.name = item.ToString();
                salio.tag = "Cartas";
                iterator++;
            }
        }

        else
        {
            int temp = 0;
            foreach (int item1 in numerosSalidos)
                if ((cartas[item1].name.Contains(buscadorCarta.text)))
                    temp++;
            panelSalidos.GetComponent<RectTransform>().sizeDelta = new Vector2(54, 80.6f * temp+1);
            temp = 0;
            foreach (int item in numerosSalidos)
                if ((cartas[item].name.Contains(buscadorCarta.text)))
                {
                    GameObject salio = Instantiate(cartaSalida, Vector3.zero, Quaternion.identity) as GameObject;
                    salio.GetComponent<Image>().sprite = cartas[item];
                    salio.transform.SetParent(panelSalidos.transform);
                    salio.transform.localScale = new Vector3(1, 1, 1);
                    salio.GetComponent<Image>().transform.localPosition = new Vector3(27, 40.3f + 80.6f * temp, 0);
                    salio.name = item.ToString();
                    salio.tag = "Cartas";
                    temp++;
                }
        }

    }
}