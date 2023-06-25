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
        PlayerThrow,
        Flight,
        PostGame
    }
    private GameState _gameState = PreGame;

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
        _gameState = PreGame;
        _timer = 0f;
    }

    void Update()
    {
        if (_gameState == PlayerThrow || _gameState == Flight)
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
        if(GoalBox.IsTriggered) 
        {
            PostGameDisplay();
        }
        else if(PenaltyBox.IsTriggered)
        {
            //Add the pentalty to the score
            //Spawn player at the nearest valid location
        }
        else if(HazardBox.IsTriggered)
        {
            //Add the pentalty to the score
            //Player stays in initial location
        }
        else
        {
            //Nothing was Triggered, Spawn Player in front of paper
        }
    }


}

