using System.ComponentModel;

namespace PythonDemoGame.Api.WorldClasses;

public class PythonBodyCell : PythonCell
{
    public PythonBodyCell(World world, int x, int y) : base(world, x, y) { }

    public override CellType Type() => CellType.PythonBody;
}