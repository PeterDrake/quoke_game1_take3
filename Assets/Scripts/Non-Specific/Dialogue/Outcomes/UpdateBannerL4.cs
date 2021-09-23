using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Objective Outcome", menuName = "Dialogue/Outcomes/UpdateBannerL1")]
public class UpdateBannerL4 : DialogueOutcome
{
    private ChangeBannerLevel1 manager;
    public override void DoOutcome(ref NPC n)
    {
        GameObject.Find("BannerManager").GetComponent<ChangeBannerLevel1>().UpdateBanner();
    }

}
