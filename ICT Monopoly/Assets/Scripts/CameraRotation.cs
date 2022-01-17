using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
        public GameObject Stone;
        private Vector3 offset;
        public float offsetDistance = 2f;
        public float rotateSpeed = 3.0F;
        public Vector3 positionForCamera;
        
        void Start()
        {
                offset = transform.position - Stone.transform.position;
        }
        
        void Update()
        { 
                positionForCamera = Stone.transform.position - Stone.transform.forward * offsetDistance;
        }
        
        void LateUpdate ()
        {
                //set camera position
                transform.position = positionForCamera;
                //set camera rotation
                transform.rotation = Quaternion.LookRotation(Stone.transform.position - transform.position, Vector3.up);
                transform.position = Stone.transform.position + offset;
                transform.Rotate(0, Input.GetAxis("Horizontal"), 0);
        }
}


