using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Objective Outcome", menuName = "Dialogue/Outcomes/UpdateBannerL2")]
public class UpdateBannerL2 : DialogueOutcome
{
    private ChangeBannerLevel2 manager;
    public override void DoOutcome(ref NPC n)
    {
        GameObject.Find("BannerManager").GetComponent<ChangeBannerLevel2>().UpdateBanner();
    }

}
