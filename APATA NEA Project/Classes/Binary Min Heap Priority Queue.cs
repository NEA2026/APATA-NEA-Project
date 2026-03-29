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

        // Append the heap node to the priority queue.
        int index = minHeap.Count - 1;
        indexMap[cell] = index;

        // SiftUp the appended heap node to restore the heap condition.
        SiftUp(index);
    }

    /// <summary>
    /// Removes the cell with the shortest distance (lowest priority) from the priority queue and returns it.
    /// </summary>
    public Cell ExtractMin()
    {
        if (minHeap.Count == 0)
        {
            // This should never happen in my program. 
            // This error handling is a good example of defensive programming.
            throw new ArgumentOutOfRangeException("The priority queue is empty!");
        }

        // In a binary min-heap the first heap node is always the minimum,
        // in my program this corresponds to the cell with the shorest distance from the maze's start.
        Cell min = minHeap[0].Cell;
        int lastIndex = minHeap.Count - 1;

        indexMap.Remove(min);

        // If the priority queue only contains one heap node, remove the heap node and return it, as this heap node must be the minimum.
        if (minHeap.Count == 1)
        {
            minHeap.RemoveAt(0);
            return min;
        }

        // Replace the removed heap node (min) with the heap node at the end of the priority queue.
        minHeap[0] = minHeap[lastIndex];

        // Update the indexMap to record the changes in indexes.
        indexMap[minHeap[0].Cell] = 0;

        minHeap.RemoveAt(lastIndex);

        // SiftDown the prepended heap node to restore the heap condition.
        SiftDown(0);
        return min;
    }

    /// <summary>
    /// Decreases the associated distance (priority) with a cell to a shorter distance.
    /// </summary>
    public void DecreaseKey(Cell cell, int shorterDistance)
    {
        // Update the cell's distance to the shorterDistance that has been found.
        int index = indexMap[cell];
        minHeap[index].Distance = shorterDistance;

        // SiftUp the heap node at index to restore the heap condition.
        SiftUp(index);
    }

    /// <summary>
    /// Moves a node up in the binary tree as much as needed, to restore the heap condition after an insertion or decrease key.
    /// </summary>
    private void SiftUp(int index)
    {
        while (index > 0)
        {
            // In a binary min-heap the parent of any heap node can always be calculated using the formula (index - 1) / 2.
            int parentIndex = (index - 1) / 2;

            // If the distance of a heap node is less than its parents, the heaps condition needs to be restored.
            if (minHeap[index].Distance < minHeap[parentIndex].Distance)
            {
                // Swap the heap node with its parent.
                (minHeap[index], minHeap[parentIndex]) = (minHeap[parentIndex], minHeap[index]);

                // Update the indexMap to record the changes in indexes.
                indexMap[minHeap[index].Cell] = index;
                indexMap[minHeap[parentIndex].Cell] = parentIndex;

                // The new heap node to be processed is the parent of the heap node that has just been processed.
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
        // In a binary min-heap the left child of any heap node can always be calculated using the formula 2 * index + 1.
        int leftChildIndex = 2 * index + 1;

        // In a binary min-heap the right child of any heap node can always be calculated using the formula 2 * index + 2.
        int rightChildIndex = 2 * index + 2;

        int smallestChildIndex = index;

        // Runs if the smallest child is the heap nodes left child.
        if (leftChildIndex < minHeap.Count && minHeap[leftChildIndex].Distance < minHeap[smallestChildIndex].Distance)
        {
            smallestChildIndex = leftChildIndex;
        }

        // Runs if the smallest child is the heap nodes right child.
        if (rightChildIndex < minHeap.Count && minHeap[rightChildIndex].Distance < minHeap[smallestChildIndex].Distance)
        {
            smallestChildIndex = rightChildIndex;
        }

        if (smallestChildIndex != index)
        {
            // Swap the heap node with its smallest child.
            (minHeap[index], minHeap[smallestChildIndex]) = (minHeap[smallestChildIndex], minHeap[index]);

            // Update the indexMap to record the changes in indexes.
            indexMap[minHeap[index].Cell] = index;
            indexMap[minHeap[smallestChildIndex].Cell] = smallestChildIndex;

            // Recursively SiftDown the smallest child to restore the heap condition.
            SiftDown(smallestChildIndex);
        }
    }
}