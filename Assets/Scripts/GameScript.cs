using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class GameScript : MonoBehaviour
{
    public Font fuente;
    public GameObject panelMenu;
    public Sprite[] cartasOriginales;
	public Sprite[] cartas;
	public GameObject carta;
    private Image cambioCarta;
    private List<int> numeros;
    public List<int> numerosSalidos;
    private bool yasta = false;
    public GameObject panelSalidos;
    public RectTransform cartaSalida;
    public Text leyenda;
    public GameObject scroller;
    bool colocar;
	public AdmobVNTIS_Interstitial AdmobVNTIS_Interstitial;
	public AdmobVNTIS AdmobVNTIS;
	public GameObject barajaPack;
	public GameObject indestructible;
	public GoogleTextToSpeech textTovoice;
	public bool autoplay = false;
	public float timeAutoplay;
	private float timerAux;
	public Sprite[] autoplayBut;
	public GameObject playBut;
	public GameObject sliderTime;
	private Slider timerSlider;
	public Text speedTXT;
    float touchBegan = 0;
    float touchEnd = 0;

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
		AdmobVNTIS.showBanner ();

        timerAux = 0;
		timerSlider = sliderTime.GetComponent<Slider> ();
		timeAutoplay = timerSlider.value;
        speedTXT.text = timerSlider.value.ToString() + " segundos";
		textTovoice = GameObject.Find ("Main Camera").GetComponent<GoogleTextToSpeech> ();

        cartas = cartasOriginales;

        numeros = new List<int>();
        numerosSalidos = new List<int>();
        cambioCarta = carta.GetComponent<Image>();

        for (int i = 0; i < cartas.Length; i++)
        {
            numeros.Add(i);
        }
        panelSalidos.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        panelSalidos.GetComponent<RectTransform>().sizeDelta = new Vector3(0, 0, 0);
        colocar = false;
    }

    void Update()
    {
        if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began && Input.GetTouch(0).tapCount == 1)
            {
                touchBegan = Input.GetTouch(0).position.x;
            }
            if (Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(0).tapCount == 1)
            {
                touchEnd = Input.GetTouch(0).position.x;
                if (touchBegan - touchEnd > 150)
                    panelMenu.SetActive(true);
            }
        }

        if (!colocar)
        {
            try
            {
                foreach (GameObject item in GameObject.FindGameObjectsWithTag("Cartas"))
                    Destroy(item);
            }
            catch { }
            panelSalidos.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 161.2f * numerosSalidos.Count);
            panelSalidos.GetComponent<RectTransform>().localPosition = new Vector2(0, 80.6f - 80.6f * numerosSalidos.Count);
            int iterator = 0;
            numerosSalidos.Reverse();
            foreach (int item in numerosSalidos)
            {
                GameObject salio = Instantiate(cartaSalida, Vector3.zero, Quaternion.identity) as GameObject;
                salio.GetComponent<Image>().sprite = cartas[item];
                salio.transform.SetParent(panelSalidos.transform);
                salio.transform.localScale = new Vector3(2, 2, 1);
                salio.GetComponent<Image>().transform.localPosition = new Vector3(0, panelSalidos.GetComponent<RectTransform>().sizeDelta.y / 2 - 161.2f * iterator - 80.6f, 0);
                salio.name = item.ToString();
                salio.GetComponent<Button>().onClick.AddListener(() => { selection(int.Parse(salio.name)); });
                iterator++;
            }
            numerosSalidos.Reverse();
            colocar = true;
        }

		if(autoplay)
        {
			if(timerAux > timeAutoplay)
            {
				timerAux = 0;
				shuffle();
			}
			else
				timerAux += Time.deltaTime;
		}
    }

    public void selection(int change)
    {
        cambioCarta.sprite = cartas[change];
        leyenda.text = leyendas[change];
    }

    public void search()
    {
        colocar = false;
    }

    public void shuffle()
    {
        leyenda.font = fuente;
        colocar = false;
        carta.GetComponentInChildren<Text>().enabled = false;
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

					if(autoplay)
                    {
						textTovoice.words = cartas[rando].name;
                        try { StartCoroutine(textTovoice.PlayTexttoVoice()); }
                        catch { }
					}

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
		try{
		AdmobVNTIS_Interstitial.showInterstitial ();
		}
		catch{}
		Application.LoadLevel(Application.loadedLevel);
        panelMenu.SetActive(false);
    }

    public void quit()
    {
        Application.Quit();
    }

	public void autoPlay()
    {
		autoplay = !autoplay;

		if(autoplay)
        {
			playBut.GetComponent<Image>().sprite = autoplayBut[1];
            if (numerosSalidos.Count == 0)
            {
                textTovoice.words = "corre y se va corriendo con";
                try { StartCoroutine(textTovoice.PlayTexttoVoice()); }
                catch { }
                timerAux = 0;
            }
            else if (numerosSalidos.Count == 54)
            {
                playBut.GetComponent<Image>().sprite = autoplayBut[0];
                textTovoice.words = "no hay más cartas";
                try { StartCoroutine(textTovoice.PlayTexttoVoice()); }
                catch { }
                autoplay = false;
            }
            else
                timerAux = timerSlider.value;
		}
		else{
			playBut.GetComponent<Image>().sprite = autoplayBut[0];
			textTovoice.words = "partida pausada";
            try { StartCoroutine(textTovoice.PlayTexttoVoice()); }
            catch { }
			timerAux = 0;
		}
	}

	public void changeAutoplayTime(){
		timeAutoplay = timerSlider.value;
		speedTXT.text = timerSlider.value.ToString() + " segundos";
	}

    public void closeMenu()
    {
        panelMenu.SetActive(false);
    }

    public void shufflePause()
    {
        autoplay = false;
        playBut.GetComponent<Image>().sprite = autoplayBut[0];
        timerAux = 0;
    }
}