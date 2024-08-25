using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipExplosion : MonoBehaviour
{
    // SpaceShipExplosion is empty since it is used 
    // just to instantiate the explosion. The delayed
    // destruction is implemented by the class DelayedDisable
    // since the explosions are managed using an object pool
    
    // // https://www.youtube.com/watch?v=rcyQ4XdHdGw
    // [SerializeField] private float _ExplosionDuration = .5f;
    //
    // // Start is called before the first frame update
    // void Start()
    // {
    //     Destroy(gameObject,_ExplosionDuration);
    // }
}
