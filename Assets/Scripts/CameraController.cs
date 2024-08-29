using UnityEngine;
using Cinemachine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera mainCamera; // Die Hauptkamera, die dem Charakter folgt
    [SerializeField] private CinemachineVirtualCamera doorCamera; // Die Kamera, die auf die T�r zoomt
    [SerializeField] private CinemachineVirtualCamera npcDialogCamera; // Die Kamera, die auf NPC Dialog zoomt
    [SerializeField] private CinemachineVirtualCamera wideCamera; // Die Kamera, raus zoomt
    [SerializeField] private float zoomDuration = 3f; // Dauer des Zooms
    [SerializeField] private NPCDialogue npcDialogue;
    [SerializeField] private DoorController doorController;

    public void TriggerDoorZoomIn()
    {
        // Erh�he die Priorit�t der Zoom-Kamera
        doorCamera.Priority = mainCamera.Priority + 1;
    }

    public void TriggerDoorZoomOut()
    {
        // Setze die Priorit�t der Hauptkamera zur�ck
        doorCamera.Priority = mainCamera.Priority - 1;
    }

    public void TriggerNPCDialigZoom()
    {
        // Erh�he die Priorit�t der Zoom-Kamera
        npcDialogCamera.Priority = mainCamera.Priority + 1;

        // Warte bis Dialog fertig ist
        if (!npcDialogue.IsDialogueActive)
        {
            // Setze die Priorit�t der Hauptkamera zur�ck
            npcDialogCamera.Priority = mainCamera.Priority - 1;
        }

    }

    public void ZoomOutCamera()
    {
        // Erh�he die Priorit�t der Zoom-Kamera
        wideCamera.Priority = mainCamera.Priority + 1;
    }

}

