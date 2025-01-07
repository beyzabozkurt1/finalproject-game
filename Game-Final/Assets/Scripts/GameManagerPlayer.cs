using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerPlayer : MonoBehaviour
{
    public bool gameContinue;
    public bool enemyKilled;
    public Enemy enemy;
    public CameraControl camControl;

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
}
