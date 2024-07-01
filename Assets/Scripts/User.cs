using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{
    public double level0Time, level1Time, level2Time, level3Time, level4Time;

    //Constructor class for users
    public User(double level0Time, double level1Time, double level2Time, double level3Time, double level4Time){
        this.level0Time = level0Time;
        this.level1Time = level1Time;
        this.level2Time = level2Time;
        this.level3Time = level3Time;
        this.level4Time = level4Time;
    }
}
