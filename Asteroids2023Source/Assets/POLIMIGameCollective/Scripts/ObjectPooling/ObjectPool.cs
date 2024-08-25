/*
 * @Author: David Crook
 *
 * Use the object pools to help reduce object instantiation time and performance
 * with objects that are frequently created and used.
 *
 *
 */
using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

/// <summary>
/// The object pool is a list of already instantiated game objects of the same type.
/// </summary>
public class ObjectPool
{
	//the list of objects.
	private List<GameObject> pooledObjects;

	//sample of the actual object to store.
	//used if we need to grow the list.
	private GameObject pooledObj;

	//maximum number of objects to have in the list.
	private int maxPoolSize;

	//initial and default number of objects to have in the list.
	private int initialPoolSize;

	/// <summary>
	/// Constructor for creating a new Object Pool.
	/// </summary>
	/// <param name="obj">Game Object for this pool</param>
	/// <param name="initialPoolSize">Initial and default size of the pool.</param>
	/// <param name="maxPoolSize">Maximum number of objects this pool can contain.</param>
	/// <param name="shouldShrink">Should this pool shrink back to the initial size.</param>
	public ObjectPool(GameObject obj, int initialPoolSize, int maxPoolSize, bool shouldShrink = false)
	{
		// Check if an empty object with obj.name exist
		GameObject parent = GameObject.Find(obj.name);

		if (parent == null) {
			parent = new GameObject ();
			parent.name = obj.name;
			parent.transform.position = Vector3.zero;
			parent.transform.rotation = Quaternion.identity;
			GameObject.DontDestroyOnLoad (parent);
		}

		//instantiate a new list of game objects to store our pooled objects in.
		pooledObjects = new List<GameObject>();

		//create and add an object based on initial size.
		for (int i = 0; i < initialPoolSize; i++)
		{
			//instantiate and create a game object with useless attributes.
			//these should be reset anyways.
			GameObject nObj = GameObject.Instantiate(obj, Vector3.zero, Quaternion.identity) as GameObject;

			nObj.transform.parent = parent.transform;

			//make sure the object isn't active.
			nObj.SetActive(false);

			//add the object too our list.
			pooledObjects.Add(nObj);

			//Don't destroy on load, so
			//we can manage centrally.
			//GameObject.DontDestroyOnLoad(nObj);
		}

		//store our other variables that are useful.
		this.maxPoolSize = maxPoolSize;
		this.pooledObj = obj;
		this.initialPoolSize = initialPoolSize;

		//are we supposed to shrink?
		if(shouldShrink)
		{
			//listen to the game state manager's event for all pools should shrink
			//back to their initial size.
			//GameStateManager.Instance.ShrinkPools += new GameStateManager.GameEvent(this.Shrink);
		}
	}

	/// <summary>
	/// Returns an active object from the object pool without resetting any of its values.
	/// You will need to set its values and set it inactive again when you are done with it.
	/// </summary>
	/// <returns>Game Object of requested type if it is available, otherwise null.</returns>
	public GameObject GetObject()
	{
		//iterate through all pooled objects.
		for (int i = 0; i < pooledObjects.Count; i++)
		{
			//look for the first one that is inactive.
			if (pooledObjects[i].activeSelf == false)
			{
				//set the object to active.
				pooledObjects[i].SetActive(true);
				//return the object we found.
				return pooledObjects[i];
			}
		}
		//if we make it this far, we obviously didn't find an inactive object.
		//so we need to see if we can grow beyond our current count.
		if (this.maxPoolSize > this.pooledObjects.Count)
		{
			//Instantiate a new object.
			GameObject nObj = GameObject.Instantiate(pooledObj, Vector3.zero, Quaternion.identity) as GameObject;
			//set it to active since we are about to use it.
			nObj.SetActive(true);
			//add it to the pool of objects
			pooledObjects.Add(nObj);
			//return the object to the requestor.
			return nObj;
		}
		//if we made it this far obviously we didn't have any inactive objects
		//we also were unable to grow, so return null as we can't return an object.
		return null;
	}

	/// <summary>
	/// Iterate through the pool and releases as many objects as
	/// possible until the pool size is back to the initial default size.
	/// </summary>
	/// <param name="sender">Who initiated this event?</param>
	/// <param name="eventArgs">The arguments for this event.</param>
//	public void Shrink(object sender, GameEventArgs eventArgs)
//	{
//		//how many objects are we trying to remove here?
//		int objectsToRemoveCount = pooledObjects.Count - initialPoolSize;
//		//Are there any objects we need to remove?
//		if (objectsToRemoveCount <= 0)
//		{
//			//cool lets get out of here.
//			return;
//		}
//
//		//iterate through our list and remove some objects
//		//we do reverse iteration so as we remove objects from
//		//the list the shifting of objects does not affect our index
//		//Also notice the offset of 1 to account for zero indexing
//		//and i >= 0 to ensure we reach the first object in the list.
//		for (int i = pooledObjects.Count - 1; i >= 0; i--)
//		{
//			//Is this object active?
//			if (!pooledObjects[i].activeSelf)
//			{
//				//Guess not, lets grab it.
//				GameObject obj = pooledObjects[i];
//				//and kill it from the list.
//				pooledObjects.Remove(obj);
//			}
//		}
//	}

}
