using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskTriggersActivator : MonoBehaviour
{
    [SerializeField]
    private Collider[] _triggers;

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
    public IEnumerator DisableTriggersWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
    }

}
