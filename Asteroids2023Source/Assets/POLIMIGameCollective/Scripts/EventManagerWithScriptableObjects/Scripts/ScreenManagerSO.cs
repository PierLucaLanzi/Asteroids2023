using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScreenManagerSO : MonoBehaviour
{
    [Header("Subscribed Events")]
    [SerializeField] private VoidEventChannelSO explodeEvent;
    [SerializeField] private VoidEventChannelSO runawayEvent;
    [SerializeField] private VoidEventChannelSO spawnEvent;
    
    [Space(10)]
    [Header("Images to Highlight")]
    [SerializeField] private Image explosionImage;
    [SerializeField] private Image runawayImage;
    [SerializeField] private Image spawnImage;
    
    // Start is called before the first frame update
    private void OnEnable()
    {
        // EventManager.StartListening ("Spawn", Spawn);
        // EventManager.StartListening ("RunAway", RunAway);
        // EventManager.StartListening ("Explode", Explode);
        spawnEvent.OnEventRaised += Spawn;
        runawayEvent.OnEventRaised += RunAway;
        explodeEvent.OnEventRaised += Explode;
    }

    private void Spawn()
    {
        // EventManager.StopListening("Spawn", Spawn);
        StartCoroutine(SpawnCoroutine());
    }
    private void Explode()
    {
        // EventManager.StopListening("Explode", Explode);
        StartCoroutine(ExplodeCoroutine());
    }
    private void RunAway()
    {
        // EventManager.StopListening("RunAway", RunAway);
        StartCoroutine(RunAwayCoroutine());
    }
    
    private IEnumerator SpawnCoroutine()
    {
        spawnEvent.OnEventRaised -= Spawn;
        
        spawnImage.color = Color.green;
        yield return new WaitForSeconds(3f);
        spawnImage.color = Color.red;
        
        spawnEvent.OnEventRaised += Spawn;
        yield return null;
    }
    private IEnumerator ExplodeCoroutine()
    {
        explodeEvent.OnEventRaised -= Explode;

        explosionImage.color = Color.green;
        yield return new WaitForSeconds(3f);
        explosionImage.color = Color.red;
        
        explodeEvent.OnEventRaised += Explode;
        yield return null;
    }
    private IEnumerator RunAwayCoroutine()
    {
        runawayEvent.OnEventRaised -= RunAway;

        runawayImage.color = Color.green;
        yield return new WaitForSeconds(3f);
        runawayImage.color = Color.red;

        runawayEvent.OnEventRaised += RunAway;
        yield return null;
    }
}
