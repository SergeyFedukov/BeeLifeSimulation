using UnityEngine;

public class Map
{
    private static Map instance;
    private Vector3 _startPosition;
    private Cell[,] _hiveCells;
    private Cell[,] _plantCells;
    private int _hiveCellsWidth;
    private int _hiveCellsHeight;
    private int _plantCellsWidth;
    private int _plantCellsHeight;

    private Map(Vector3 startPosition, int hiveCellsWidth, int hiveCellsHeight, int plantCellsWidth, int plantCellsHeight)
    {
        _startPosition = startPosition;
        _hiveCellsWidth = hiveCellsWidth;
        _hiveCellsHeight = hiveCellsHeight;
        _plantCellsWidth = plantCellsWidth;
        _plantCellsHeight = plantCellsHeight;
        _hiveCells = new Cell[_hiveCellsWidth, hiveCellsHeight];
        _plantCells = new Cell[_plantCellsWidth, plantCellsHeight];
    }

    public static Map GetInstance(Vector3 startPosition, int hiveCellsWidth, int hiveCellsHeight, int plantCellsWidth, int plantCellsHeight)
    {
        if (instance == null)
            instance = new Map(startPosition, hiveCellsWidth, hiveCellsHeight, plantCellsWidth, plantCellsHeight);
        return instance;
    }

    public bool TryCreate(Cell cellPrefub, float hiveCellSize)
    {
        if (_hiveCells[0, 0] != null || _plantCells[0, 0] != null)
            return false;
        float plantCellSize = hiveCellSize / 2;
        CreateCells(cellPrefub, _startPosition, _hiveCells, hiveCellSize);

        Vector3 hiveCellPosition = _hiveCells[_hiveCellsWidth - 1, 0].transform.position;
        Vector3 hiveCellScale = _hiveCells[_hiveCellsWidth - 1, 0].transform.localScale;
        Vector3 plantCellScale = Vector3.one * plantCellSize;
        Vector3 plantCellsPosition = new Vector3(hiveCellPosition.x - plantCellScale.x * 5, hiveCellPosition.y, hiveCellPosition.z - (hiveCellScale.z + plantCellScale.z) * 5);

        CreateCells(cellPrefub, plantCellsPosition, _plantCells, plantCellSize);
        AddAdjacentCells(_hiveCells);
        AddAdjacentCells(_plantCells);
        return true;
    }

    public void Destroy()
    {
        DestroyCells(_hiveCells);
        DestroyCells(_plantCells);
        _hiveCells = null;
        _plantCells = null;
        instance = null;
        _hiveCellsWidth = 0;
        _hiveCellsHeight = 0;
        _plantCellsWidth = 0;
        _plantCellsHeight = 0;
    }

    public void PlacePlantInCell(Plant plant)
    {
        System.Random random1 = new System.Random();
        System.Random random2 = new System.Random();
        int firstIndex, secondIndex;
        bool isTook = true;

        while (isTook)
        {
            firstIndex = random1.Next(_plantCellsWidth);
            secondIndex = random2.Next(_plantCellsHeight);

            if (_plantCells[firstIndex, secondIndex].TryTakeStateObject(plant))
                isTook = false;
        }
    }

    public void PlaceHiveInCell(Hive hive)
    {
        _hiveCells[0, 0].TryTakeStateObject(hive);
    }

    private void CreateCells(Cell cellPrefub, Vector3 startPosition, Cell[,] cells, float cellSize)
    {
        cellPrefub.transform.localScale = Vector3.one * cellSize;
        cellPrefub.transform.position = startPosition;
        int width = cells.GetUpperBound(0) + 1;
        int height = cells.Length / width;

        for (int i = 0; i < width; i++)
        {
            if (i > 0)
                cellPrefub.transform.position += Vector3.back * cellPrefub.transform.localScale.z * 10;

            for (int j = 0; j < height; j++)
            {
                if (j > 0)
                    cellPrefub.transform.position += Vector3.right * cellPrefub.transform.localScale.x * 10;

                Cell cell = Object.Instantiate(cellPrefub, cellPrefub.transform.position, Quaternion.identity);
                cells[i, j] = cell;
            }

            cellPrefub.transform.position = new Vector3(startPosition.x, startPosition.y, cellPrefub.transform.position.z);
        }
    }

    private void AddAdjacentCells(Cell[,] cells)
    {
        int width = cells.GetUpperBound(0) + 1;
        int height = cells.Length / width;

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Cell topCell = null;
                Cell bottomCell = null;
                Cell leftCell = null;
                Cell rightCell = null;

                if (width > 1 && i != width - 1)
                    bottomCell = cells[i + 1, j];
                if (i != 0)
                    topCell = cells[i - 1, j];
                if (j != height - 1)
                    rightCell = cells[i, j + 1];
                if (j != 0)
                    leftCell = cells[i, j - 1];

                cells[i, j].TryAddAdjacentCells(topCell, bottomCell, leftCell, rightCell);
            }
        }
    }

    private void DestroyCells(Cell[,] cells)
    {
        int width = cells.GetUpperBound(0) + 1;
        int height = cells.Length / width;

        foreach (var cell in cells)
        {
            IStateObject stateObject = cell.StateObject;

            if (stateObject != null)
            {
                if (stateObject is Plant)
                    Object.Destroy(((Plant)stateObject).gameObject);
                else
                    Object.Destroy(((Hive)stateObject).gameObject);
            }

            Object.Destroy(cell.gameObject);
        }
    }
}