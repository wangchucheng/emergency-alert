using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveScene3
{
    public double playerPositionX;
    public double playerPositionZ;
    public double playerRotationW;
    public double playerRotationY;

    public List<double> enemyPositionX = new List<double>();
    public List<double> enemyPositionZ = new List<double>();
    public List<double> enemyRotationW = new List<double>();
    public List<double> enemyRotationY = new List<double>();

    public double bossPositionX;
    public double bossPositionZ;
    public double bossRotationW;
    public double bossRotationY;

    public double playerHp;
    public double bossHp;
    public int totalCount;
    public double time;
}
