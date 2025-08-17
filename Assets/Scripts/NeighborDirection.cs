namespace DefaultNamespace
{
    public enum NeighborDirection
    {
        TopLeft,
        TopMiddle,
        TopRight,
        MiddleRight,
        BottomRight,
        BottomMiddle,
        BottomLeft,
        MiddleLeft,
        size
    }

    public static class NeighborDirectionExtensions
    {
        public static readonly DirectionIndexes[] IndexValues = new DirectionIndexes[]
        {
            new DirectionIndexes( 1, -1 ),
            new DirectionIndexes( 1, 0 ),
            new DirectionIndexes( 1, 1 ),
            new DirectionIndexes( 0, 1 ),
            new DirectionIndexes( -1, 1 ),
            new DirectionIndexes( -1, 0 ),
            new DirectionIndexes( -1, -1 ),
            new DirectionIndexes( 0, -1 )
        };
    }
}