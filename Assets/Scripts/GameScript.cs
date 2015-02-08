using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class GameScript : MonoBehaviour
{
    public GameObject panelMenu;
    public Sprite[] cartas;
    public GameObject carta;
    private Image cambioCarta;
    private List<int> numeros;
    public List<int> numerosSalidos;
    private bool yasta = false;
    public GameObject panelSalidos;
    public RectTransform cartaSalida;
    public Text buscadorCarta;
    public Text leyenda;
    public GameObject scroller;
    bool colocar;
    string[] leyendas = { "El que la cantó a San Pedro", "Pórtate bien cuatito, si no te lleva el coloradito", "Puliendo el paso, por toda la calle real",
                        "Don Ferruco en la alameda, su bastón quería tirar", "Para el sol y para el agua", "Medio cuerpo de señora se divisa en altamar",
                        "Súbeme paso a pasito, no quieras pegar brinquitos", "La herramienta del borracho", "Tanto bebió el albañil, que quedó como barril",
                        "El que a buen árbol se arrima buena sombra le cobija", "Me lo das o me lo quitas", "Valiente con las mujeres trayendo tan buen puñal",
                        "El gorrito que me ponen", "La muerte siriqui siaca", "El que espera desespera", "Verde blanco y colorado, la bandera del soldado", 
                        "Tocando su bandolón, está el mariachi Simón", "Creciendo se fue hasta el cielo y como no fue violín, tuvo que ser violoncello",
                        "Al otro lado del río tengo mi banco de arena, donde se sienta mi chata, pico de garza morena", "Tú me traes a puros brincos, como pájaro en la rama",
                        "La mano de un criminal", "Una bota igual que l'otra", "El farol de los enamorados", "Cotorro cotorro saca la pata y empiézame a platicar",
                        "¡Ah qué borracho tan necio, ya no lo puedo aguantar!", "El que se comió el azúcar", "No me extrañes corazón, que regreso en el camión",
                        "La barriga que Juan tenía, era empacho de sandía", "No te arrugues cuero viejo, que te quiero pa'tambor", "Camarón que se duerme, se lo lleva la corriente",
                        "Las jaras del indio Adán, donde pegan, dan", "El músico trompa de hule, ya no me quiere tocar", "Atarántamela a palos, no me la dejes llegar",
                        "Uno, dos y tres, el soldado p'al cuartel", "La guía de los marineros", "El caso que te hago es poco", "Este mundo es una bola, y nosotros un balón",
                        "¡Ah Chihuahua! Cuánto apache con pantalón y huarache", "Al nopal lo van a ver, nomás cuando tiene tunas", "El que con la cola pica, le dan una paliza",
                        "Rosita, Rosaura, ven que te quiero ahora", "Al pasar por el panteón, me encontré un calaverón", "Tú con la campana y yo con tu hermana",
                        "Tanto va el cántaro al agua, que se quiebra y te moja las enaguas", "Saltando va buscando, pero no ve nada", "Solo solo te quedaste, de cobija de los pobres",
                        "El sombrero de los reyes", "Rema rema va Lupita, sentada en su chalupita", "Fresco y oloroso, en todo tiempo hermoso", "El que por la boca muere, aunque mudo fuere",
                        "Palmero sube a la palma y bájame un coco real", "El que nace pa'maceta, no sale del corredor", "Arpa vieja de mi suegra, ya no sirves pa'tocar",
                        "Al ver a la verde rana, qué brinco pegó tu hermana"};

    void Start()
    {
        numeros = new List<int>();
        numerosSalidos = new List<int>();
        cambioCarta = carta.GetComponent<Image>();

        for (int i = 0; i < cartas.Length; i++)
        {
            numeros.Add(i);
        }
        panelSalidos.GetComponent<RectTransform>().pivot = Vector2.zero;
        panelSalidos.GetComponent<RectTransform>().position = new Vector3(6, 20, 0);
        panelSalidos.GetComponent<RectTransform>().sizeDelta = new Vector3(0, 0, 0);
        colocar = false;
    }

    void Update()
    {
        try
        {
            foreach (GameObject item in GameObject.FindGameObjectsWithTag("Cartas"))
                Destroy(item);
        }
        catch { }

        if (buscadorCarta.text == "" || buscadorCarta.text == "Buscar carta...")
        {
            if (numerosSalidos.Count > 7)
                scroller.GetComponent<ScrollRect>().enabled = true;
            else
                scroller.GetComponent<ScrollRect>().enabled = false;
            int iterator = 0;
            panelSalidos.GetComponent<RectTransform>().sizeDelta = new Vector2(108, 80.6f * numerosSalidos.Count * 2);
            if (colocar)
            {
                panelSalidos.GetComponent<RectTransform>().localPosition = new Vector3(panelSalidos.GetComponent<RectTransform>().localPosition.x,
                                                                                       -1800 + (numerosSalidos.Count - 15) * -161.2f, 0);
                colocar = false;
            }
            foreach (int item in numerosSalidos)
            {
                GameObject salio = Instantiate(cartaSalida, Vector3.zero, Quaternion.identity) as GameObject;
                salio.GetComponent<Image>().sprite = cartas[item];
                salio.transform.SetParent(panelSalidos.transform);
                salio.transform.localScale = new Vector3(2, 2, 1);
                salio.GetComponent<Image>().transform.localPosition = new Vector3(54, 80.6f + 80.6f * iterator * 2, 0);
                salio.name = item.ToString();
                salio.tag = "Cartas";
                iterator++;
            }
        }

        else
        {
            int temp = 0;
            foreach (int item1 in numerosSalidos)
                if ((cartas[item1].name.Contains(buscadorCarta.text.ToLower())))
                    temp++;
            if (temp > 7)
                scroller.GetComponent<ScrollRect>().enabled = true;
            else
                scroller.GetComponent<ScrollRect>().enabled = false;
            panelSalidos.GetComponent<RectTransform>().sizeDelta = new Vector2(108, 80.6f * temp * 2);
            if (colocar)
            {
                panelSalidos.GetComponent<RectTransform>().localPosition = new Vector3(panelSalidos.GetComponent<RectTransform>().localPosition.x,
                                                                                       -1800 + (temp - 15) * -161.2f, 0);
                colocar = false;
            }
            temp = 0;
            foreach (int item in numerosSalidos)
                if ((cartas[item].name.Contains(buscadorCarta.text.ToLower())))
                {
                    GameObject salio = Instantiate(cartaSalida, Vector3.zero, Quaternion.identity) as GameObject;
                    salio.GetComponent<Image>().sprite = cartas[item];
                    salio.transform.SetParent(panelSalidos.transform);
                    salio.transform.localScale = new Vector3(2, 2, 1);
                    salio.GetComponent<Image>().transform.localPosition = new Vector3(54, 80.6f + 80.6f * temp * 2, 0);
                    salio.name = item.ToString();
                    salio.tag = "Cartas";
                    temp++;
                }
        }

    }

    public void text()
    {
        colocar = true;
    }

    public void shuffle()
    {
        buscadorCarta.GetComponent<InputField>().text = "Buscar carta...";
        carta.GetComponentInChildren<Text>().enabled = false;
        GameObject.Find("PlayButton").GetComponent<Image>().enabled = false;
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
                    if (rando == 9)
                    {
                        switch (Random.Range(0, 2))
                        { 
                            case 0:
                                leyenda.text = leyendas[rando];
                                break;
                            case 1:
                                leyenda.text = "El árbol grueso de El Tule";
                                break;
                        }
                    }
                    else if (rando == 10)
                    {
                        switch (Random.Range(0, 2))
                        {
                            case 0:
                                leyenda.text = leyendas[rando];
                                break;
                            case 1:
                                leyenda.text = "El melón de tierra fría";
                                break;
                        }
                    }
                    else if (rando == 11)
                    {
                        switch (Random.Range(0, 3))
                        {
                            case 0:
                                leyenda.text = leyendas[rando];
                                break;
                            case 1:
                                leyenda.text = "Por qué le corres cobarde, trayendo tan buen puñal";
                                break;
                            case 2:
                                leyenda.text = "El valiente y su tranchete";
                                break;
                        }
                    }
                    else if (rando == 12)
                    {
                        switch (Random.Range(0, 2))
                        {
                            case 0:
                                leyenda.text = leyendas[rando];
                                break;
                            case 1:
                                leyenda.text = "Ponle su gorrito al nene, no se nos vaya a resfriar";
                                break;
                        }
                    }
                    else if (rando == 13)
                    {
                        switch (Random.Range(0, 2))
                        {
                            case 0:
                                leyenda.text = leyendas[rando];
                                break;
                            case 1:
                                leyenda.text = "La muerte tilica y flaca";
                                break;
                        }
                    }
                    else if (rando == 19)
                    {
                        switch (Random.Range(0, 2))
                        {
                            case 0:
                                leyenda.text = leyendas[rando];
                                break;
                            case 1:
                                leyenda.text = "El pájaro chirlo mirlo";
                                break;
                        }
                    }
                    else if (rando == 20)
                    {
                        switch (Random.Range(0, 2))
                        {
                            case 0:
                                leyenda.text = leyendas[rando];
                                break;
                            case 1:
                                leyenda.text = "La mano de un escribano";
                                break;
                        }
                    }
                    else if (rando == 21)
                    {
                        switch (Random.Range(0, 2))
                        {
                            case 0:
                                leyenda.text = leyendas[rando];
                                break;
                            case 1:
                                leyenda.text = "Bótala si no te sirve";
                                break;
                        }
                    }
                    else if (rando == 22)
                    {
                        switch (Random.Range(0, 2))
                        {
                            case 0:
                                leyenda.text = leyendas[rando];
                                break;
                            case 1:
                                leyenda.text = "Ya viene la linda luna rodeada de mil estrellas pa'lumbrar a mi morena cuando salga a su ventana";
                                break;
                        }
                    }
                    else if (rando == 26)
                    {
                        switch (Random.Range(0, 2))
                        {
                            case 0:
                                leyenda.text = leyendas[rando];
                                break;
                            case 1:
                                leyenda.text = "El corazón de una ingrata";
                                break;
                        }
                    }
                    else if (rando == 27)
                    {
                        switch (Random.Range(0, 2))
                        {
                            case 0:
                                leyenda.text = leyendas[rando];
                                break;
                            case 1:
                                leyenda.text = "La sandía y su rebanada";
                                break;
                        }
                    }
                    else if (rando == 28)
                    {
                        switch (Random.Range(0, 2))
                        {
                            case 0:
                                leyenda.text = leyendas[rando];
                                break;
                            case 1:
                                leyenda.text = "Tambor o caja de guerra";
                                break;
                        }
                    }
                    else if (rando == 34)
                    {
                        switch (Random.Range(0, 2))
                        {
                            case 0:
                                leyenda.text = leyendas[rando];
                                break;
                            case 1:
                                leyenda.text = "La estrella polar del norte";
                                break;
                        }
                    }
                    else if (rando == 36)
                    {
                        switch (Random.Range(0, 2))
                        {
                            case 0:
                                leyenda.text = leyendas[rando];
                                break;
                            case 1:
                                leyenda.text = "Cristóbal cargando el mundo";
                                break;
                        }
                    }
                    else if (rando == 40)
                    {
                        switch (Random.Range(0, 2))
                        {
                            case 0:
                                leyenda.text = leyendas[rando];
                                break;
                            case 1:
                                leyenda.text = "Rosa Rosita Rosaura tu palabra es más firme que la d'un notario";
                                break;
                        }
                    }
                    else if (rando == 43)
                    {
                        switch (Random.Range(0, 2))
                        {
                            case 0:
                                leyenda.text = leyendas[rando];
                                break;
                            case 1:
                                leyenda.text = "El cantarito del pulque no se te vaya a quebrar pos lo quiere la patrona pa poderme enamorar";
                                break;
                        }
                    }
                    else if (rando == 44)
                    {
                        switch (Random.Range(0, 2))
                        {
                            case 0:
                                leyenda.text = leyendas[rando];
                                break;
                            case 1:
                                leyenda.text = "El que brinca los peñascos";
                                break;
                        }
                    }
                    else if (rando == 45)
                    {
                        switch (Random.Range(0, 2))
                        {
                            case 0:
                                leyenda.text = leyendas[rando];
                                break;
                            case 1:
                                leyenda.text = "La cobija de los pobres";
                                break;
                        }
                    }
                    else if (rando == 46)
                    {
                        switch (Random.Range(0, 2))
                        {
                            case 0:
                                leyenda.text = leyendas[rando];
                                break;
                            case 1:
                                leyenda.text = "La corona del imperio";
                                break;
                        }
                    }
                    else if (rando == 47)
                    {
                        switch (Random.Range(0, 2))
                        {
                            case 0:
                                leyenda.text = leyendas[rando];
                                break;
                            case 1:
                                leyenda.text = "La chalupa rema y rema se va para Xochimilco";
                                break;
                        }
                    }
                    else if (rando == 48)
                    {
                        switch (Random.Range(0, 2))
                        {
                            case 0:
                                leyenda.text = leyendas[rando];
                                break;
                            case 1:
                                leyenda.text = "El pino de la Alameda siempre verde y siempre hermoso";
                                break;
                        }
                    }
                    else if (rando == 48)
                    {
                        switch (Random.Range(0, 2))
                        {
                            case 0:
                                leyenda.text = leyendas[rando];
                                break;
                            case 1:
                                leyenda.text = "El pez por su boca muere";
                                break;
                        }
                    }
                    else if (rando == 50)
                    {
                        switch (Random.Range(0, 2))
                        {
                            case 0:
                                leyenda.text = leyendas[rando];
                                break;
                            case 1:
                                leyenda.text = "La palma real de Colima";
                                break;
                        }
                    }
                    else if (rando == 53)
                    {
                        switch (Random.Range(0, 2))
                        {
                            case 0:
                                leyenda.text = leyendas[rando];
                                break;
                            case 1:
                                leyenda.text = "La rana mujer del sapo";
                                break;
                        }
                    }
                    else
                        leyenda.text = leyendas[rando];
                }
            }
            yasta = false;
            colocar = true;
        }
        else
            leyenda.text = "No hay más cartas";
    }

    public void menu()
    {
        panelMenu.SetActive(true);
    }

    public void restart()
    {
        Application.LoadLevel(Application.loadedLevel);
        panelMenu.SetActive(false);
    }

    public void quit()
    {
        Application.Quit();
    }

    public void closeMenu()
    {
        panelMenu.SetActive(false);
    }
}