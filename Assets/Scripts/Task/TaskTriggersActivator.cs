using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskTriggersActivator : MonoBehaviour
{
    [SerializeField]
    private Collider[] _triggers; //triggers colliders

    public void EnableTriggers()
    {
        foreach (var trigger in _triggers)
        {
            trigger.enabled = true;
        }
    }
    public void DisableTriggers()
    {
        foreach (var trigger in _triggers)
        {
            trigger.enabled = false;
        }
    }
    public IEnumerator DisableTriggersWithDelay(float delay) // this one needed for disable with delay, if we just disable triggers the task window will stay at the screen
    {
        yield return new WaitForSeconds(delay);
        DisableTriggers();
    }

}
