using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBannerLevel2 : MonoBehaviour
{
    public InformationCanvas _canvas;
    public void UpdateBanner()
    {
        _canvas.ChangeText("Build a two bucket sanitation system");
    }
}
