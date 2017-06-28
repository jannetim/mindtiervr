using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuestionHandler : MonoBehaviour {
    string[] Questions;
    string currentQuestion;
    Text question;
    int questionNumber;
    Toggle chosenToggle;
    int chosenToggleCounter;
    bool readyToProceed;
    ToggleGroup tg;
    Color ButtonOriginal;
    Color ButtonSelected;
    Toggle[] Toggles;
    bool feelings;
    bool feelingsPair;
    public Image image;
    public GameObject Qt;
    public GameObject Ft;
    public Toggle[] QuestionToggles;
    public Toggle[] FeelingToggles;
    public Sprite[] Images;
    public GameObject Proceed;
    public ParticleSystem ps;


    // Use this for initialization
    void Start () {
        questionNumber = -1;
        Questions = new string[24];
        Questions[0] = "Minusta tuntuu sympaattiselta";
        Questions[1] = "Minusta tuntuu myötätuntoiselta";
        Questions[2] = "Minusta tuntuu lämminsydämiseltä";
        Questions[3] = "Minusta tuntuu lämpimältä";
        Questions[4] = "Minusta tuntuu hellältä";
        Questions[5] = "Minusta tuntuu liikuttuneelta";
        Questions[6] = "Huomasin parini";
        Questions[7] = "Parini huomasi minut";
        Questions[8] = "Parini läsnäolo oli minulle ilmeistä";
        Questions[9] = "Minun läsnäoloni oli ilmeistä parilleni";
        Questions[10] = "Parini herätti huomioni";
        Questions[11] = "Herätin parini huomion";
        Questions[12] = "Pystyin päättelemään miltä paristani tuntui";
        Questions[13] = "Parini pystyi päättelemään miltä minusta tuntui";
        Questions[14] = "Parini emootiot/tunteet eivät olleet minulle selvät";
        Questions[15] = "Minun emootioni eivät olleet parilleni selkeitä";
        Questions[16] = "Pystyin kuvailemaan parini tunteita tarkasti";
        Questions[17] = "Parini pystyi kuvailemaan tunteitani tarkasti";
        Questions[18] = "Parini mielialalla oli minuun vaikutusta";
        Questions[19] = "Minun mielialallani oli pariini vaikutusta";
        Questions[20] = "Parini tunteet vaikuttivat vuorovaikutuksemme tunnelmaan";
        Questions[21] = "Minun tunteeni vaikuttivat vuorovaikutuksemme tunnelmaan";
        Questions[22] = "Parini asenteet vaikuttivat tunteisiini";
        Questions[23] = "Minun asenteeni vaikuttivat parini tunteisiin";
        question = GameObject.Find("Question").GetComponent<Text>();
        Toggles = FeelingToggles;
        tg = GameObject.Find("Question Holder").GetComponent<ToggleGroup>();
        question.text = "Valitse seuraavista hahmoista ne, jotka parhaiten kuvastavat tunnetilaasi.";
        //question.text = Questions[0];
        feelings = true;
        feelingsPair = false;
        chosenToggleCounter = 4;
        chosenToggle = Toggles[chosenToggleCounter];
        readyToProceed = false;
        ps = (ParticleSystem)GameObject.Find("SelectorParticles").GetComponent(typeof(ParticleSystem));
        ps.transform.position = new Vector3(chosenToggle.transform.position.x, chosenToggle.transform.position.y - 3, chosenToggle.transform.position.z);
        ButtonOriginal = Proceed.GetComponent<Button>().colors.normalColor;
        ButtonSelected = new Color(0.5f, 0.5f, 1, 1);
        Proceed.SetActive(false);
    }
	

    public void NextQuestion()
    {
        readyToProceed = false;
        ButtonColorToOriginal();
        Proceed.SetActive(false);
        questionNumber++;
        foreach (Toggle toggle in Toggles)
        {
            if (toggle.isOn)
            {
                // disabling radio button logic until toggles cleared
                tg.allowSwitchOff = true;
                toggle.isOn = false;
                tg.allowSwitchOff = false;
            }
        }
        if (!feelings) { 
            if (questionNumber >= 24)
            {
                // proceed to somewhere
                SceneManager.LoadScene(0);

            }
            question.text = Questions[questionNumber];
        } else
        {
            if (questionNumber > 2)
            {
                if (feelingsPair)
                {
                    feelings = false;
                    Toggles = QuestionToggles;
                    Ft.SetActive(false);
                    Qt.SetActive(true);
                    question.text = Questions[0];
                    image.transform.gameObject.SetActive(false);

                } else
                {
                    Debug.Log(image.transform.gameObject.activeInHierarchy);
                    image.transform.gameObject.SetActive(false);
                    feelingsPair = true;
                    question.text = "Valitse seuraavista hahmoista ne, joiden arvelet parhaiten kuvaavan parisi tunnetilaa.";
                }
                questionNumber = -1;
            } else { 
                if (!image.transform.gameObject.activeInHierarchy)
                {
                    image.transform.gameObject.SetActive(true);
                }
                image.sprite = Images[questionNumber];
            }
        }
        ps.transform.position = new Vector3(chosenToggle.transform.position.x, chosenToggle.transform.position.y - 4, chosenToggle.transform.position.z);
    }

	// Update is called once per frame
	void Update () {
        if (!Proceed.activeInHierarchy) { 
		    foreach (Toggle toggle in Toggles)
            {
                if (toggle.isOn)
                {
                    Proceed.SetActive(true);
                }
            }
        }

        if (OVRInput.GetDown(OVRInput.Button.One) || OVRInput.GetDown(OVRInput.Button.Two) || OVRInput.GetDown(OVRInput.Button.Three) || OVRInput.GetDown(OVRInput.Button.Four) 
        || OVRInput.GetDown(OVRInput.Button.PrimaryThumbstick) || OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger)
            || OVRInput.GetDown(OVRInput.Button.SecondaryThumbstick) || OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger) || Input.GetKeyDown("r"))
        {
            if (readyToProceed)
            {
                NextQuestion();
            } else
            {
                chosenToggle.isOn = true;
            }
        }


        if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickLeft) || OVRInput.GetDown(OVRInput.Button.SecondaryThumbstickLeft) || Input.GetKeyDown("left"))
        {
            if (readyToProceed)
            {
                readyToProceed = false;
                ButtonColorToOriginal();
            }
            chosenToggleCounter--;
            if (chosenToggleCounter < 0 )
            {
                chosenToggleCounter = Toggles.Length -1;
            }
            Debug.Log(chosenToggleCounter);
            chosenToggle = Toggles[chosenToggleCounter];
            ps.transform.position = new Vector3(chosenToggle.transform.position.x, chosenToggle.transform.position.y - 3, chosenToggle.transform.position.z);

        }
        if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickRight) || OVRInput.GetDown(OVRInput.Button.SecondaryThumbstickRight) || Input.GetKeyDown("right"))
        {
            if (readyToProceed)
            {
                readyToProceed = false;
                ButtonColorToOriginal();
            }
            chosenToggleCounter++;
            if (chosenToggleCounter >= Toggles.Length)
            {
                chosenToggleCounter = 0;
            }
            Debug.Log(chosenToggleCounter);
            chosenToggle = Toggles[chosenToggleCounter];
            ps.transform.position = new Vector3(chosenToggle.transform.position.x, chosenToggle.transform.position.y - 3, chosenToggle.transform.position.z);
        }



        if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickUp) || OVRInput.GetDown(OVRInput.Button.SecondaryThumbstickUp) || Input.GetKeyDown("up"))
        {
            readyToProceed = false;
            ps.transform.position = new Vector3(chosenToggle.transform.position.x, chosenToggle.transform.position.y - 3, chosenToggle.transform.position.z);
            ButtonColorToOriginal();

        }
        if (Proceed.activeInHierarchy && (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickDown) || OVRInput.GetDown(OVRInput.Button.SecondaryThumbstickDown) || Input.GetKeyDown("down")))
        {
            readyToProceed = true;
            ps.transform.position = new Vector3(Proceed.transform.position.x, Proceed.transform.position.y - 3, Proceed.transform.position.z);
            ColorBlock cb = Proceed.GetComponent<Button>().colors;
            cb.normalColor = ButtonSelected;
            Proceed.GetComponent<Button>().colors = cb;

        }
    }

    void ButtonColorToOriginal()
    {
        ColorBlock cb = Proceed.GetComponent<Button>().colors;
        cb.normalColor = ButtonOriginal;
        Proceed.GetComponent<Button>().colors = cb;
    }

}
