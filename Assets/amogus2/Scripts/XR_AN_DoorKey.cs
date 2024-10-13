using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XR_AN_DoorKey : MonoBehaviour
{
    [Tooltip("True - red key object, false - blue key")]
    public bool isRedKey = true;
    XR_AN_HeroInteractive hero;

    private void Start()
    {
        hero = FindObjectOfType<XR_AN_HeroInteractive>(); // key will get up and it will saved in "inventary"
    }

    public void TryAction()
    {
        if ( Input.GetKeyDown(KeyCode.E) )
        {
            if (isRedKey) hero.RedKey = true;
            else hero.BlueKey = true;
            Destroy(gameObject);
        }
    }
}
