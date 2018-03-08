using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{

    private int tankId;

    private int lifeValue;
    private String type;
    private float maxSpeed;
    
    private bool isShielded;
    private bool isFrozen;

    public Tank(int id, String type)
    {
        this.tankId = id;
        this.type = type;

        if (this.type == "a")
        {
            this.maxSpeed = 1;
            this.lifeValue = 100;
        }
        else if (this.type == "b")
        {
            this.maxSpeed = 1.2f;
            this.lifeValue = 90;
        }
        else
        {
            this.maxSpeed = 0.8f;
            this.lifeValue = 120;
        }
    }


    public float MaxSpeed
    {
        get { return maxSpeed; }
    }

    public string Type
    {
        get { return type; }
    }

    public int TankId
    {
        get { return tankId; }
    }

    public int LifeValue
    {
        get { return lifeValue; }
        set { lifeValue = value; }
    }

    public bool IsShielded
    {
        get { return isShielded; }
        set { isShielded = value; }
    }

    public bool IsFrozen
    {
        get { return isFrozen; }
        set { isFrozen = value; }
    }
    

}
