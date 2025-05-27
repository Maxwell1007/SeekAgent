using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefabToSpawn;
    [SerializeField] private Camera cam;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Instantiate(prefabToSpawn, hit.point, Quaternion.Euler(90f, 0f, 0f));
            }
        }
    }
}