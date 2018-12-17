using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveScene1
{
    public double playerPositionX;
    public double playerPositionZ;
    public double playerRotationW;
    public double playerRotationY;

    public List<double> helperPositionX = new List<double>();
    public List<double> helperPositionZ = new List<double>();
    public List<double> helperRotationW = new List<double>();
    public List<double> helperRotationY = new List<double>();

    public List<double> enemyPositionX = new List<double>();
    public List<double> enemyPositionZ = new List<double>();
    public List<double> enemyRotationW = new List<double>();
    public List<double> enemyRotationY = new List<double>();

    public double playerHp;
    public double heartHp;
    public double remainingTime;
}
