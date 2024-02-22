using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

namespace AppsDaddyO.TimeMan
{
    // Attached to GameObject timeManager for setting through inspector
    public class TimeManager : MonoBehaviour
    {
        public bool useOwnServer;
        public string ServerAddress;                // The ServerAddress where the current time is fetched
        [Header("REWARDS RESET TIME")]
        public int rewardHour;                      // The hour rewards should become available or reset if not claimed
        public int rewardMinute;                    // The minute rewards should become available or reset if not claimed
        public int rewardSecond;                    // The second rewards should become available or reset if not claimed
        public GameObject rewardsPanel;             // The rewards main GameObject
        

        public static DateTime theCurrentTime;      // the current time as static variable for access accross scripts
        public static TimeManager timeMan;          // this timeManager script as a static variable for access accross scripts

        
        private void Awake()
        {
            timeMan = this;                     // Set the Static Variable to this instance (timeManager)
        }
        // we create a coroutine which is required to get info from the internet with a callback action that returns the curent time. The callback is to be used in the RewardsManager. 
        private void Start()
        {
            StartCoroutine(GetCurrentTime());   // Start a coroutine to connect to server and get current time
        }
        public IEnumerator GetCurrentTime()
        {
            UnityWebRequest request = UnityWebRequest.Get(ServerAddress); // The Request to the server is created.
            yield return request.SendWebRequest(); // The request is created

            if (request.isNetworkError || request.isHttpError) // Checking if there was an error in the sending of the request
            {
                Debug.Log(request.error); // If error occured debug the error

            }
            else
            {
                if (request.isDone) // If request was successful
                {
                    Debug.Log("connection Successfull");
                    DateTime currentTime;
                    string data = request.downloadHandler.text; // download the text returned by the PHP file which is the current DateTime in string format.
                    if (!useOwnServer)
                    {
                        Debug.Log(ReturnStringFromWorldTimeApi(data));
                        currentTime = System.DateTime.Parse(ReturnStringFromWorldTimeApi(data)); // Convert the string received fron the request to a DateTime                        
                    }
                    else
                    {
                        currentTime = System.DateTime.Parse(data); // Convert the string received fron the request to a DateTime
                    }
                   

                    Debug.Log("This is from the server: " + currentTime);
                    // Debug.Log("TimeManger: " + currentTime);
                    // Debug.Log("DateTimeNow: " + DateTime.Now);
                    // callback to return is the current DateTime.
                    theCurrentTime = currentTime;                   // Set theCurrentTime variable to the returned time from server
                    if (SceneManager.GetActiveScene().name == "DemoScene (Landscape)" || SceneManager.GetActiveScene().name == "DemoScene (Portrait)")
                    {
                        GameObject.Find("OpenUI").transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
                    }
                    
                    rewardsPanel.SetActive(true);                   // Set the rewards panel active on start of application
                }
            }
        }
        void Update()
        {
            theCurrentTime = theCurrentTime.AddSeconds(Time.unscaledDeltaTime); // Simulate realtime by adding unscaled seconds to the returen                                                                      server time
        }

        string ReturnStringFromWorldTimeApi(string jsonText)
        {
            MyCustomDateTimeClass myObject = new MyCustomDateTimeClass();

            myObject = JsonUtility.FromJson<MyCustomDateTimeClass>(jsonText);

            Debug.Log("this is the json parsed: " + myObject.datetime);
            return myObject.datetime;
        }
    }
}

[Serializable]
public class MyCustomDateTimeClass
{
    public string datetime;
}