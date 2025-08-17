namespace DefaultNamespace
{
    public struct DirectionIndexes
    {
        public DirectionIndexes(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; private set; }
        public int Y { get; private set; }
    }
}