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
        Cursor.lockState = CursorLockMode.Locked; // �mleci ekran�n ortas�na kilitle
        Cursor.visible = false; // �mleci g�r�nmez yap

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
