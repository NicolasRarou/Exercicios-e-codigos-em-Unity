using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject ballPrefab;
    public GameObject playerPrefab;

    public Text scoreText;
    public Text ballsText;
    public Text levelText;

    public GameObject panelMenu;
    public GameObject panelPlay;
    public GameObject panelLevelCompleted;
    public GameObject panelGameOver;

    public GameObject[] levels;

    public static GameManager instance { get; private set; }

    public enum State { MENU,INIT,PlAY,LEVELCOMPLETED,LOADlEVEL,GAMEOVER}
    State _state;
    GameObject _currentball;
    GameObject _currentlevel;
    bool _isSwitchingState;

    private int _score;
    public int Score
    {
        get { return _score; }
        set { _score = value;
            scoreText.text = "Score!!! " + _score;
            }
    }

    private int _level;
    public int Level
    {
        get { return _level; }
        set { _level = value; }
    }

    private int _balls;

    public int Balls
    {
        get { return _balls; }
        set { _balls = value; }
    }



    public void Playclicked()
    {
        SwitchState(State.INIT);
    }

    void Start()
    {
        instance = this;
        SwitchState(State.MENU);
    }

    public void SwitchState(State newState, float delay = 0)
    {
        StartCoroutine(SwitchDelay(newState, delay));
    }
    IEnumerator SwitchDelay(State newState, float delay)
    {
        _isSwitchingState = true;
        yield return new WaitForSeconds(delay);
        EndState();
        _state = newState;
        BeginState(newState);
        _isSwitchingState = false;
    }
    void BeginState(State newState)
    {
        switch (newState)
        {
            case State.MENU:
                panelMenu.SetActive(true);
                break;
            case State.INIT:
                panelPlay.SetActive(true);
                Score = 0;
                Level = 0;
                Balls = 3;
                Instantiate(playerPrefab);
                SwitchState(State.LOADlEVEL);
                break;
            case State.PlAY:
                break;
            case State.LEVELCOMPLETED:
                panelLevelCompleted.SetActive(true);
                break;
            case State.LOADlEVEL:
                if(Level >= levels.Length)
                {
                    SwitchState(State.GAMEOVER);
                }
                else
                {
                    _currentlevel = Instantiate(levels[Level]);
                    SwitchState(State.PlAY);
                 }
                break;
            case State.GAMEOVER:
                panelGameOver.SetActive(true);
                break;
        }
    }
    void Update()
    {
        switch (_state)
        {
            case State.MENU:
                break;
            case State.INIT:
                break;
            case State.PlAY:
                if (_currentball == null)
                {
                    if (Balls > 0)
                    {
                       _currentball = Instantiate(ballPrefab);
                    }
                    else
                    {
                        SwitchState(State.GAMEOVER);
                    }
                }
                break;
            case State.LEVELCOMPLETED:
                break;
            case State.LOADlEVEL:
                break;
            case State.GAMEOVER:
                break;
        }
    }
    void EndState()
    {
        switch (_state)
        {
            case State.MENU:
                panelMenu.SetActive(false);
                break;
            case State.INIT:
                break;
            case State.PlAY:
                break;
            case State.LEVELCOMPLETED:
                break;
            case State.LOADlEVEL:
                break;
            case State.GAMEOVER:
                panelPlay.SetActive(false);
                break;
        }
    }

}
