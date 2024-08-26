using System.Collections.Generic;
using UnityEngine;

public class PointerSpawner : MonoBehaviour {
    private Dictionary<Pointer, GameObject> _pointersAndTargets = new Dictionary<Pointer, GameObject>();

    [SerializeField] private GameObject pointerPrefab;
    [SerializeField] private Transform pointerCanvas;

    public void TrySpawnPointer(GameObject target, Color color) {
        bool alreadyPointingAtObject = _pointersAndTargets.ContainsValue(target);
        bool shouldAddPointer = !alreadyPointingAtObject;
        if (shouldAddPointer) {
            GameObject newPointerGo = Instantiate(pointerPrefab, pointerCanvas.transform);
            Pointer newPointer = newPointerGo.GetComponent<Pointer>();
            newPointer.SetColor(color);
            newPointer.target = target.transform;
            _pointersAndTargets.Add(newPointer, target);
        }
    }

    public void RemovePointer(Pointer pointer) {
        _pointersAndTargets.Remove(pointer);
        Destroy(pointer.target.gameObject);
        Destroy(pointer.gameObject);
    }

    public Pointer GetPointerForTargetIfItExists(GameObject target) {
        Pointer pointer = null;

        foreach (var pointerAndTarget in _pointersAndTargets) {
            if (pointerAndTarget.Value == target) {
                pointer = pointerAndTarget.Key;
                break;
            }
        }

        return pointer;
    }
}