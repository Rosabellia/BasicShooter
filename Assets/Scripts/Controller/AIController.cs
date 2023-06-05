using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : Controller
{
    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        MakeDesisions();
        base.Update();
    }

    public void MakeDesisions()
    {
        Debug.Log("I'm thinking...");
    }
}
