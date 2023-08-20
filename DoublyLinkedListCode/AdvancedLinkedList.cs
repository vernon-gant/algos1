using AlgorithmsDataStructures;

namespace DoublyLinkedListCode
{
    public class AdvancedLinkedList
    {
        private readonly Node dummy;

        public AdvancedLinkedList()
        {
            dummy = new Node(0);
            dummy.next = dummy;
            dummy.prev = dummy;
        }

        public void AddInTail(Node _item)
        {
            _item.prev = dummy.prev;
            _item.next = dummy;
            dummy.prev.next = _item;
            dummy.prev = _item;
        }

        public bool RemoveNode(int value)
        {
            Node toDelete;
            for (toDelete = dummy.next; toDelete != dummy && toDelete.value != value; toDelete = toDelete.next){}

            if (toDelete == dummy) return false;

            toDelete.prev.next = toDelete.next;
            toDelete.next.prev = toDelete.prev;
            return true;
        }

        public void InsertAfter(Node beforeInsert, Node toInsert)
        {
            if (beforeInsert == null)
            {
                toInsert.next = dummy.next;
                toInsert.prev = dummy;
                dummy.next.prev = toInsert;
                dummy.next = toInsert;
            }
            else
            {
                toInsert.next = beforeInsert.next;
                toInsert.prev = beforeInsert;
                beforeInsert.next.prev = toInsert;
                beforeInsert.next = toInsert;
            }
        }
    }

}