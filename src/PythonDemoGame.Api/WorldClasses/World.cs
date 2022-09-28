namespace PythonDemoGame.Api.WorldClasses;

public class World
{
    public  BaseCell[,]    Map        { get; init; }
    public  int            MapSize    { get; init; }
    private List<BaseCell> EmptyCells { get; init; }
    public  bool           IsGameOver { get; private set; }
    public  PythonHeadCell Python     { get; init; }

    public World(int mapSize)
    {
        MapSize = mapSize;
        if (mapSize is < 16 or > 512)
        {
            throw new ArgumentOutOfRangeException(nameof(mapSize));
        }

        EmptyCells = new List<BaseCell>(mapSize * mapSize);

        Map = new BaseCell[mapSize, mapSize];

        for (var x = 0; x < mapSize; x++)
        {
            for (var y = 0; y < mapSize; y++)
            {
                EmptyCells.Add(new EmptyCell(this, x, y));
            }
        }

        Python = new PythonHeadCell(this, MapSize / 2, MapSize / 2);
        EmptyCells.RemoveAt(MapSize / 2);
    }

    public void Initialize()
    {
        CreateNumber();
    }

    private void CreateEmptyCell(int x, int y)
    {
        Map[x, y] = new EmptyCell(this, x, y);
        EmptyCells.Add(Map[x, y]);
    }

    public NumberCell CreateNumber()
    {
        var rnd  = new Random().Next(0, EmptyCells.Count - 1);
        var cell = EmptyCells[rnd];
        EmptyCells.RemoveAt(rnd);

        var number = BaseCell.Create(CellType.Number, this, cell.X, cell.Y) as NumberCell ?? throw new InvalidOperationException();
        return number;
    }

    public IEnumerable<object> GetCells()
    {
        for (var x = 0; x < MapSize; x++)
        {
            for (var y = 0; y < MapSize; y++)
            {
                switch (Map[x, y])
                {
                    case EmptyCell:
                        continue;
                    case NumberCell c:
                        yield return new {type = c.Type(), x, y, c.Number};
                        break;
                    default:
                        yield return new {type = Map[x, y].Type(), x, y};
                        break;
                }
            }
        }
    }

    public void GameOver() => IsGameOver = true;

    public void Update()
    {
        if (!IsGameOver)
        {
            Python.Move();
        }
    }
}