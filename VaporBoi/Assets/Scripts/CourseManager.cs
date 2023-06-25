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

    GoalBox _goal;
    List<PenaltyBox> _penaltyBoxes = new List<PenaltyBox>();
    List<HazardBox> _hazardBoxes = new List<HazardBox>();

    enum GameState
    {
        PreGame,
        PlayerThrow,
        Flight,
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
    public void IncrementShot(){ _shotCount++; }

    void Start() { LevelSetup(); }

    void LevelSetup()
    {
        _gameState = GameState.PreGame;
        _timer = 0f;

        //Gather all the Special Hitboxes
        _goal = GameObject.FindGameObjectWithTag("Goal").GetComponent<GoalBox>();

        GameObject[] tempGO = GameObject.FindGameObjectsWithTag("Hazard");
        foreach(GameObject go in tempGO) { _hazardBoxes.Add(GetComponent<HazardBox>()); }

        tempGO = GameObject.FindGameObjectsWithTag("Penalty");
        foreach(GameObject go in tempGO) { _penaltyBoxes.Add(GetComponent<PenaltyBox>()); }

    }

    void Update()
    {
        if (_gameState == GameState.PlayerThrow || _gameState == GameState.Flight)
        {
            //Increment Timer
            _timer += Time.deltaTime;

        }
    }


    // Displays the total score of the player for the hole, golf style. Is also  
    // displays a UI that lets the player continue or quit. 
    void PostGameDisplay() 
    {

    }

    //Sets any manager specific data needed for the next level.
    void LevelCleanup()
    {

    }  
    

    void AfterPlayerThrow()
    {
        if(_goal.IsTriggered) 
        {
            PostGameDisplay();
        }

        foreach(PenaltyBox penaltyBox in _penaltyBoxes)
        {
            if (penaltyBox.IsTriggered) 
            { 
                //Do Stuff
            }
        }

        foreach(HazardBox hazardBox in _hazardBoxes)
        {
            if (hazardBox.IsTriggered) 
            { 
                //Do Stuff
            }
        }

    }


}

