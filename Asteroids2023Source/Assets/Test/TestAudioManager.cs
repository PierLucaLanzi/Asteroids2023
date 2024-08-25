using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAudioManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AudioManager.Instance.StartBackgroundMusic();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AudioManager.Instance.StopBackgroundMusic();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            AudioManager.Instance.ShiftToDramaticBackgroundMusic();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            AudioManager.Instance.PlayExtraSpaceship();
        }
    }
}
