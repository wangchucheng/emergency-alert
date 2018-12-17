using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveScene2
{
    public double playerPositionX;
    public double playerPositionZ;
    public double playerRotationW;
    public double playerRotationY;

    public List<double> enemyPositionX = new List<double>();
    public List<double> enemyPositionZ = new List<double>();
    public List<double> enemyRotationW = new List<double>();
    public List<double> enemyRotationY = new List<double>();

    public double playerHp;
    public double enemyHeartHp1;
    public double enemyHeartHp2;
    public double enemyHeartHp3;
    public double remainingTime;
    public int awareCount;
}
