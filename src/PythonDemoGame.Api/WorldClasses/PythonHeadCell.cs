using System.ComponentModel;
using System.Runtime.InteropServices.ComTypes;

namespace PythonDemoGame.Api.WorldClasses;

public class PythonHeadCell : PythonCell
{
    public LinkedList<PythonBodyCell> Body      { get; private set; } = new();
    public Direction                  Direction { get; private set; } = Direction.Up;
    public int                        Grow      { get; private set; } = 1;

    public PythonHeadCell(World world, int x, int y) : base(world, x, y) { }

    public void SetDirection(Direction direction) => Direction = direction;

    public void Move()
    {
        var x = X;
        var y = Y;

        switch (Direction)
        {
            case Direction.Up:
                Y--;
                if (Y < 0)
                {
                    World.GameOver();
                    return;
                }

                break;
            case Direction.Down:
                Y++;
                if (Y > World.MapSize - 1)
                {
                    World.GameOver();
                    return;
                }

                break;
            case Direction.Left:
                X--;
                if (X < 0)
                {
                    World.GameOver();
                    return;
                }

                break;
            case Direction.Right:
                X++;
                if (X > World.MapSize - 1)
                {
                    World.GameOver();
                    return;
                }

                break;
            default:
                throw new InvalidEnumArgumentException();
        }

        switch (World.Map[X, Y])
        {
            case NumberCell c:
                Grow += c.Number;
                World.CreateNumber();
                break;
            case PythonCell:
                World.GameOver();
                return;
            case EmptyCell:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        SetPosition(X, Y);

        if (Grow > 0)
        {
            Grow--;
            Body.AddFirst(new PythonBodyCell(World, x, y));
        }
        else
        {
            var tail = Body.Last();
            Body.RemoveLast();
            Body.AddFirst(tail);
            _ = new EmptyCell(World, tail.X, tail.Y);

            tail.SetPosition(x, y);
        }
    }

    public override CellType Type() => CellType.PythonHead;
}