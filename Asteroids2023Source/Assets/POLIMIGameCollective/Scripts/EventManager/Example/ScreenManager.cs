using System.Collections;
using UnityEngine;
using POLIMIGameCollective;
using TMPro;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour
{
    [SerializeField] private Image explosionImage;
    [SerializeField] private Image runawayImage;
    [SerializeField] private Image spawnImage;
    
    // Start is called before the first frame update
    private void OnEnable()
    {
        EventManager.StartListening ("Spawn", Spawn);
        EventManager.StartListening ("RunAway", RunAway);
        EventManager.StartListening ("Explode", Explode);
    }

    private void Spawn()
    {
        EventManager.StopListening("Spawn", Spawn);
        StartCoroutine(SpawnCoroutine());
        
    }
    private void Explode()
    {
        EventManager.StopListening("Explode", Explode);
        StartCoroutine(ExplodeCoroutine());
    }
    private void RunAway()
    {
        EventManager.StopListening("RunAway", RunAway);
        StartCoroutine(RunAwayCoroutine());
    }
    
    private IEnumerator SpawnCoroutine()
    {
        spawnImage.color = Color.green;
        yield return new WaitForSeconds(3f);
        spawnImage.color = Color.red;
        EventManager.StartListening ("Spawn", Spawn);
    }
    private IEnumerator ExplodeCoroutine()
    {
        explosionImage.color = Color.green;
        yield return new WaitForSeconds(3f);
        explosionImage.color = Color.red;
        EventManager.StartListening ("Explode", Explode);
    }
    private IEnumerator RunAwayCoroutine()
    {
        runawayImage.color = Color.green;
        yield return new WaitForSeconds(3f);
        runawayImage.color = Color.red;
        EventManager.StartListening ("RunAway", RunAway);        
    }
    
}
