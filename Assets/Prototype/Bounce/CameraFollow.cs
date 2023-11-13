using System;
using UnityEngine;

namespace Prototype{
	public class CameraFollow : MonoBehaviour{
		[SerializeField] private Transform target;
		[SerializeField] private bool ignoreX = true;


		private void FixedUpdate(){
			var position = transform.position;
			if(ignoreX){
				var targetOffset = Vector3.Slerp(position, new Vector3(position.x, target.position.y, position.z),
					Time.fixedDeltaTime);
				transform.position = targetOffset;
			}
			else{
				var targetPosition = target.position;
				var targetOffset = Vector3.Lerp(position, new Vector3(targetPosition.x, targetPosition.y, position.z),
					Time.fixedDeltaTime * 5);
				transform.position = targetOffset;
			}

		}
	}
}