using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce.Cameras
{
    public class SpecificTargetCameraZone : MonoBehaviour
    {
        private static List<SpecificTargetCameraZone> _activeZones = new List<SpecificTargetCameraZone>();
        private static SpecificTargetCameraZone _currentZone;

        private static void UpdateCurrentZone(SpecificTargetCameraZone zone)
        {
            if (zone != null)
                zone._camera.SetDistantTarget(zone._focusPoint);
            else
                _currentZone._camera.ResetDistantTarget();
            _currentZone = zone;
        }

        [SerializeField]
        private int _priority = 0;
        [SerializeField]
        private Transform _focusPoint;
        [SerializeField]
        private AutoframeCameraController _camera;

        private bool _isActive;

        private void OnTriggerEnter(Collider other)
        {
            if (_isActive || other.transform.GetComponentInParent<IZilla>() == null)
                return;
            _isActive = true;

            _activeZones.Add(this);
            if (_currentZone == null ||
                _priority > _currentZone._priority)
                UpdateCurrentZone(this);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!_isActive || other.transform.GetComponentInParent<IZilla>() == null)
                return;
            _isActive = false;

            _activeZones.Remove(this);
            if (_currentZone == this)
            {
                SpecificTargetCameraZone newZone = null;
                foreach (var zone in _activeZones)
                    if (newZone == null || zone._priority > newZone._priority)
                        newZone = zone;
                UpdateCurrentZone(newZone);
            }
        }
    }
}
