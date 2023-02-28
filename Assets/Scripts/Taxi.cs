using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taxi : MonoBehaviour
{
    //TODO - add sound effect of taxi driving off
    public void HideTaxi()
    {
        Destroy(gameObject);
    }
}