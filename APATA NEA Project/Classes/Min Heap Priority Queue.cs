namespace APATA_NEA_Project.Classes;

internal class MinHeapPriorityQueue
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
        while (index * 2 + 1 < minHeap.Count)
        {
            int leftChildIndex = index * 2 + 1;
            int rightChildIndex = index * 2 + 2;
            int smallestChildIndex;

            if (rightChildIndex < minHeap.Count && minHeap[rightChildIndex].Distance < minHeap[leftChildIndex].Distance)
            {
                smallestChildIndex = rightChildIndex;
            }

            else
            {
                smallestChildIndex = leftChildIndex;
            }

            if (minHeap[index].Distance > minHeap[smallestChildIndex].Distance)
            {
                (minHeap[smallestChildIndex], minHeap[index]) = (minHeap[index], minHeap[smallestChildIndex]);
                indexMap[minHeap[index].Cell] = index;
                indexMap[minHeap[smallestChildIndex].Cell] = smallestChildIndex;
                index = smallestChildIndex;
            }

            else
            {
                break;
            }
        }
    }
}