using System;
using UnityEngine;
[Serializable]
public class PlayerData
{
    [SerializeField] private int currentLevel;
    [SerializeField] private bool musicOn;
    [SerializeField] private bool soundOn;
    [SerializeField] private bool shakeOn;
    [SerializeField] private int coin;
    [SerializeField] private int gem;
    public PlayerData(int currentLevel, bool musicOn, bool soundOn, bool shakeOn, int coin, int gem)
    {
        this.currentLevel = currentLevel;
        this.musicOn = musicOn;
        this.soundOn = soundOn;
        this.shakeOn = shakeOn;
        this.coin = coin;
        this.gem = gem;
    }

    public PlayerData()
    {
    }
    public int getLevel() { return currentLevel; }
    public bool isMusicOn() { return musicOn; }
    public bool isSoundOn() { return soundOn; }
    public bool isShakeOn() { return shakeOn; }
    public int getCoin() { return coin; }
    public int getGem() { return gem; }
    public void setLevel(int level) { currentLevel = level; }
    public void setMusicOn(bool musicOn) { this.musicOn = musicOn; }
    public void setSoundOn(bool soundOn) { this.soundOn = soundOn; }
    public void setShakeOn(bool shakeOn) { this.shakeOn = shakeOn; }
    public void setCoin(int coin) { this.coin = coin; }
    public void setGem(int gem) { this.gem = gem; }
}
