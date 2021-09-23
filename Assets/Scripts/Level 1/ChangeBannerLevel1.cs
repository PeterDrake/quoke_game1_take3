using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBannerLevel1 : MonoBehaviour
{
    public InformationCanvas _canvas;

    public void UpdateBanner()
    {
        _canvas.ChangeText("Shut off the gas");
    }
}
