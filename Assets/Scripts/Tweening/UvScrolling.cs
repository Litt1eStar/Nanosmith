using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UvScrolling : MonoBehaviour {
	public Vector2 scrollVector  = new Vector2(0,0.5F);


	Renderer _rend;
	void Start () {
		_rend = this.GetComponent<Renderer> ();
	}
	

	void Update () {
        if(_rend.enabled)
        {
            
			_rend.material.SetTextureOffset ("_MainTex",scrollVector * Time.time);
        }
	}
}
