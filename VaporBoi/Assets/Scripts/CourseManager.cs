using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
    The Course Manager manages the core gameplay loop of
    throwing, checking win/hazard/OOB conditions on the throws
    and teleporting the player to the next shot within the course. 
*/

public class CourseManager : MonoBehaviour
{
    [SerializeField] private GameObject _scoreboard; 

    public enum GameState
    {
        PreGame,
        InPlay,
        PostGame,
        Ended
    }
    private GameState _gameState = GameState.PreGame;

    //Level Score Parameters
    //Timer
    private float _timer = 0f;
    public float Timer { get { return _timer; } }

    //Shot Counter.
    private int _shotCount = 0;
    public int ShotCount { get { return _shotCount; } }

    //Distance Tracker
    private float _travelDistance = 0f;
    public float TravelDistance { get { return _travelDistance; } }

    private float _penaltyDistance = 0f;
    public float PenaltyDistance { get { return _penaltyDistance; } }

    //Proximity to bullseye
    private float _bullseyeProximity = 0f;
    public float BullseyeProximity { get { return _bullseyeProximity; } }

    //Papers Used
    private int _papersUsed = 0;
    public int PapersUsed { get { return _papersUsed; } }

    //Gnomes Smashed 
    private int _objectsDestroyed = 0;
    public int GnomesSmashed { get { return _objectsDestroyed; } }

    //Window Delivery?
    private bool _windowDelivery = false;
    public bool WindowDelivery { get { return _windowDelivery; } }

    //Important Level Objects
    private GoalBox _goal;
    private List<PenaltyBox> _penaltyBoxes = new List<PenaltyBox>();
    private List<HazardBox> _hazardBoxes = new List<HazardBox>();
    private List<WindowBox> _windowBoxes = new List<WindowBox>();
    private List<BreakableObject> _breakableObjects = new List<BreakableObject>();
    private GameObject _player; 
    private bool _isEnded = false;

    void Start() 
    { 
        _timer = 0f;
        _shotCount = 0;
        LevelSetup(); 
    }


    void LevelSetup()
    {
        //Gather the Player
        _player = GameObject.FindGameObjectWithTag("Player");

        //Gather all the Special Hitboxes
        _goal = GameObject.FindGameObjectWithTag("Goal").GetComponent<GoalBox>();

        GameObject[] tempGO = GameObject.FindGameObjectsWithTag("Hazard");
        foreach(GameObject go in tempGO) { _hazardBoxes.Add(go.GetComponent<HazardBox>()); }

        tempGO = GameObject.FindGameObjectsWithTag("Penalty");
        foreach(GameObject go in tempGO) { _penaltyBoxes.Add(go.GetComponent<PenaltyBox>()); }

        tempGO = GameObject.FindGameObjectsWithTag("Window");
        foreach(GameObject go in tempGO) { _windowBoxes.Add(go.GetComponent<WindowBox>()); }

        tempGO = GameObject.FindGameObjectsWithTag("Breakable");
        foreach(GameObject go in tempGO) { _breakableObjects.Add(go.GetComponent<BreakableObject>()); }
       // GameObject.Find("Left Hand Ray").SetActive(false);
       // GameObject.Find("Right Hand Ray").SetActive(false);
    }

    void Update()
    {
        if(_gameState == GameState.Ended){ return; }
        switch(_gameState)
        {
            case GameState.InPlay:
            {
                //Increment Timer
                _timer += Time.deltaTime;
                //Determine Level State
                LevelLogic();
                //Get Updated Player Info
                _travelDistance = _player.GetComponent<DistanceCalculator>().Distance;
                _shotCount = _player.GetComponent<ThrowTracker>().NumThrows;
                break;
            }

            case GameState.PostGame:
            {
                CalculateScore();
                Debug.Log("Post Game Display");
                PostGameDisplayTrigger();
                _gameState = GameState.Ended;
                //_isEnded = true;
                break;
            }
            default:
            {
                break;
            }
        }
    }

    // Displays the total score of the player for the hole, golf style. Is also  
    // displays a UI that lets the player continue or quit. 
    void PostGameDisplayTrigger() 
    {
        if(_isEnded){ return; }
        //Enable Post game menu   
        Debug.Log("Post Game Display");
        Debug.Log("Shots Taken: " + _shotCount);
        Debug.Log("Distance Traveled: " + _travelDistance);
        Debug.Log("Time Taken: " + _timer);
        Debug.Log("Papers Used: " + _papersUsed);
        Debug.Log("Distance From Bullseye: " + _bullseyeProximity);
        Debug.Log("Objects Destroyed: " + _objectsDestroyed);
        Debug.Log("Window Delivery: " + _windowDelivery);
        Debug.Log("Penalty Distance: " + _penaltyDistance);
        if (GameObject.Find("LevelManager") == null){ return ;}
        GameObject.Find("LevelManager").GetComponent<LevelManager>().LoadNextLevel();
        // Get the component
        //GameObject camera = GameObject.Find("Main Camera");
        //GameObject tMP = Instantiate(_scoreboard, camera.transform);
        //TMP_InputField tMP_Input = tMP.GetComponent<TMP_InputField>();
        //tMP_Input.interactable = true;
        // To get the text
        //tMP_Input.text = _shotCount.ToString() + "\n" + _travelDistance.ToString() + "\n" + _timer.ToString() + "\n" + _papersUsed.ToString() + "\n" + _bullseyeProximity.ToString() + "\n" + _objectsDestroyed.ToString() + "\n" + _windowDelivery.ToString() + "\n" + _penaltyDistance.ToString();

        //Change over to ray casters 
        //GameObject.Find("Left Hand").SetActive(false);
        //GameObject.Find("Right Hand").SetActive(false);
        //GameObject.Find("Left Hand Ray").SetActive(true);
        //GameObject.Find("Right Hand Ray").SetActive(true);

    }

    //Sets any manager specific data needed for the next level.
    void CalculateScore()
    {
        //Get distance traveled in penalty boxes
        foreach(PenaltyBox penaltyBox in _penaltyBoxes) 
        {
            _penaltyDistance += penaltyBox.PenaltyDistance; 
        }
        
        //Get number of breakable objects the player broke
        foreach(BreakableObject bo in _breakableObjects) 
        {
            if (bo.IsTriggered) { _objectsDestroyed++; }
        }   
    }  

    void HazardDisplay()
    {
        //Enable Hazard Display Indicating that the Paper went into a Hazard
        Debug.Log("Hazard Display");
    }

    void PenaltyDisplay()
    {
        //Enable Penalty Display Indicating that the Paper went into a Hazard
        Debug.Log("Penalty Display");
    }

    void LevelLogic()
    {
        if(_goal.IsTriggered && (_isEnded == false)) 
        {
            _gameState = GameState.PostGame;
        }
        
        //Check for any triggered penalty or hazard boxes or windows
        foreach(PenaltyBox penaltyBox in _penaltyBoxes)
        {
            if (penaltyBox == null) { break; }
            if (penaltyBox.IsTriggered) 
            { 
                //Add Penalty Shot
                //_shotCount++;
                penaltyBox.turnOffTrigger();
                PenaltyDisplay();
            }
        }

        foreach(HazardBox hazardBox in _hazardBoxes)
        {
            if (hazardBox == null) { break; }
            if (hazardBox.IsTriggered) 
            { 
                //Add Penalty Shot
                _shotCount++;
                hazardBox.turnOffTrigger();
                HazardDisplay();
            }
        }

        foreach(WindowBox window in _windowBoxes)
        {
            if (window == null) { break; }
            if (window.IsTriggered) 
            { 
                _windowDelivery = true;
                _gameState = GameState.PostGame;
            }
        }
    }

    public void SetBullseyeProximity(float distance)
    {
        _bullseyeProximity = distance;
    }

    public void StartGame()
    {
        _gameState = GameState.InPlay;
    }

    public void IncrementShot()
    { 
        _shotCount++;
        //Debug.Log("Shot taken");
        //Debug.Log(_shotCount);
    }

    public void IncrementPapers()
    { 
        _papersUsed++;
        //Debug.Log("Paper Spawned");
        //Debug.Log(_papersUsed);
    }
}

