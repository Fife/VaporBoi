using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    The Course Manager manages the core gameplay loop of
    throwing, checking win/hazard/OOB conditions on the throws
    and teleporting the player to the next shot within the course. 
*/

public class CourseManager : MonoBehaviour
{
    enum GameState
    {
        PreGame,
        InPlay,
        PostGame
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
    private float _distance = 0f;
    private float Distance { get { return _distance; } }

    //Papers Used
    private int _papersUsed = 1;
    private int PapersUsed { get { return _papersUsed; } }


    //Important Level Objects
    private GoalBox _goal;
    private List<PenaltyBox> _penaltyBoxes = new List<PenaltyBox>();
    private List<HazardBox> _hazardBoxes = new List<HazardBox>();
    private GameObject _player; 

    void Awake() 
    { 
        _timer = 0f;
        _shotCount = 0;
        LevelSetup(); 
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
    }

    public void StartGame()
    {
        _gameState = GameState.InPlay;
    }

    void Update()
    {
        if (_gameState == GameState.InPlay)
        {
            //Increment Timer
            _timer += Time.deltaTime;

            //Determine Level State
            LevelLogic();

            //Get Updated Player Info
            _distance = _player.GetComponent<DistanceCalculator>().Distance;
            _shotCount = _player.GetComponent<ThrowTracker>().NumThrows;
        }   
    }


    // Displays the total score of the player for the hole, golf style. Is also  
    // displays a UI that lets the player continue or quit. 
    void PostGameDisplayTrigger() 
    {
        //Enable Post game menu
    }

    //Sets any manager specific data needed for the next level.
    void LevelCleanup()
    {
        //Do Stuff (May or may not need this function)
    }  

    void HazardDisplay()
    {
        //Enable Hazard Display Indicating that the Paper went into a Hazard
    }

    void PenaltyDisplay()
    {
        //Enable Penalty Display Indicating that the Paper went into a Hazard
    }
    

    void LevelLogic()
    {
        if(_goal.IsTriggered) 
        {
            //Player wins, hole is over
            PostGameDisplayTrigger();
            _gameState = GameState.PostGame;
            Debug.Log("Win Display");
            Debug.Log("Shots Taken: " + _shotCount);
            Debug.Log("Distance Traveled: " + _distance);
            Debug.Log("Time Taken: " + _timer);
            Debug.Log("Papers Used: " + _papersUsed);

        }
        
        //Check for any triggered penalty or hazard boxes
        foreach(PenaltyBox penaltyBox in _penaltyBoxes)
        {
            if (penaltyBox == null) { break; }
            if (penaltyBox.IsTriggered) 
            { 
                //Add Penalty Shot
                _shotCount++;
                penaltyBox.turnOffTrigger();
                PenaltyDisplay();
                Debug.Log("Penalty Display");
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
                Debug.Log("Hazard Display");
            }
        }
    }
}

