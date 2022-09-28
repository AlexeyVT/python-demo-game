namespace PythonDemoGame.Api.WorldClasses;

public abstract class PythonCell : BaseCell
{
    protected PythonCell(World world, int x, int y) : base(world, x, y) { }

    public virtual void SetPosition(int x, int y)
    {
        X = x;
        Y = y;

        World.Map[x, y] = this;
    }
}