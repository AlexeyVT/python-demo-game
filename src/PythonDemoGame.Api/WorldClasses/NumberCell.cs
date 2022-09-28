namespace PythonDemoGame.Api.WorldClasses;

public class NumberCell : BaseCell
{
    public int Number { get; init; }

    public NumberCell(World world, int x, int y) : base(world, x, y)
    {
        var rnd = new Random();
        Number = rnd.Next(1, 9);
    }

    public override CellType Type() => CellType.Number;
}