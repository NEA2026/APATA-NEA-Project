namespace APATA_NEA_Project.Classes;

internal class BinaryMinHeapPriorityQueue
{
    internal class HeapNode(Cell cell, int distance)
    {
        public Cell Cell = cell;
        public int Distance = distance;
    }

    private readonly List<HeapNode> minHeap = new();

    private readonly Dictionary<Cell, int> indexMap = new();

    public int Count => minHeap.Count;

    public bool Contains(Cell cell) => indexMap.ContainsKey(cell);

    public void Insert(Cell cell, int distance)
    {
        HeapNode heapNode = new(cell, distance);
        minHeap.Add(heapNode);

        int index = minHeap.Count - 1;
        indexMap[cell] = index;

        SiftUp(index);
    }

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

    public void DecreaseKey(Cell cell, int shorterDistance)
    {
        int index = indexMap[cell];
        minHeap[index].Distance = shorterDistance;

        SiftUp(index);
    }

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