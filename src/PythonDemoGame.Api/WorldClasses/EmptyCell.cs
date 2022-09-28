namespace PythonDemoGame.Api.WorldClasses;

public class EmptyCell : BaseCell
{
    public EmptyCell(World world, int x, int y) : base(world, x, y) { }
    public override CellType Type() => CellType.Empty;
}