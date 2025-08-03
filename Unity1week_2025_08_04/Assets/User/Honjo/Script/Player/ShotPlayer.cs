using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Honjo
{
    public class ShotPlayer : MonoBehaviour
    {
        public float powerMultiplier = 10f;
        public float maxPower = 20f;
        public LineRenderer lineRenderer;

        private Vector3 dragStartWorld;
        private bool isDragging = false;
        private Rigidbody rb;
        private Camera cam;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            cam = Camera.main;
        }

        void OnMouseDown()
        {
            if (rb.velocity.magnitude > 0.1f) return;

            isDragging = true;
            dragStartWorld = GetMouseWorldPoint();
        }

        void OnMouseUp()
        {
            if (!isDragging) return;

            isDragging = false;

            Vector3 dragEndWorld = GetMouseWorldPoint();
            Vector3 forceDirection = dragStartWorld - dragEndWorld;
            Vector3 force = Vector3.ClampMagnitude(forceDirection * powerMultiplier, maxPower);

            rb.AddForce(force, ForceMode.Impulse);

            if (lineRenderer) lineRenderer.enabled = false;
        }

        void Update()
        {
            if (isDragging && lineRenderer)
            {
                lineRenderer.enabled = true;
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, dragStartWorld);
            }

            // Debug用RayをSceneビューに表示
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);
        }

        Vector3 GetMouseWorldPoint()
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.up, Vector3.zero); // Y=0の平面
            plane.Raycast(ray, out float enter);
            return ray.GetPoint(enter);
        }
    }
}

