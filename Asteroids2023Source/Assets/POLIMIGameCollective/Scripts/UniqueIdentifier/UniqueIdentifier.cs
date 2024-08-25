using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace POLIMIGameCollective
{
    public class UniqueIdentifier
    {
        public static string Identifier()
        {
            if (SystemInfo.deviceUniqueIdentifier != SystemInfo.unsupportedIdentifier)
            {
                return SystemInfo.deviceUniqueIdentifier;
            }
            else
            {
                string DeviceID = PlayerPrefs.GetString("DeviceID", "null");
                if (DeviceID == "null")
                {
                    DeviceID = Utility.RandomString(20);
                    PlayerPrefs.SetString("DeviceId", DeviceID);
                }

                return DeviceID;
            }
        }
    }
}
