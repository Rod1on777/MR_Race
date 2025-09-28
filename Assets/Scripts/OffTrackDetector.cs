using UnityEngine;

public class OffTrackDetector : MonoBehaviour
{
    public Transform spawnPoint;
    private bool onTrack = true;

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("OffRaceTrack"))
        {
            onTrack = false;
            ReturnToTrack();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("OffRaceTrack"))
        {
            onTrack = true;
        }
    }
    public void ReturnToTrack()
    {
        Debug.Log(gameObject.name + " respawned");

        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
