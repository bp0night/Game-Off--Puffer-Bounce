using System;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype.Topdown{
	public class ShootingScale : MonoBehaviour{
		[SerializeField] private List<GameObject> scales;
		[SerializeField] private GameObject target;


		private GameObject GetCloseScale(){
			float minDot = 0;
			GameObject closeScale = null;
			foreach(var scale in scales){
				var dot = Vector3.Dot(scale.transform.up, target.transform.position - scale.transform.position);
				if(dot < minDot) continue;
				closeScale = scale;
				minDot = dot;
			}
			return closeScale;
		}

		private void OnDrawGizmos(){
			if(!target) return;
			Gizmos.color = Color.green;
			Gizmos.DrawLine(transform.position, target.transform.position);
			Gizmos.color = Color.red;
			var closeScale = GetCloseScale();
			Gizmos.DrawLine(closeScale.transform.position , target.transform.position);
		}
	}
}