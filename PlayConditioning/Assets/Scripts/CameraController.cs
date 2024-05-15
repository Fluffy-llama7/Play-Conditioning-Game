using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Obscura
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private GameObject Target;
        private Camera managedCamera;

        private void Awake()
        {
            managedCamera = gameObject.GetComponent<Camera>();
        }

        void LateUpdate()
        {
            var targetPosition = this.Target.transform.position;
            var cameraPosition = managedCamera.transform.position;

            // Camera is always centered on Target
            cameraPosition = new Vector3(targetPosition.x, targetPosition.y, cameraPosition.z);

            managedCamera.transform.position = cameraPosition; 
        }
    }
}