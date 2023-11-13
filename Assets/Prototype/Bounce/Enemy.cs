using System;
using UnityEngine;

namespace Prototype{
	public class Enemy : MonoBehaviour{
		
		private void OnCollisionEnter2D(Collision2D col){
			Destroy(gameObject);
		}
	}
}