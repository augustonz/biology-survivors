using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWhenAnimationFinish : MonoBehaviour
{
    public float delay = 0f;

	// Use this for initialization
	void Start () {
		Destroy (gameObject, GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay); 
	}
}
