using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "Inisial Data Gameplay",
    menuName = "Game Kuis/Inisial Data Gameplay")]

public class InitialDataGamePlay : ScriptableObject
{
    public LevelPackKuis levelPack = null;
    public int levelIndex = 0;

    [SerializeField]
    private bool _gameOver = false;

    public bool GameIsOver 
    {
        get => _gameOver;
        set => _gameOver = value;
    }
}
