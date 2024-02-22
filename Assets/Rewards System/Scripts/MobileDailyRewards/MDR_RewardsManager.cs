using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using AppsDaddyO.TimeMan;

namespace AppsDaddyO.Rewards
{
    public class MDR_RewardsManager : MonoBehaviour
    {
        public bool useFirstAndLastDays;                    // if the extra 2 list should be used or not
        //public string serverAddress;                        // Server address where the php file is hosted
        [Header("GAMEOBJECTS")]
        public GameObject rewardsPrefab;                    // the daily reward displayed to user             
        public Transform rewardsParent;                     // the parent where the rewards prefab is instantiated under
        public GameObject rewardPanel;                      // the gameObject that is instantiated once reward is claimed

        [Header("REWARDS")]
        public MDR_RewardsList rewardsList;                 // The GameObject which has all the reward items 
        private List<MDR_RewardsItem> _thisWeeksrewards;    // the weeks daily rewards chosen from the rewards list

        [Header("COLORS")]
        public Color UNAVAILABLE_Color;                     // the color when the reward is unavailable              
        public Color AVAILABLE_Color;                       // the color when the reward is Available which is on the day  
        public Color CLAIMED_Color;                         // the color when the reward is Claimed 


        //[SerializeField]
        private int _TheDay;                                // The Day the rewards should become available or reset if not claimed 
                                                            //private becuse it is dynamically changed.
        private int _nextRewardDay;                         // used in the resetCountdown timer to check if reward was claimed or not

        [Header("DATETIME")]
        // public DateTime theCurrentTime;                     // The current time received from the TimeManager request to the server     
        private DateTime endTime;                          // The time rewards should become available or rewards reset if not claimed

        [Header("TEXT OBJECTS")]
        public TextMeshProUGUI CountDownUI;                 // The UI countdown timer till next reward
        public TextMeshProUGUI timeLeft;                    // Debug time to see how long left to claim reward before reset

        [Header("BUTTON OBJECTS")]
        public GameObject closeBTN;                         // The close button


        private bool _isAvailable;                          // If rewards is available this returns true
        private bool _isClaimed;                            // If rewards is claimed this returns true
        public static MDR_RewardsManager rewardsManager;    // created to alow access to colors

        /**********************DEBUG************************/
        // This is the Debug section to control the buttons and the timer
        public void DebugNextDay()              // The method created to make the reward available or reset the list in 5 sec
        {
            if (_isClaimed) // if _isClaimed return true
            {
                StartCoroutine(DebugRewardAvailable()); //start IEnumerator DebugRewardAvailable coroutine
            }
            if (_isAvailable)   // if _isAvailable return true
            {
                StartCoroutine(DebugShowReset()); //start IEnumerator DebugShowReset coroutine
            }
        }

        public void ResetDebug() // Resets the endTime to the current day and 8am
        {
            _TheDay = TimeManager.theCurrentTime.Day; // the current day
            TimeManager.timeMan.rewardHour = 8;                   // 8am chosen
            TimeManager.timeMan.rewardMinute = 0;                 // zero minutes
            TimeManager.timeMan.rewardSecond = 0;                 // zero seconds
        }

        IEnumerator DebugRewardAvailable() // if _isClaimed is true adn coroutine started
        {
            _TheDay -= 1;                               //we minus 1 day to activated reward and make it available
            yield return new WaitForSeconds(0.5f);      // we wait for half a second
            _TheDay += 1;                               // we return to the previous value of _TheDay
            SaveWeeksRewardList();                      // we save everything incase of Application exit
        }
        IEnumerator DebugShowReset()
        {
            _TheDay -= 1;                               // We minus 1 day as if a day has gone by
            TimeManager.timeMan.rewardHour = TimeManager.theCurrentTime.Hour;                   // Setting the hour to the current hour
            TimeManager.timeMan.rewardMinute = TimeManager.theCurrentTime.Minute;               // Setting the minute to the current minute
            if (TimeManager.theCurrentTime.Second < 55)               // Check to see if seconds ar less than 55 and if true
            {
                TimeManager.timeMan.rewardSecond = TimeManager.theCurrentTime.Second + 5;       // add 5 second to countdown to allow user to pause and restart app for 
                                                                                                //debugging
            }
            else                                        // if seconds are 55 or more
            {
                TimeManager.timeMan.rewardSecond = 0;                             // Set seconds to zero
                TimeManager.timeMan.rewardMinute = TimeManager.theCurrentTime.Minute + 1;       // add one minute which still add the 5 seconds to the countdown timer
            }
            yield return new WaitForSeconds(0.5f);      // wait half a second
            //_TheDay += 1;
            SaveWeeksRewardList();                      // Save everything incase user exits application
        }

        public void ResetPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
        }

        /*********************END DEBUG***********************/

        private void Awake()
        {
            rewardsManager = this;                              //Setting the Static Variable created earlier to this instance

            // The coroutine is started to delay the application till we get the current time from the server

            CheckIfRewardsListExists(); // Check to see if there is  a saved list and if not creates one

            endTime = DateTime.Parse(               // Assigning the endTime variable a value
                TimeManager.theCurrentTime.Year.ToString() + "/" +
                TimeManager.theCurrentTime.Month + "/" +
                _TheDay.ToString() + " " +
                TimeManager.timeMan.rewardHour.ToString() + ":" +
                TimeManager.timeMan.rewardMinute.ToString() + ":" +
                TimeManager.timeMan.rewardSecond.ToString());
            Debug.Log("endTime: " + endTime.ToString()
                );

            if (TimeManager.theCurrentTime > endTime && _isAvailable) // Check whether the reset timer has run out and if so calls the method
            {
                ResetDailyRewards();   // method created to reset rewards if daily reward is not claimed
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            // TimeManager.theCurrentTime = TimeManager.theCurrentTime.AddSeconds(Time.unscaledDeltaTime); // we add seconds to the returned server time

            endTime = DateTime.Parse(                 // constantly checking endTime value for changes in inspector and for Debugging 
                TimeManager.theCurrentTime.Year.ToString() + "/" +
                TimeManager.theCurrentTime.Month + "/" +
                _TheDay.ToString() + " " +
                TimeManager.timeMan.rewardHour.ToString() + ":" +
                TimeManager.timeMan.rewardMinute.ToString() + ":" +
                TimeManager.timeMan.rewardSecond.ToString()
                );
            TimeCheck(TimeManager.theCurrentTime);                  // Method to check compare the DateTimes of TimeManager.theCurrentTime and endTime
        }

        // A Method to compare DateTimes, create a timespan for UI display and control if rewards list resets of if reward becomes available
        void TimeCheck(DateTime time)
        {
            if (time < endTime)                         // If the current Time is less than endTime
            {
                // we check to see if _isAvailable is false to create a timespan for the next reward to be available
                if (!_isAvailable)
                {
                    TimeSpan span = endTime - time;             // time left is created
                    // convert the DateTime into string with format of 00:00:00 for UI display
                    string countdown = string.Format("{0:D2}:{1:D2}:{2:D2}", span.Hours, span.Minutes, span.Seconds);
                    // Add the string to the text GameObject
                    CountDownUI.text = string.Format("Come back in {0} for your next reward", countdown);

                    closeBTN.SetActive(true); // enable close button to hide the claim button
                }
                else        // we check to see if _isAvailable is true / so reward is available
                {
                    // if true, we create a countdown timer for item to be claimed
                    TimeSpan endSpan;               // A Timespan variable we create later
                    TimeSpan span = endTime - time; // create the TimeSpan for the countdown

                    /*********PART OF DEBUGGING***************/
                    string countdown = string.Format(   // create a string for Debug display purposes
                        "{0:D2}:{1:D2}:{2:D2}",
                        span.Hours,
                        span.Minutes,
                        span.Seconds
                        );
                    timeLeft.text = countdown.ToString(); // used as part of the debug to view time left to claim reward               
                    /*********END OF DEBUGGING***************/

                    if (span.Milliseconds >= 990)       // creating millisecond to be used in endTime for quick execution of function
                    {
                        //if milliseconds are greater that 990 we do the following 
                        int milli = span.Milliseconds;
                        endSpan = new TimeSpan(span.Days, 00, 00, 00, milli);
                    }
                    //the above helps the Method below run once

                    if (span == endSpan)  // comparing current time and endTime and if they are equal then reward has not been claimed 
                    {
                        ResetDailyRewards();    // reset the weekly rewards as the daily reward was not claimed 
                    }

                    //if span is not equal to endSpan continue the countdown and reward is still available
                    closeBTN.SetActive(false);  // reward is still available so claim button is shown by hiding the close button
                    CountDownUI.text = "Your reward is ready to be claimed"; // The UI text show reward is available

                }

            }
            else            // If Time has past the endTime
            {
                if (!_isAvailable)   // if the reward is not available
                {
                    Debug.Log("TIME HasEnded");

                    if (!_isClaimed)    // and if the reward is not claimed
                    {
                        Item_isAvailable(); // making the reward available to be claimed
                    }
                    else
                    {
                        _isClaimed = false;     // if the reward is claimed we make it false to run the if statement above
                    }


                }
                else            // if reward is available
                {
                    CountDownUI.text = "Your reward is ready to be claimed"; // we continue to show the UI text
                }
                // the above helped me with the ;oading of the saved rewards list
            }
        }

        void CheckIfRewardsListExists()
        {
            if (PlayerPrefs.GetString("WeeksRewardList") == "")  // checks if Saved rewards list is empty
            {
                GetNewWeeksRewards();                           // because Save is empty create a new rewards list
            }
            else                                                // if Saved rewards list is not empty
            {
                //splitting the save string to be used
                _thisWeeksrewards = new List<MDR_RewardsItem>();     // create a new list
                string PPerfsRewards = PlayerPrefs.GetString("WeeksRewardList");  // get Saved string for rewards list
                string[] theComString = PPerfsRewards.Split('~');       // split the string at the symbol
                _nextRewardDay = int.Parse(theComString[1]);            // convert string to int and assign to 
                _isAvailable = bool.Parse(theComString[2]);             // convert string to bool and set 
                _isClaimed = bool.Parse(theComString[3]);               // convert string to bool and set
                _TheDay = int.Parse(theComString[4]);                   // convert string to int and assign to

                // spliting the rewards list string into individual rewards
                string[] theWeeksRewardsString = theComString[0].Split('|'); // Split the string into an Array
                int day = 1;
                foreach (string st in theWeeksRewardsString)     // for each string in the array   
                {
                    string[] theReward = st.Split(',');         // Split the string into an Array

                    if (useFirstAndLastDays)        // if this bool is true we iterate through 3 different lists of rewards
                    {
                        foreach (MDR_RewardsItem firstR in rewardsList.firstDayOrAllRewards) // the first days rewards to choose from
                        {
                            if (theReward[0] == firstR.rewardName.ToString())    // comparing string names
                            {
                                InstantiateSavedRewards(firstR, theReward[2], day); // method to instantiate the reward UI
                                day++;
                            }
                        }
                        foreach (MDR_RewardsItem midR in rewardsList.midWeekRewards) // the middle days rewards to choose from
                        {
                            if (theReward[0] == midR.rewardName.ToString())    // comparing string names
                            {
                                InstantiateSavedRewards(midR, theReward[2], day); // method to instantiate the reward UI
                                day++;
                            }
                        }
                        foreach (MDR_RewardsItem lastR in rewardsList.lastDayRewards) // the last day rewards to choose from
                        {
                            if (theReward[0] == lastR.rewardName.ToString())    // comparing string names
                            {
                                InstantiateSavedRewards(lastR, theReward[2], day); // method to instantiate the reward UI
                                day++;
                            }
                        }
                    }
                    else        // if this bool is false we iterate through 1 list of rewards
                    {
                        foreach (MDR_RewardsItem firstR in rewardsList.firstDayOrAllRewards)
                        {
                            if (theReward[0] == firstR.rewardName.ToString())    // comparing string names
                            {
                                InstantiateSavedRewards(firstR, theReward[2], day); // method to instantiate the reward UI
                                day++;
                            }
                        }
                    }


                }
            }
        }

        void CreateRewardObjects() // a method to instantiate reward objects when a new rewards list is created 
        {
            // Debug.Log("Rewards were instantiated");
            int day = 1;                                     // an int to track the day the instantiated reward should display
            foreach (MDR_RewardsItem reward in _thisWeeksrewards) // iterating through the 8 chosen rewards list
            {
                GameObject instance = Instantiate(rewardsPrefab); // creating each reward gameObject
                instance.name = rewardsPrefab.name;               //removing the (Clone) from the instantiated object
                instance.transform.SetParent(rewardsParent);      // setting the parent gameobject to which the instantiated object                                                       will be placed  
                instance.transform.localScale = Vector3.one;      // setting the scale of the object to 1
                instance.GetComponent<MDR_RewardOnItem>().reward = reward; // get component under instantiated object ad assign the                                                                 reward
                instance.transform.GetChild(1).GetComponent<Text>().text = "Day " + day.ToString(); // Reward day to display
                instance.transform.GetChild(2).GetComponent<Image>().sprite = reward.rewardImage;       // Reward day to display
                instance.transform.GetChild(3).GetComponent<Text>().text = reward.rewardAmount.ToString();  // the rewards amount

                day++; // increase the day by 1 after each run 8 will be the final day
            }
            Item_isAvailable(); // method to make the item available

        }

        // a method to instantiate reward objects from the saved string 
        void InstantiateSavedRewards(MDR_RewardsItem item, string state, int day)
        {
            GameObject instance = Instantiate(rewardsPrefab);       // creating each reward gameObject
            instance.name = rewardsPrefab.name;                     //removing the (Clone) from the instantiated object
            instance.transform.SetParent(rewardsParent);            // setting the parent gameobject to which the instantiated object                                                       will be placed 
            instance.transform.localScale = Vector3.one;            // setting the scale of the object to 1
            instance.GetComponent<MDR_RewardOnItem>().reward = item;    // get component under instantiated object ad assign the                                                                 reward
            instance.transform.GetChild(1).GetComponent<Text>().text = "Day " + day.ToString();         // Reward day to display
            instance.transform.GetChild(2).GetComponent<Image>().sprite = item.rewardImage;             // Reward day to display
            instance.transform.GetChild(3).GetComponent<Text>().text = item.rewardAmount.ToString();    // the rewards amount
            // convert the string into an int to compare and assign the correct state accordingly
            if (int.Parse(state) == 2) // if string equals 2
            {
                // the state is claimed
                instance.GetComponent<MDR_RewardOnItem>().state = MDR_RewardOnItem.State.CLAIMED;
            }
            else if (int.Parse(state) == 1) // if string equals 1
            {
                // the state is available
                instance.GetComponent<MDR_RewardOnItem>().state = MDR_RewardOnItem.State.AVAILABLE;
            }
            else                            // if string equals 0
            {
                // the state is unavailable
                instance.GetComponent<MDR_RewardOnItem>().state = MDR_RewardOnItem.State.UNAVAILABLE;
            }
        }

        // A Method to create a new set of rewards
        public void GetNewWeeksRewards()
        {
            _thisWeeksrewards = new List<MDR_RewardsItem>(); // create a new list

            for (int i = 1; i < 9; i++)     // loop through this 8 times
            {
                if (useFirstAndLastDays)    // if this returns true
                {
                    if (i == 1)             // if day 1
                    {
                        // go through the first days list and randomly pick a reward and add to list
                        int num = UnityEngine.Random.Range(0, rewardsList.firstDayOrAllRewards.Count);
                        _thisWeeksrewards.Add(rewardsList.firstDayOrAllRewards[num]);
                    }
                    else if (i == 8)     // if day 8 so the last day
                    {
                        // go through the last days list and randomly pick a reward and add to list
                        int num = UnityEngine.Random.Range(0, rewardsList.lastDayRewards.Count);
                        _thisWeeksrewards.Add(rewardsList.lastDayRewards[num]);
                    }
                    else                // if day is greater than 1 but less than 8
                    {
                        // go through the mid days list and randomly pick a reward and add to list
                        int num = UnityEngine.Random.Range(0, rewardsList.midWeekRewards.Count);
                        MDR_RewardsItem rewardItem = rewardsList.midWeekRewards[num];
                        if (!_thisWeeksrewards.Contains(rewardItem))
                        {
                            _thisWeeksrewards.Add(rewardItem);
                        }
                        else
                        {
                            i--;
                        }
                    }
                }
                else        //if useFirstAndLastDays returns false
                {
                    List<MDR_RewardsItem> allRewards = GetAllRewards();
                    // go through the first days list and randomly pick the 8 rewards and add them to the list
                    int num = UnityEngine.Random.Range(0, allRewards.Count);
                    MDR_RewardsItem rewardItem = allRewards[num];
                    if (!_thisWeeksrewards.Contains(rewardItem))
                    {
                        _thisWeeksrewards.Add(rewardItem);
                    }
                    else
                    {
                        i--;
                    }
                }
            }
            _nextRewardDay = 0;         // set the reward day to zero
            CreateRewardObjects();      // Instantiate the gameobjects from the list
            _TheDay = TimeManager.theCurrentTime.Day; // set the day to the server current day
        }

        private List<MDR_RewardsItem> GetAllRewards()
        {
            List<MDR_RewardsItem> _allrewards = new List<MDR_RewardsItem>(); // create a new list
            foreach (MDR_RewardsItem item in rewardsList.firstDayOrAllRewards)
            {
                _allrewards.Add(item);
            }
            foreach (MDR_RewardsItem item in rewardsList.midWeekRewards)
            {
                _allrewards.Add(item);
            }
            foreach (MDR_RewardsItem item in rewardsList.lastDayRewards)
            {
                _allrewards.Add(item);
            }
            return _allrewards;
        }

        // Here the reward is made available by changing the backgroung to the available color and setting the bool to true
        private void Item_isAvailable()
        {
            Debug.Log("ITEM AVAIALABLE: " + _nextRewardDay);
            // check the state of the reward at index of _nextDayReward and if the reward is UNAVAILABLE
            if (rewardsParent.GetChild(_nextRewardDay).GetComponent<MDR_RewardOnItem>().state == MDR_RewardOnItem.State.UNAVAILABLE)
            {
                // change the state of the reward
                rewardsParent.GetChild(_nextRewardDay).GetComponent<MDR_RewardOnItem>().state = MDR_RewardOnItem.State.AVAILABLE;
                // change the colour of the reward to the AVAILABLE_Color
                rewardsParent.GetChild(_nextRewardDay).GetChild(0).GetComponent<Image>().color = AVAILABLE_Color;
            }
            _isAvailable = true;        // set the bool to true
            _TheDay++;
            SaveWeeksRewardList();      // save the list after changes
            closeBTN.SetActive(false);  // show the claim button by hiding the close button
        }

        // This method is called once the timeLeft countdown timer has run out and reward has not been claimed
        void ResetDailyRewards()
        {
            _TheDay = TimeManager.theCurrentTime.Day;     // set the day to the servers current day

            while (rewardsParent.childCount > 0)    // while there are still gameObjects under the rewardsParent
            {
                DestroyImmediate(GameObject.FindGameObjectWithTag("RewardOBJ")); // destroy each gameObject to clear the UI
            }

            GetNewWeeksRewards();               // once above is done, a call to the method to create a new set of rewards
            closeBTN.SetActive(false);          // claim button is made available as the first reward is always ready to be claimed
            Debug.Log("REWARD HAS BEEN LOST");
        }

        //Here we attach this method to the claim button in the UI to make the reward claimable
        public void ClaimDailyReward()
        {
            foreach (Transform Obj in rewardsParent)    // for each object under the rewardParent
            {
                MDR_RewardOnItem.State state = Obj.GetComponent<MDR_RewardOnItem>().state;
                if (state == MDR_RewardOnItem.State.AVAILABLE) // if the state of the object is available
                {
                    // a pop up display showing the reward is displayed
                    MDR_RewardOnItem RoI = Obj.GetComponent<MDR_RewardOnItem>();      // the pop up reward item is equal to the reward                                                                     item
                    RoI.state = MDR_RewardOnItem.State.CLAIMED;  // the state is changed to CLAIMED
                    Obj.GetChild(0).GetComponent<Image>().color = CLAIMED_Color;  // the color is changed to CLAIMED_Color
                    Obj.GetChild(4).gameObject.SetActive(true);                   // A tick is placed over the object to show it has                                                                    been claimed
                    rewardPanel.transform.GetChild(3).GetComponent<Image>().sprite = RoI.reward.rewardImage; //the pop up image is                                                                                              updated
                    rewardPanel.transform.GetChild(4).GetComponent<Text>().text = // Add display text to pop up
                        "Got "
                        + RoI.reward.rewardAmount.ToString() + " " 
                        + RoI.reward.rewardName;
                    rewardPanel.SetActive(true); // set the pop up panel to true
                }
            }
            _isClaimed = true;                      // set _isClaimed to true
            _isAvailable = false;                   // set _isAvailable to false
            _TheDay = TimeManager.theCurrentTime.Day + 1;         //add 1 to the day to simulate tomorrow
            _nextRewardDay++;                       // add 1 to _nextRewardDay++

            if (_nextRewardDay == 8)                // if the _nextRewardDay++ is = to the last 
            {
                Debug.Log("GOT NEW REWARDS");
                ResetDailyRewards();                // reset the rewrad list as everything has been claimed
            }
            SaveWeeksRewardList();                  // save the new list

            
        }


        // A method to save a string to be able to load and keep track 
        private void SaveWeeksRewardList()
        {
            Debug.Log("A SAVE HAPPENED");
            List<string> rewardsSaveString = new List<string>(); // create a new or overwrite a list
            int stateNum; // the variable to hold the number related to the state

            foreach (Transform item in rewardsParent)  // for each gameObject under the rewards parent
            {
                MDR_RewardOnItem i = item.GetComponent<MDR_RewardOnItem>();

                if (i.state == MDR_RewardOnItem.State.UNAVAILABLE) // if i equal to unavailable the number is 0
                {
                    stateNum = 0;
                }
                else if (i.state == MDR_RewardOnItem.State.AVAILABLE) // if i equal to available the number is 1
                {
                    stateNum = 1;
                }
                else                        // if i equal to claimed the number is 2
                {
                    stateNum = 2;
                }

                rewardsSaveString.Add(              // add to list
                                                    // the created string including name, amount and state
                    i.reward.rewardName + "," + i.reward.rewardAmount + "," + stateNum + "|" // the created string
                    );
            }

            string theSaveList = string.Join("", rewardsSaveString.ToArray());     //Join the RewardsSaveString into a string so we can                                                                         add further things to save
            string[] theSaveArray = new string[]// create a new array to hold the rewards list and anything else that needs to be saved
            {
                theSaveList,                // the rewards list
                _nextRewardDay.ToString(),  // the _nextRewardDay int to string
                _isAvailable.ToString(),    // the _isAvailable bool wether true or false
                _isClaimed.ToString(),      // the _isClaimed bool wether true or false
                _TheDay.ToString()          // the _TheDay int to string
            };
            string theSaveString = string.Join("~", theSaveArray);      // join the above with the symbol
            PlayerPrefs.SetString("WeeksRewardList", theSaveString);    // Save string in this case to playerPrefs
            Debug.Log("STRING SAVED: " + theSaveString);
        }
    }
}