using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Common.Scripts.UI.Components
{
    public class PointersLogicComponent : MonoBehaviour
    {
        [SerializeField] private Image pointerPrefab;
        [SerializeField] private Transform pointersParent;
        [SerializeField] private Vector2 screenBordersMargin= new Vector2(200,500);

        private readonly Dictionary<Transform, Image> pointers = new Dictionary<Transform, Image>();
        
        private Camera currentCamera;

        private void Awake()
        {
            currentCamera = Camera.main;
        }

        public void AddPointer(Transform target, Color color)
        {
            var newPointer = Instantiate(pointerPrefab, pointersParent);
            newPointer.color = color;
            pointers.Add(target,newPointer);
        }
        
        public void RemovePointer(Transform targetTargetPosition)
        {
            var pointer = pointers[targetTargetPosition];
            pointers.Remove(targetTargetPosition);
            Destroy(pointer.gameObject);
        }
        
        private void LateUpdate()
        {
            RotatePointersToTarget();
            MovePointers();
        }

        private void RotatePointersToTarget()
        {
            foreach (var pair in pointers)
            {
                var target = pair.Key;
                var pointer = pair.Value;
                var targetScreenPos = currentCamera.WorldToScreenPoint(target.transform.position);
            
                if (IsTargetOffScreen(targetScreenPos))
                {
                    Vector2 dir = (targetScreenPos - pointer.transform.position);
                    pointer.transform.rotation = Quaternion.AngleAxis(GetAngleFromVector(dir.normalized)+90,Vector3.forward);
                }
                else
                {
                    pointer.transform.eulerAngles = Vector3.zero;
                }
            }
            
        }

        private void MovePointers()
        {
            foreach (var pair in pointers)
            {
                var target = pair.Key;
                var pointer = pair.Value;
                var targetScreenPos = currentCamera.WorldToScreenPoint(target.transform.position);

                pointer.transform.position = targetScreenPos;
                if (IsTargetOffScreen(targetScreenPos))
                    pointer.transform.position = CapPosition(pointer.transform.position);
            }
        }

        private bool IsTargetOffScreen(Vector2 position)
        {
            return position.x <= screenBordersMargin.x ||
                position.x >= Screen.width - screenBordersMargin.x ||
                position.y <= screenBordersMargin.y ||
                position.y >= Screen.height - screenBordersMargin.y;
        }

        private Vector2 CapPosition(Vector2 position)
        {
            if (position.x <= screenBordersMargin.x) position.x = screenBordersMargin.x;
            if (position.x >= Screen.width - screenBordersMargin.x ) position.x = Screen.width - screenBordersMargin.x ;
            if (position.y <= screenBordersMargin.y) position.y = screenBordersMargin.y;
            if (position.y >= Screen.height - screenBordersMargin.y ) position.y = Screen.height - screenBordersMargin.y ;
            return position;
        }

        private static float GetAngleFromVector(Vector2 dir)
        {
            return Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        }
    }
}