using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerPlayer : MonoBehaviour
{
    public bool gameContinue;
    public bool enemyKilled;
    public bool puzzleContinue;
    public Enemy enemy;
    public CameraControl camControl;
    public GameManager gameManagerPuzzle;
    public Camera puzzleCamera;

    public ParticleSystem finishEffect;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked; // Ýmleci ekranýn ortasýna kilitle
        Cursor.visible = false; // Ýmleci görünmez yap

        gameContinue = true;
    }

    public void StartFight()
    {
        enemy.FightStarted();
    }

    public void KillEnemy()
    {
        enemyKilled = true;
        camControl.StartOrbit();
        finishEffect.Play();
    }

    public void StartPuzzle()
    {
        puzzleContinue = true;
        //puzzleCamera.gameObject.SetActive(true);
        SceneManager.LoadScene("Puzzle Scene");
    }

    public void FinishPuzzle()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        puzzleCamera.gameObject.SetActive(false);
        puzzleContinue=false;
    }
}
