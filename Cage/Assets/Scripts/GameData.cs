using System;
using UnityEngine;

[Serializable]
public class GameData
{
    public Vector3 playerPosition;
    public bool enemy1Alive;
    public bool enemy2Alive;
    public bool swordPicked;
    public bool bookPicked;
    public bool potionPicked;
    public bool doorUpOpened;
    public bool doorLeftOpened;
    public bool doorRightOpened;
}
