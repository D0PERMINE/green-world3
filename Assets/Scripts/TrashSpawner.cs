using UnityEngine;
using System.Collections;

public class TrashSpawner : MonoBehaviour
{
    public GameObject[] trashPrefabs; // Array von M�ll-Prefabs
    [SerializeField] int numberOfTrashToSpawn = 1; // Anzahl der zu spawnenden M�llobjekte
    public Vector2 spawnAreaMin; // Minimum X und Y Koordinaten des Spawn-Bereichs
    public Vector2 spawnAreaMax; // Maximum X und Y Koordinaten des Spawn-Bereichs

    private bool canSpawn = false; // Steuerung, ob Spawnen erlaubt ist

    private void Update()
    {
        StartSpawning();
    }

    public void StartSpawning()
    {
        if (canSpawn)
        {
            canSpawn = false;
            StartCoroutine(SpawnTrashRoutine());
        }
    }

    IEnumerator SpawnTrashRoutine()
    {
        for (int i = 0; i < numberOfTrashToSpawn; i++)
        {
            SpawnTrash();
            yield return new WaitForSeconds(0.5f); // Kurze Wartezeit zwischen den Spawns
        }
    }

    void SpawnTrash()
    {
        // W�hle ein zuf�lliges M�llobjekt aus dem Array
        int randomIndex = Random.Range(0, trashPrefabs.Length);
        GameObject trashPrefab = trashPrefabs[randomIndex];

        // Bestimme eine zuf�llige Position innerhalb des definierten Bereichs
        float x = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
        float y = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
        Vector2 spawnPosition = new Vector2(x, y);

        // Erzeuge das M�llobjekt an der zuf�lligen Position
        Instantiate(trashPrefab, spawnPosition, Quaternion.identity);
    }

    // Methode zum Aktivieren des Spawnen
    public void EnableSpawning()
    {
        canSpawn = true;
    }

    public int GetNumberOfTrashToSpawn()
    {
        return this.numberOfTrashToSpawn;
    }
}