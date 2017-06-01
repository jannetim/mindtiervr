using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Networking;

public class PlayerManager : NetworkBehaviour
{

    public GameObject AuraExpander;
    public GameObject AuraController;
    public GameObject BridgeBars;
    public GameObject StatueAnimator;
    public bool IsSimNPC = false;
    public float PlayerFA;
    GameObject SessionManager;
    GameObject DataHolder;
    GameObject SpawnPoint1;
    GameObject SpawnPoint2;
    public int PlayerNumber = 0;
    private float breatheNow = 9f;
    private float breathePast = 10f;
    private float respThreshold = 0f;
    private float respMax = -1f;
    private float respMin = 10000f;
    bool inBreathContinues = false;
    bool outBreathContinues = false;
    private GameObject otherPlayerManager;
    public float[] respArray = new float[6];
    public float[] faArray = new float[20];
    //private Queue<float> respQueue = new Queue< float > (10);

    public Queue<float> respMinQueue = new Queue<float>(new float[3]);
    public Queue<float> respMaxQueue = new Queue<float>(new float[3]);
    public Queue<float> respQueue = new Queue<float>(new float[6]);
    public Queue<float> faQueue = new Queue<float>(new float[20]);

    public bool RespChanged = true;
    float RespDataOld = 0f;

    bool firstwaveset = false;
    bool breatheCooldown = false;
    bool breatheQueueCooldown = false;


    // Use this for initialization
    void Start()
    {
        if (!GameObject.Find("Session Manager").GetComponent<SessionManager>().SingleUserSession)
        {
            if (!isLocalPlayer)
            {
                return;
            }
        }


        SessionManager = GameObject.Find("Session Manager");
        DataHolder = GameObject.Find("Data Holder");

        SpawnPoint1 = GameObject.Find("Spawn Point 1");
        SpawnPoint2 = GameObject.Find("Spawn Point 2");

        //find out which player this one is, if it has not been set yet.

        if (PlayerNumber == 0)
        {
            float dist1 = Vector3.Distance(this.transform.position, SpawnPoint1.transform.position);
            float dist2 = Vector3.Distance(this.transform.position, SpawnPoint2.transform.position);

            if (dist1 < dist2)
            {
                PlayerNumber = 2;
                Debug.Log("Player 2 found");
            }
            else
            {
                PlayerNumber = 1;
                Debug.Log("Player 1 found");
            }

            if (PlayerNumber == 1)
            {
                AuraController = GameObject.Find("Player1_Manager");
                AuraExpander = GameObject.Find("Aura_player1Expander");
                BridgeBars = GameObject.Find("Player1_BridgeLayers");
                StatueAnimator = GameObject.Find("Statue_Player1");
            }
            else
            {
                AuraController = GameObject.Find("Player2_Manager");
                AuraExpander = GameObject.Find("Aura_player2Expander");
                BridgeBars = GameObject.Find("Player2_BridgeLayers");
                StatueAnimator = GameObject.Find("Statue_Player2");
            }

        }
    }





    // Update is called once per frame
    void FixedUpdate()
    {
        if (!GameObject.Find("Session Manager").GetComponent<SessionManager>().SingleUserSession)
        {
            if (!isLocalPlayer)
            {
                return;
            }
        }

        if (RespDataOld - SensorData.RespOut != 0)
        {
            RespDataOld = SensorData.RespOut;
            RespChanged = true;

        }

        breathePast = breatheNow;



        // are we simulating everything?
        if (((SessionManager.GetComponent<SessionManager>().SimulateSelf == true) && (IsSimNPC == false)) || ((IsSimNPC == true)))
        {

            if (PlayerNumber == 1)
            {
                PlayerFA = DataHolder.GetComponent<SimulationData>().P1FrontAs;



                breatheNow = DataHolder.GetComponent<SimulationData>().P1Breathing;

            }

            if (PlayerNumber == 2)
            {
                PlayerFA = DataHolder.GetComponent<SimulationData>().P2FrontAs;


                breatheNow = DataHolder.GetComponent<SimulationData>().P2Breathing;

            }

        }


        else
        {  //this is the adaptation coming from sensors.
            if (PlayerNumber == 1)
            {

                if (RespChanged)
                {
                    if (respQueue.Count == 6)
                    {
                        respQueue.Dequeue();
                    }
                    respQueue.Enqueue(SensorData.RespOut);
                    respArray = respQueue.ToArray();
                    breatheNow = respArray.Average();
                    respArray = respQueue.ToArray();
                    //breatheNow = respQueue.Average();

                    if (!breatheQueueCooldown)
                    {
                        breatheQueueCooldown = true;
                        if (respMaxQueue.Count > 0)
                        {
                            respMaxQueue.Dequeue();
                        }
                        if (respMinQueue.Count > 0)
                        {
                            respMinQueue.Dequeue();
                        }
                        StartCoroutine("RespQueCoolDown");
                    }


                    if (respQueue.Max() > respMax)
                    {
                        if (respMaxQueue.Count == 3)
                        {
                            respMaxQueue.Dequeue();
                        }
                        respMaxQueue.Enqueue(respQueue.Max());
                        respMax = respMaxQueue.Average();
                    }
                    if (respQueue.Min() < respMin)
                    {
                        if (respMinQueue.Count == 3)
                        {
                            respMinQueue.Dequeue();
                        }
                        respMinQueue.Enqueue(respQueue.Min());
                        respMin = respMinQueue.Average();
                    }

                    respThreshold = (respMax - respMin) * 0.02f;


                    if (faQueue.Count == 20)
                    {
                        faQueue.Dequeue();
                    }
                    faQueue.Enqueue(SensorData.FAOut);
                    faArray = faQueue.ToArray();
                    PlayerFA = faArray.Average();
                    faArray = faQueue.ToArray();

                    //PlayerFA = SensorData.FAOut;
                    //breatheNow = SensorData.RespOut;
                    Debug.Log("new resp calculated");
                    RespChanged = false;

                }
            }

            if (PlayerNumber == 2)
            {
                if (RespChanged)
                {
                    if (respQueue.Count == 6)
                    {
                        respQueue.Dequeue();
                    }
                    respQueue.Enqueue(SensorData.RespOut);
                    respArray = respQueue.ToArray();
                    breatheNow = respArray.Average();
                    respArray = respQueue.ToArray();
                    //breatheNow = respQueue.Average();

                    if (!breatheQueueCooldown)
                    {
                        breatheQueueCooldown = true;
                        if (respMaxQueue.Count > 0)
                        {
                            respMaxQueue.Dequeue();
                        }
                        if (respMinQueue.Count > 0)
                        {
                            respMinQueue.Dequeue();
                        }
                        StartCoroutine("RespQueCoolDown");
                    }

                    
                    if (respQueue.Max() > respMax)
                    {                        
                        respMaxQueue.Enqueue(respQueue.Max());
                        respMax = respMaxQueue.Average();
                    }
                    if (respQueue.Min() < respMin)
                    {                        
                        respMinQueue.Enqueue(respQueue.Min());
                        respMin = respMinQueue.Average();
                    }


                    respThreshold = (respMax - respMin) * 0.02f;

                    if (faQueue.Count == 20)
                    {
                        faQueue.Dequeue();
                    }
                    faQueue.Enqueue(SensorData.FAOut);
                    faArray = faQueue.ToArray();
                    PlayerFA = faArray.Average();
                    faArray = faQueue.ToArray();

                    //PlayerFA = SensorData.FAOut;
                    //breatheNow = SensorData.RespOut;
                    Debug.Log("new resp calculated");
                    RespChanged = false;

                }


            }

        }

        if (PlayerNumber == 1)
        {
            otherPlayerManager = GameObject.Find("Player2_Manager");
        }

        if (PlayerNumber == 2)
        {
            otherPlayerManager = GameObject.Find("Player1_Manager");
        }
        float otherPlayerFA = otherPlayerManager.GetComponent<PlayerFAScript>().PlayerFA_Display;

        AuraController.GetComponent<PlayerFAScript>().PlayerFA_Display = PlayerFA;
        AuraController.GetComponent<PlayerFAScript>().OtherFA = otherPlayerFA;

        float fasync = Mathf.Abs(PlayerFA - otherPlayerFA);
        //print(PlayerFA + "  " + otherPlayerFA + "   " + fasync);


        // RESPIRATION CONTROLS

        // first breathe out
        if ((breatheNow < breathePast - respThreshold) && (outBreathContinues == false))
        {

            //		Debug.Log ("Player " + PlayerNumber + " breathing out");
            //		Debug.Log (PlayerNumber + ": " + breathePast + " " + breatheNow);
            StatueAnimator.GetComponent<Animator>().SetTrigger("StartOut");
            //Debug.Log ("breath out animtrigger sent");


            // send a wave.
            if (SessionManager.GetComponent<SessionManager>().Waves)
            {
                GetComponent<Adap_WaveSend>().SendWave(PlayerNumber);
            }
            if (firstwaveset && !breatheCooldown)
            {
                breatheCooldown = true;
                Debug.Log("user breathing wave sent");
                BridgeBars.GetComponent<BreathLayerer>().InitBreatheBar();
                StartCoroutine("CoolDown");

            }
            else firstwaveset = true;
           

            //	Debug.Log ("Player " + PlayerNumber + " breathing in");
            //	Debug.Log (PlayerNumber + ": " + breathePast + " " + breatheNow);




            outBreathContinues = true;

            inBreathContinues = false;


        }

        //breathing out continues
        if (outBreathContinues == true)
        {
            AuraExpander.GetComponent<AuraScaler>().expand = false;

        }



        if ((breatheNow >= breathePast + respThreshold) && (inBreathContinues == false))
        {
            StatueAnimator.GetComponent<Animator>().SetTrigger("StartIn");

            outBreathContinues = false;

            inBreathContinues = true;
        }



        //breathing in continues
        if (inBreathContinues == true)
        {
            AuraExpander.GetComponent<AuraScaler>().expand = true;


        }
    }

    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(1.0f);
        breatheCooldown = false;
    }

    IEnumerator RespQueCoolDown()
    {
        yield return new WaitForSeconds(4.0f);
        breatheQueueCooldown = false;
    }

}