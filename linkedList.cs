using LinkedListNamespace;
using Microsoft.VisualBasic.FileIO;
using System.Diagnostics.Tracing;

LinkedList list = new LinkedList();

// Print the list when it is empty
Console.WriteLine(list.ToString());

// Add a value to end of the list
list.AddLast(1);

list.AddLast(2);

list.AddLast(3);

list.AddLast(1);

list.AddAfter(list.FindLast(1),5);

list.AddBefore(list.Find(1), new Node() { value = 7});
Console.WriteLine(list.ToString());

// a value needs to be stored in order for this to work
var n = list.Find(2);
if (n != null)
{
    Console.WriteLine($"{n.value} is in the list.");
}
else
{
    Console.WriteLine("value not found.");
}
//Console.WriteLine(list.ToString());

var c = list.CountValue(1);
Console.WriteLine($"there are this many nodes within the given value {c}");

var a = list.Find(1);
if (a != null)
{
    Console.WriteLine(list.IndexOf(a));
}
else
{
    Console.WriteLine("value not found");
}


namespace LinkedListNamespace
{
    public class Node
    {
        public int value { get; set; }

        public Node? next { get; set; }
    }

    public class LinkedList
    {
        /// <summary>
        /// The first node in the list
        /// </summary>
        private Node? _head;

        /// <summary>
        /// The last node in the list
        /// </summary>
        private Node? _tail;

        /// <summary>
        /// Constructs an empty list
        /// </summary>
        public LinkedList()
        {
            _head = null;
            _tail = null;
        }

        /// <summary>
        /// Adds a new node containing the specified value after the specified existing node in the list
        /// </summary>
        /// <param name="node">The <see cref="Node"/> after which to insert a new <see cref="Node"/> containing value</param>
        /// <param name="value">The value to add to the list</param>
        /// <returns>The new <see cref="Node"/> containing the value</returns>
        public Node? AddAfter(Node? node, int value)
        {
            // let value = 5
            // [Head] -> ... -> [ node ] -> [  7  ] -> ... -> [    ] -> [Tail] -> null
            // [Head] -> ... -> [ node ] -> [  5  ] -> [  7  ] -> ... -> [    ] -> [Tail]-> null
            //                             new node
            Node? newNode = new Node();

            newNode.value = value;
            newNode.next = node.next;
            node.next = newNode;

            if (node == _tail)
            {
                _tail = newNode;
            }
            return newNode;
        }

        /// <summary>
        /// Adds the specified new node before the specified existing node in the list
        /// </summary>
        /// <param name="node">The <see cref="Node"/> before which to insert newNode</param>
        /// <param name="newNode">The new <see cref="Node"/> to add to the list</param>
        /// <exception cref="ArgumentNullException">if <paramref name="node"/> is null or <paramref name="newNode"/> is null</exception>
        /// <exception cref="InvalidOperationException">if <paramref name="node"/> is not in the current list</exception>
        public void AddBefore(Node? node, Node? newNode)
        {
            Node? curr = _head;
            Node? previousNode = null; // if looking for something if not found it is null

            while (curr != null)
            {
                if (curr.next == node)
                {
                    previousNode = curr;
                }
                curr = curr.next;
            }

            if (node == _head)
            {
                // the previous node is null so we have to make it eq to something so you don't get an exception
                previousNode = newNode;
                _head = newNode;
            }

            if (previousNode == null)
            {
                throw new InvalidOperationException("Not found");
            }
            previousNode.next = newNode;
            newNode.next = node;
        }

        /// <summary>
        /// Adds a node containing the specified value at the start of the list
        /// </summary>
        /// <param name="value">The value to add to the start of the liist</param>
        /// <returns>The new <see cref="Node"/> containing the value</returns>
        public Node? AddFirst(int value)
        {
            // We are modifying the beginning of the list -> we have to move the head

            // Create the new node we want to add to the list
            Node? newNode = new Node();
            newNode.value = value;  // assign the value to the new node
            newNode.next = _head; // The new node is going to the beginning of the list.

            // We need to think aobut 3 scenarios
            // 1. When the list is empty
            //      -> _head = null and _tail = null
            // 2. When there is one node in the list
            //      -> there is only one node in the list so _head and _tail are the same
            // 3. When there is more than one node in the list
            //      -> _head is at the beginning of the list and _tail is at the end of the list. _head != _tail
            if (_head == null /* 1. The list is empty*/)
            {
                _head = newNode;
                _tail = newNode;
            }
            else if (_head == _tail /* 2. There is one node in the list*/ )
            {
                _head = newNode;
            }
            else /* 3. The list contains more than one node -> head != tail*/
            {
                _head = newNode;
            }
            return newNode;
        }

        /// <summary>
        /// Removes the first node in the list
        /// </summary>
        public void RemoveFist()
        {
            // We are modifying the beginning of the list -> we have to move the head

            // We need to think aobut 3 scenarios
            // 1. When the list is empty
            //      -> _head = null and _tail = null
            // 2. When there is one node in the list
            //      -> there is only one node in the list so _head and _tail are the same
            // 3. When there is more than one node in the list
            //      -> _head is at the beginning of the list and _tail is at the end of the list. _head != _tail
            if (_head == null /* 1. The list is empty*/)
            {
                return;
            }
            else if (_head == _tail /* 2. There is one node in the list*/ )
            {
                _head = null;
                _tail = null;
            }
            else /* 3. The list contains more than one node -> head != tail*/
            {
                // Moving the head to the next node
                _head = _head.next;
            }
        }

        /// <summary>
        /// Adds a node containing the specified value at the end of the list
        /// </summary>
        /// <param name="value">The value to add at the end of the list</param>
        /// <returns>The new <see cref="Node"/> containing the value</returns>
        public Node? AddLast(int value)
        {
            // Modifying the end of the list -> we have to move the tail

            // Create the new node we want to add to the list
            Node? newNode = new Node();
            newNode.value = value;  // assign the value to the new node
            newNode.next = null; // The new node is going to the end of the list. The last node should always point to null.

            // We need to think aobut 3 scenarios
            // 1. When the list is empty
            //      -> _head = null and _tail = null
            // 2. When there is one node in the list
            //      -> there is only one node in the list so _head and _tail are the same
            // 3. When there is more than one node in the list
            //      -> _head is at the beginning of the list and _tail is at the end of the list. _head != _tail

            if (_head == null /* 1. The list is empty*/)
            {
                _head = newNode;
                _tail = newNode;
            }
            else if (_head == _tail /* 2. There is one node in the list*/ )
            {
                _tail.next = newNode; // Linking the new node to the list.
                _tail = newNode; // The tail is always supposed to be at the end of the list.
            }
            else /* 3. The list contains more than one node -> head != tail*/
            {
                _tail.next = newNode; // Linking the new node to the list
                _tail = newNode; // The tail is always supposed to be at the end of the list.
            }

            return newNode;
        }

        /// <summary>
        /// Removes the last node in the list
        /// </summary>
        public void RemoveLast()
        {
            // Modifying the end of the list -> we have to move the tail

            // We need to think aobut 3 scenarios
            // 1. When the list is empty
            //      -> _head = null and _tail = null
            // 2. When there is one node in the list
            //      -> there is only one node in the list so _head and _tail are the same
            // 3. When there is more than one node in the list
            //      -> _head is at the beginning of the list and _tail is at the end of the list. _head != _tail
            if (_head == null /* 1. The list is empty*/)
            {
                return;
            }
            else if (_head == _tail /* 2. There is one node in the list*/ )
            {
                _head = null;
                _tail = null;
            }
            else /* 3. The list contains more than one node -> head != tail*/
            {
                // We need to find the new tail -> the node before the tail
                //
                //  [Head] -> [    ] -> [    ] -> ... -> [    ] -> [Tail] -> null
                //                                         ^
                Node? curr = _head; // We should never move the head when looping through a list. We will move curr, curr starts where the head is
                while (curr.next != _tail) // Loop though the list until we find the node that is "pointing" at the tail.
                {
                    curr = curr.next;
                }
                // The curr node is the node that is pointing at the tail
                //  [Head] -> [    ] -> [    ] -> ... -> [ curr ] -> [Tail] -> null
                _tail = curr;
                //  [Head] -> [    ] -> [    ] -> ... -> [ Tail ] -> [old tail] -> null
                _tail.next = null;
                //  [Head] -> [    ] -> [    ] -> ... -> [ Tail ] -> null
            }
        }

        /// <summary>
        /// Finds the first node that contains the specified value.
        /// </summary>
        /// <param name="value">The value to locate in the list</param>
        /// <returns>The <see cref="Node"/> containing the value if found, otherwise null</returns>
        public Node? Find(int value)
        {
            Node? curr = _head; // current node starts at the head
            while (curr != null && curr.value != value) // check for null before accessing what's in the while loop
            {
                curr = curr.next;
            }
            return curr;
        }

        /// <summary>
        /// Finds the last node that contains the specified value.
        /// </summary>
        /// <param name="value">The value to locate in the list</param>
        /// <returns>The last <see cref="Node"/> containing the value if found, otherwise null</returns>
        public Node? FindLast(int value)
        {
            Node? curr = _head;
            Node? lastNode = null;
            while (curr != null)
            {
                if (curr.value == value)
                {
                    lastNode = curr;
                }
                curr = curr.next;
            }
            return lastNode;
        }

        /// <summary>
        /// Returns the index of the given node in the list
        /// </summary>
        /// <param name="node"></param>
        /// <returns>The index of the given node if it is in the list, otherwise -1</returns>
        public int IndexOf(Node? node)
        {
            int index = 0;
            Node? curr = _head;
            while(curr != null)
            {
                if(curr == node)
                {
                    return index;
                }
                curr = curr.next;
                index++;
            }
            return -1;
        }

        /// <summary>
        /// Counts how many nodes are in the linked list with the given value 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int CountValue(int value)
        {
            Node? curr = _head;
            int count = 0;

            while (curr != null)
            {
                if (curr.value == value)
                {
                    count++;
                }
                curr = curr.next;
            }
            return count;
        }

        /// <summary>
        /// Returns a string that contains all the values in the list, seperated by a comma
        /// </summary>
        /// <returns>A string containing all the values in the list</returns>
        public string ToString()
        {
            // We need to think aobut 3 scenarios
            // 1. When the list is empty
            //      -> _head = null and _tail = null
            // 2. When there is one node in the list
            //      -> there is only one node in the list so _head and _tail are the same
            // 3. When there is more than one node in the list
            //      -> _head is at the beginning of the list and _tail is at the end of the list. _head != _tail

            if (_head == null /* 1. The list is empty*/ )
            {
                // Return an empty string
                return "";
            }
            if (_head == _tail /* 2. The list contains one node */)
            {
                // Return the value in that node as a string
                return _head.value.ToString();
            }

            /* 3. There are more than one node in the list */
            string str = "";
            // We will use currentNode to itereate through the list.
            // currentNode will be the node that should be "printed" at each time.
            Node? currentNode = _head; // currentNode starts at the head since the head contains the first element to be "printed"
            while (currentNode != _tail)
            {
                // Append the value in the current node to the string
                str = str + currentNode.value.ToString() + ", ";

                // Move the current node to the next node
                currentNode = currentNode.next;
            }

            // Append the value in the tail to the end.
            // We don't add a comma after the value in the tail because we don't want a "trailing comma" at the end.
            str = str + _tail.value.ToString();

            return str;
        }
    }
}
