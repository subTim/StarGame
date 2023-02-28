using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;


public class RotationController : MonoBehaviour
{
    [SerializeField, Range(0f, 5f)] private float sensivity;
    [SerializeField, Range(0f,1f)] private float threahold;
    
    [SerializeField] private GraphicRaycaster raycaster;
    [SerializeField] private EventSystem eventSystem;
    
    private Vector2 _previousSwipe;
    private Quaternion _addTo;

    private PointerEventData _pointerData;
    private List<RaycastResult> _raycastResults = new (2);

    public Quaternion AddTo => gameObject.transform.rotation;

    private void Awake()
    {
        _pointerData = new PointerEventData(eventSystem);
    }

    private void Start()
    {
        _addTo = Quaternion.identity;
    }

    private void Update()
    {
        CheackRotation();
    }

    private void AddRotation(Vector2 direction)
    {
         _addTo.y +=  direction.x * sensivity;
         _addTo.x += direction.y * sensivity;
         
         gameObject.transform.rotation = Quaternion.Euler(_addTo.x, _addTo.y, _addTo.z);
    }

    private void CheackRotation()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);

            if (CheackUiRayCast(touch))
            { 
                CalculateDelta(touch);
            }
        }
    }

    private void CalculateDelta(Touch touch)
    {
        var delta = touch.deltaPosition.normalized;

        if (delta.magnitude > _previousSwipe.magnitude + threahold)
        {
            AddRotation(delta - _previousSwipe);
        }
    }

    private bool CheackUiRayCast(Touch touch)
    {
        _pointerData.position = touch.position;
        _raycastResults = new List<RaycastResult>();

        raycaster.Raycast(_pointerData, _raycastResults);
        
        return _raycastResults.Count == 0;
    }
    

    private bool IsCancelled(Touch touch) => touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled;
}