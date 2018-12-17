using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : System.IComparable<Score>
{
    public string name;
    public double time;
    //public Score(string n, int s)
    //{
    //    name = n;
    //    score = s;
    //}
    public int CompareTo(Score other)
    {
        if (other == null)
            return 0;
        int value = (int)other.time - (int)this.time;
        return value;
    }
}
