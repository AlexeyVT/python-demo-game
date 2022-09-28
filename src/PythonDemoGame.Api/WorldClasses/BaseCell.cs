using System.ComponentModel;

namespace PythonDemoGame.Api.WorldClasses;

public abstract class BaseCell
{
    public int   X     { get; protected set; }
    public int   Y     { get; protected set; }
    public World World { get; init; }

    protected BaseCell(World world, int x, int y)
    {
        World = world;
        X     = x;
        Y     = y;

        world.Map[x, y] = this;
    }

    public abstract CellType Type();

    public static BaseCell? Create(CellType type, World world, int x, int y)
    {
        var cell = world.Map[x, y];
        if (cell is not EmptyCell) return null;

        return type switch
               {
                   CellType.Empty      => new EmptyCell(world, x, y),
                   CellType.Number     => new NumberCell(world, x, y),
                   CellType.PythonHead => new PythonHeadCell(world, x, y),
                   CellType.PythonBody => new PythonBodyCell(world, x, y),
                   _                   => throw new InvalidEnumArgumentException()
               };
    }
}