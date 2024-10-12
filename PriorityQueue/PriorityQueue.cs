using System.Collections.Generic;

namespace PriorityQueue{

    public class PriorityQueueDto<T>{
        public int Priority;
        public T element;
        public PriorityQueueDto(int priority) {
            Priority = priority;
        }
        public PriorityQueueDto(int priority, T elem) {
            Priority = priority;
            element = elem;
        }

        public override string ToString()
        {
            return $"{Priority}-{element.ToString()}";
        }
    }
    public class StructurePriorityQueue<T> {
        private List<PriorityQueueDto<T>> storage = null;
        public StructurePriorityQueue(){
            storage = new List<PriorityQueueDto<T>>();
        }

        public bool IsEmpty() {
            return storage.Count == 0;
        }

        public int Size() {
            return storage.Count;
        }

        public void Clear() {
            storage.Clear();
        }

        public PriorityQueueDto<T> Peek() {
            if (IsEmpty()) return null;
            return storage[0];
        }

        public PriorityQueueDto<T> Get() {
            if (IsEmpty()) return null;
            return RemoveAt(0);
        }

        public void Add(PriorityQueueDto<T> elem) {
            if (elem == null)
                throw new System.Exception("Cannot be null");
            
            storage.Add(elem);
            Climb(Size() -1);
        }

        public bool Contains(PriorityQueueDto<T> elem) {
            foreach(PriorityQueueDto<T> el in storage)
                if(el.Equals(elem))
                    return true;
            return false;
        }

        private bool LessThan(int i, int j) {
            return storage[i].Priority < storage[j].Priority;
        }

        public bool Remove (PriorityQueueDto<T> elem) {
            if (elem == null)   return false;
            for (int i = 0; i<Size(); i++){
                if(storage[i].Equals(elem)){
                    RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        private void Swap(int i, int j){
            var elI = storage[i];
            var elJ = storage[j];

            storage[j] = elI;
            storage[i] = elJ;
        }

        private PriorityQueueDto<T> RemoveAt(int i) {
            if(IsEmpty()) return null;
            var removed = storage[i];
            int lastIndex = Size()-1;
            Swap(i, lastIndex);
            storage.RemoveAt(lastIndex);

            if(i == lastIndex) return removed;

            var elem = storage[i];
            Sink(i);

            if(storage[i].Priority == elem.Priority)
                Climb(i);

            return removed;
        }

        private void Climb(int i) {
            int parent = (i-1) /2;
            while (i>0 && LessThan(i, parent)) {
                Swap(parent, i);
                i = parent;
                parent = (i-1)/2;
            }
        }

        private void Sink(int i) {
            int size = Size();
            while (true) {
                int left = 2 * i +1;
                int right = 2 * i +2;
                int smallest = left;

                if(right < size && LessThan(right, left))
                    smallest = right;

                if(left >= size || LessThan(i, smallest)) break;

                Swap(smallest, i);
                i = smallest;
            }
        }
    }
}