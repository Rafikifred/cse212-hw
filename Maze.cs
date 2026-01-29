public class Maze
{
    private readonly Dictionary<ValueTuple<int, int>, bool[]> _mazeMap;
    private int _currX = 1;
    private int _currY = 1;

    public Maze(Dictionary<ValueTuple<int, int>, bool[]> mazeMap)
    {
        _mazeMap = mazeMap;
    }

    public void MoveLeft()
    {
        var moves = _mazeMap[(_currX, _currY)];
        if (!moves[0])
            throw new InvalidOperationException("Can't go that way!");

        _currX--;
    }

    public void MoveRight()
    {
        var moves = _mazeMap[(_currX, _currY)];
        if (!moves[1])
            throw new InvalidOperationException("Can't go that way!");

        _currX++;
    }

    public void MoveUp()
    {
        var moves = _mazeMap[(_currX, _currY)];
        if (!moves[2])
            throw new InvalidOperationException("Can't go that way!");

        _currY--;
    }

    public void MoveDown()
    {
        var moves = _mazeMap[(_currX, _currY)];
        if (!moves[3])
            throw new InvalidOperationException("Can't go that way!");

        _currY++;
    }

    public string GetStatus()
    {
        return $"Current location (x={_currX}, y={_currY})";
    }
}
