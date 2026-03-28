namespace APATA_NEA_Project.Classes;

/// <summary>
/// Encapsulates a binary min-heap implementation of a priority queue. 
/// The priority queue is indexed using a dictionary which maps each cell with its index in the priority queue, 
/// necessary for an efficient O(log n) decrease key worst-case complexity. 
/// Insertions and extractions also have an O(log n) worst-case complexity. 
/// These three operations are needed in both Dijkstra’s algorithm and the A* Search algorithm.
/// </summary>
internal class BinaryMinHeapPriorityQueue
{
    /// <summary>
    /// A nested class that stores a key-value pair, 
    /// with the key being a Cell and the value being an integer which represents an associated distance (as used in pathfinding). 
    /// The class is nested inside the Binary Min-Heap Priority Queue class as a form of encapsulation, 
    /// as only the Binary Min-Heap Priority Queue class needs access to it.
    /// </summary>
    internal class HeapNode(Cell cell, int distance)
    {
        public Cell Cell = cell;
        public int Distance = distance;
    }

    /// <summary>
    ///  A list that stores all the heap nodes in the binary min-heap priority queue.
    /// </summary>
    private readonly List<HeapNode> minHeap = new();

    /// <summary>
    /// A dictionary that maps each Cell in the binary min-heap priority queue with its index in the binary min-heap priority queue.
    /// </summary>
    private readonly Dictionary<Cell, int> indexMap = new();

    /// <summary>
    /// An expression bodied property that returns the number of heap nodes in the binary min-heap priority queue.
    /// </summary>
    public int Count => minHeap.Count;

    /// <summary>
    /// An expression bodied method that returns true if the binary min-heap priority queue contains the cell being passed and false if it does not contain that cell.
    /// </summary>
    public bool Contains(Cell cell) => indexMap.ContainsKey(cell);

    /// <summary>
    /// Adds a cell to the priority queue with an associated distance as the priority (as used in pathfinding).
    /// </summary>
    public void Insert(Cell cell, int distance)
    {
        HeapNode heapNode = new(cell, distance);
        minHeap.Add(heapNode);

        int index = minHeap.Count - 1;
        indexMap[cell] = index;

        SiftUp(index);
    }

    /// <summary>
    /// Removes the cell with the shortest distance (lowest priority) from the priority queue and returns it.
    /// </summary>
    public Cell ExtractMin()
    {
        if (minHeap.Count == 0)
        {
            throw new ArgumentOutOfRangeException("The priority queue is empty!");
        }

        Cell min = minHeap[0].Cell;
        int lastIndex = minHeap.Count - 1;

        indexMap.Remove(min);

        if (minHeap.Count == 1)
        {
            minHeap.RemoveAt(0);
            return min;
        }

        minHeap[0] = minHeap[lastIndex];
        indexMap[minHeap[0].Cell] = 0;

        minHeap.RemoveAt(lastIndex);

        SiftDown(0);
        return min;
    }

    /// <summary>
    /// Decreases the associated distance (priority) with a cell to a shorter distance.
    /// </summary>
    public void DecreaseKey(Cell cell, int shorterDistance)
    {
        int index = indexMap[cell];
        minHeap[index].Distance = shorterDistance;

        SiftUp(index);
    }

    /// <summary>
    /// Moves a node up in the binary tree as much as needed, to restore the heap condition after an insertion or decrease key.
    /// </summary>
    private void SiftUp(int index)
    {
        while (index > 0)
        {
            int parentIndex = (index - 1) / 2;

            if (minHeap[index].Distance < minHeap[parentIndex].Distance)
            {
                (minHeap[index], minHeap[parentIndex]) = (minHeap[parentIndex], minHeap[index]);
                indexMap[minHeap[index].Cell] = index;
                indexMap[minHeap[parentIndex].Cell] = parentIndex;
                index = parentIndex;
            }

            else
            {
                break;
            }
        }
    }

    /// <summary>
    /// Moves a node down in the binary tree as much as needed, to restore the heap condition after an extraction.
    /// </summary>
    private void SiftDown(int index)
    {
        int leftChildIndex = 2 * index + 1;
        int rightChildIndex = 2 * index + 2;
        int smallestChildIndex = index;

        if (leftChildIndex < minHeap.Count && minHeap[leftChildIndex].Distance < minHeap[smallestChildIndex].Distance)
        {
            smallestChildIndex = leftChildIndex;
        }

        if (rightChildIndex < minHeap.Count && minHeap[rightChildIndex].Distance < minHeap[smallestChildIndex].Distance)
        {
            smallestChildIndex = rightChildIndex;
        }

        if (smallestChildIndex != index)
        {
            (minHeap[index], minHeap[smallestChildIndex]) = (minHeap[smallestChildIndex], minHeap[index]);
            indexMap[minHeap[index].Cell] = index;
            indexMap[minHeap[smallestChildIndex].Cell] = smallestChildIndex;
            SiftDown(smallestChildIndex);
        }
    }
}