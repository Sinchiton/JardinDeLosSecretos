using UnityEngine;
using System.Collections.Generic;

public class ProceduralColumns : MonoBehaviour
{
    public GameObject columnPrefab; // Prefab de la columna
    public Transform leftBoundary; // Límite izquierdo del movimiento
    public Transform rightBoundary; // Límite derecho del movimiento

    public int maxColumns = 20; // Número máximo de columnas visibles
    public float columnSpeed = 2f; // Velocidad de movimiento de las columnas
    public int minColumnsInGroup = 2; // Mínimo número de columnas en un grupo
    public int maxColumnsInGroup = 3; // Máximo número de columnas en un grupo
    public float minGapBetweenGroups = 0.5f; // Mínima distancia entre grupos
    public float maxGapBetweenGroups = 1.5f; // Máxima distancia entre grupos

    private List<GameObject> columns = new List<GameObject>(); // Lista de columnas activas
    private float nextSpawnX; // Posición X de la siguiente columna
    private float fixedYPosition; // Posición fija en "Y"

    void Start()
    {
        fixedYPosition = columnPrefab.transform.position.y; // Usar la posición inicial "Y" del prefab
        nextSpawnX = leftBoundary.position.x;

        // Crear columnas iniciales dentro de los límites
        for (int i = 0; i < maxColumns; i++)
        {
            SpawnColumnGroup();
        }
    }

    void Update()
    {
        // Mover columnas existentes
        foreach (GameObject column in columns)
        {
            column.transform.position += Vector3.left * columnSpeed * Time.deltaTime;

            // Reutilizar columna si sale del límite izquierdo
            if (column.transform.position.x < leftBoundary.position.x)
            {
                column.SetActive(false); // Desactivar temporalmente
            }
        }

        // Reutilizar columnas desactivadas para nuevos grupos
        if (columns.FindAll(c => !c.activeSelf).Count >= maxColumnsInGroup)
        {
            SpawnColumnGroup();
        }
    }

    void SpawnColumnGroup()
    {
        // Generar un grupo de columnas
        int numberOfColumns = Random.Range(minColumnsInGroup, maxColumnsInGroup + 1);
        for (int i = 0; i < numberOfColumns; i++)
        {
            GameObject column = GetInactiveColumn();
            if (column != null)
            {
                float spawnX = nextSpawnX + (i * columnPrefab.transform.localScale.x);
                column.transform.position = new Vector3(spawnX, fixedYPosition, 0);
                column.SetActive(true); // Reactivar columna
            }
        }

        // Actualizar la posición para el próximo grupo
        nextSpawnX += numberOfColumns * columnPrefab.transform.localScale.x;

        // Controlar el espacio entre los grupos
        float gap = Random.Range(minGapBetweenGroups, maxGapBetweenGroups);
        nextSpawnX += gap;

        // Si es el último grupo, agregar una columna extra si queda un hueco
        GameObject lastColumn = columns.FindLast(c => c.activeSelf);
        if (lastColumn.transform.position.x < rightBoundary.position.x - 1f)
        {
            GameObject extraColumn = GetInactiveColumn();
            if (extraColumn != null)
            {
                extraColumn.transform.position = new Vector3(lastColumn.transform.position.x + columnPrefab.transform.localScale.x, fixedYPosition, 0);
                extraColumn.SetActive(true);
            }
        }
    }

    GameObject GetInactiveColumn()
    {
        // Obtener una columna desactivada para reutilizar
        foreach (GameObject column in columns)
        {
            if (!column.activeSelf)
            {
                return column;
            }
        }

        // Si no hay columnas inactivas, crear una nueva
        GameObject newColumn = Instantiate(columnPrefab);
        columns.Add(newColumn);
        return newColumn;
    }
}
