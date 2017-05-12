using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;



public class TCPClientFA : MonoBehaviour
{
    private System.Threading.Thread myThread;
    public bool doit = false;
    public float upsi = 0.0F;
    float posi = 0.0F;

    // Use this for initialization
    public static bool running = true;



    void Start()
    {
        TCPClientThreadFA myTCPClientFA = new TCPClientThreadFA(this);
        myThread = new System.Threading.Thread(new ThreadStart(myTCPClientFA.myTCPClient));
        myThread.Start();

        //  GlobalClass.EDA = 1;
        eeg_data.RespOut = 0;
        eeg_data.FAOut = 0;

    }


    // Update is called once per frame
    void Update()
    {
        if (doit)
        {
            //Debug.Log("Eyecoordinates are: " + GlobalClass.eyeCoordinate_x + " " + GlobalClass.eyeCoordinate_y);
            doit = false;
        }
    }
    void OnApplicationQuit()
    {
        Debug.Log("Application quit");
        running = false;
    }
    void OnDestroy()
    {
        running = false;
    }
}

public class TCPClientThreadFA
{

    TCPClientFA openvibeClient;
    TcpClient myClient;
    int numberOfChannels;
    int numberOfAlphaChannels;
    int numberOfThetaChannels;
    float frontalAss;
    float alphaLeft;
    float alphaRight;
    float respiration;
    float prevRespiration;
    int BaselineLength;
    float faMin;
    float AlphaPower;

    float faMax;

    //float ThetaMax;

    float faRange = 0.01f;
    float ThetaRange = 0.01f;

    string StaticIP = "130.233.50.206";


    public TCPClientThreadFA(TCPClientFA c)
    {
        openvibeClient = c;
        //	numberOfAlphaChannels = 31;
        //	numberOfThetaChannels = 31;
        //	BaselineLength = 30;
        numberOfAlphaChannels = 3;

        BaselineLength = 2;

        faMin = 1000000;
        faMax = -1;
    }

    public void myTCPClient()
    {
        if (PlayerPrefs.HasKey("StaticIPStored"))
        {
            StaticIP = PlayerPrefs.GetString("StaticIPStored");
            Debug.Log(StaticIP + "from save");
        }




        float tmp_EDA = 0f;
        string input, stringData;
        byte[] message = new byte[128];
        int bytesRead;

        myClient = null;
        Debug.Log("running tcp client");


        myClient = new TcpClient();
        //myClient.Connect("localhost", 9995);
        myClient.Connect(StaticIP, 9995);
        Debug.Log("running tcp client2");

        NetworkStream myClientStream = myClient.GetStream();

        while (true)
        {
            Debug.Log("running tcp client3");
            bytesRead = 0;
            Debug.Log("running tcp client4");
            try
            {

                bytesRead = myClientStream.Read(message, 0, 128);

            }
            catch (Exception e)
            {
                Debug.Log("Caugth exception : " + e);
                break;
            }
            // If we received 0 bytes the connection was closed.
            Debug.Log("running tcp client7");

            if (bytesRead == 0)
            {
                Debug.Log("Connection closed.");
                break;
            }
            Debug.Log("Something was read from the server");
            stringData = Encoding.ASCII.GetString(message, 0, bytesRead);
            string[] words = stringData.Split(',');

            // Assume 2x32 channels of data.
            numberOfChannels = words.GetLength(0);
            if (numberOfChannels != (numberOfAlphaChannels + numberOfThetaChannels))
            {
                Debug.Log("Invalid number of channels: " + numberOfChannels);
                continue;
            }

            try
            {

                alphaLeft = (float)Convert.ToDouble(words[0]);
                alphaRight = (float)Convert.ToDouble(words[1]);
                respiration = (float)Convert.ToDouble(words[2]);

                frontalAss = Mathf.Log(alphaRight) - Mathf.Log(alphaLeft);
                /*for (int i = numberOfAlphaChannels; i < (numberOfAlphaChannels+numberOfThetaChannels);
				     i++){
					ThetaPower = (float)Convert.ToDouble (words[i]) + ThetaPower;
					 	
				}
				ThetaPower = ThetaPower/numberOfThetaChannels;*/
                Debug.Log("Relax: " + eeg_data.RespOut);

                //		if (BaselineLength > 0){



                if (frontalAss < faMin)
                {
                    faMin = frontalAss;
                }
                /*if(ThetaPower < ThetaMin){
                    ThetaMin = ThetaPower;
                }*/

                if (frontalAss > faMax)
                {
                    faMax = frontalAss;
                }
                /*	if(ThetaPower > ThetaMax){
						ThetaMax = ThetaPower;
					} */
                faRange = faMax - faMin;
                //ThetaRange = ThetaMax - ThetaMin;

                //	Debug.Log("AlphaRange: " + AlphaRange);
                Debug.Log("ThetaRange: " + ThetaRange);
              //  Debug.Log("ThetaMin: " + ThetaMin);
             //   Debug.Log("ThetaMax: " + ThetaMax);
           //     Debug.Log("thetapower: " + ThetaPower);

                Debug.Log(" Toimii kuin junan vessa - works like a toilet in a train ");

                Debug.Log("AlphaMin: " + faMin);
                Debug.Log("AlphaMax: " + faMax);
                Debug.Log("frontalAss: " + frontalAss);
                Debug.Log("AlphaRange: " + faRange);

                if (BaselineLength > 0)
                {
                    eeg_data.RespOut = 0;
                    eeg_data.FAOut = 0;
                }
                else
                {
                    eeg_data.RespOut = (frontalAss - faMin) / faRange - 0.2f;
                    eeg_data.FAOut = respiration - prevRespiration;
                }

                if (BaselineLength == 1)
                {
                    faRange = faMax - faMin;
                    prevRespiration = respiration;
                    Debug.Log("AlphaRange: " + faRange);
                    Debug.Log("BaselineLength: " + BaselineLength);
                }
                if (BaselineLength > 0)
                {
                    BaselineLength--;
                }
                /*
                if(AlphaPower < AlphaMin){
                    AlphaMin = AlphaPower;
                }
                if(ThetaPower < ThetaMin){
                    ThetaMin = ThetaPower;
                }

                if(AlphaPower > AlphaMax){
                    AlphaMax = AlphaPower;
                }
                if(ThetaPower > ThetaMax){
                    ThetaMax = ThetaPower;
                }*/

                //			}

            }
            catch (Exception e)
            {
                Debug.Log("Vituiks man" + e.ToString());
                continue;
            }

            Debug.Log("input from server " + stringData);
            Debug.Log("Alpha power is: " + AlphaPower);

        }
        //myClient.Close();
        //     Console.WriteLine("Disconnecting...");
        //     myListener.Shutdown(SocketShutdown.Both);
        //    myListener.Close();
    }
    ~TCPClientThreadFA()
    {
        myClient.Close();
    }
}
