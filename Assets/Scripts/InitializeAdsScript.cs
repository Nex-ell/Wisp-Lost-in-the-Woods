using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
public class InitializeAdsScript : MonoBehaviour
{

    string GameID = "3590318";
    bool testMode = true;
    // Start is called before the first frame update
    void Start()
    {
        Advertisement.Initialize(GameID, testMode);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
