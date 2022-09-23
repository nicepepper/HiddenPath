namespace Map.Pathfinding
{
    public class PathNode
    {
        public Point Position { get; set; }
        public int PathLengthFromStart { get; set; }
        public PathNode CameFrom { get; set; }
        public int HeuristicEstimatePathLength { get; set; }
        public int EstimateFullPathLength => this.PathLengthFromStart + this.HeuristicEstimatePathLength;
    }
}

