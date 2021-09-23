using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Objective Outcome", menuName = "Dialogue/Outcomes/UpdateBannerL3")]
public class UpdateBannerL3 : DialogueOutcome
{
    private ChangeBannerLevel3 manager;
    public override void DoOutcome(ref NPC n)
    {
        GameObject.Find("BannerManager").GetComponent<ChangeBannerLevel3>().UpdateBanner();
    }

}
