using System;
using UnityEngine;
[Serializable]
public class PlayerData
{
    [SerializeField] private int currentLevel;
    [SerializeField] private bool musicOn;
    [SerializeField] private bool soundOn;
    [SerializeField] private bool shakeOn;
    public PlayerData(int currentLevel, bool musicOn, bool soundOn, bool shakeOn)
    {
        this.currentLevel = currentLevel;
        this.musicOn = musicOn;
        this.soundOn = soundOn;
        this.shakeOn = shakeOn;
    }

    public PlayerData()
    {
    }
    public int getLevel() { return currentLevel; }
    public bool isMusicOn() { return musicOn; }
    public bool isSoundOn() { return soundOn; }
    public bool isShakeOn() { return shakeOn; }
}
