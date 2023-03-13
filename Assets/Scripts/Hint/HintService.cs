using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintService : MonoBehaviour
{
    public List<HintBehaviour> Hints = new List<HintBehaviour>();
    public void UpdateAnimations()
    {
        foreach (var item in Hints)
        {
            item.RestartAnimation();
        }
    }
    public void OnHintDestroyed(HintBehaviour hint)
    {
        Hints.Remove(hint);
    }
}
