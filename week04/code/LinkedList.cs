class Node:
    """Represents a node in a doubly linked list."""
    def __init__(self, data: int):
        self.data = data
        self.next: 'Node | None' = None
        self.prev: 'Node | None' = None

class LinkedList:
    """
    A doubly linked list implementation.
    The class implements the iterable protocol for forward iteration.
    """
    def __init__(self):
        self._head: 'Node | None' = None
        self._tail: 'Node | None' = None

    # --- Core Methods (C# Translation) ---

    def insert_head(self, value: int):
        """Insert a new node at the front (i.e. the head) of the linked list."""
        new_node = Node(value)
        if self._head is None:
            # If the list is empty, then point both head and tail to the new node.
            self._head = new_node
            self._tail = new_node
        else:
            # If the list is not empty, then only head will be affected.
            new_node.next = self._head    # Connect new node to the previous head
            self._head.prev = new_node    # Connect the previous head to the new node
            self._head = new_node         # Update the head to point to the new node

    def remove_head(self):
        """Remove the first node (i.e. the head) of the linked list."""
        if self._head is None:
            return # List is empty

        if self._head == self._tail:
            # If the list has only one item in it, then set head and tail to None.
            self._head = None
            self._tail = None
        else:
            # If the list has more than one item in it, then only the head will be affected.
            self._head = self._head.next
            if self._head is not None:
                self._head.prev = None # Disconnect the second node from the first node

    def insert_after(self, value: int, new_value: int):
        """Insert 'newValue' after the first occurrence of 'value' in the linked list."""
        curr = self._head
        while curr is not None:
            if curr.data == value:
                # If the location of 'value' is at the end of the list,
                # then we can call insert_tail to add 'new_value'
                if curr == self._tail:
                    self.insert_tail(new_value)
                # For any other location of 'value', need to create a 
                # new node and reconnect the links to insert.
                else:
                    new_node = Node(new_value)
                    new_node.prev = curr              # Connect new node to the node containing 'value'
                    new_node.next = curr.next         # Connect new node to the node after 'value'
                    curr.next.prev = new_node         # Connect node after 'value' to the new node
                    curr.next = new_node              # Connect the node containing 'value' to the new node
                
                return # We can exit the function after we insert

            curr = curr.next # Go to the next node to search for 'value'

    # --- Problem 1: Insert Tail ---

    def insert_tail(self, value: int):
        """Insert a new node at the back (i.e. the tail) of the linked list."""
        new_node = Node(value)
        if self._tail is None:
            # List is empty
            self._head = new_node
            self._tail = new_node
        else:
            # List is not empty
            new_node.prev = self._tail
            self._tail.next = new_node
            self._tail = new_node

    # --- Problem 2: Remove Tail ---

    def remove_tail(self):
        """Remove the last node (i.e. the tail) of the linked list."""
        if self._tail is None:
            return # List is empty, nothing to do
        
        if self._head == self._tail:
            # Only one node
            self._head = None
            self._tail = None
        else:
            # More than one node
            self._tail = self._tail.prev
            if self._tail is not None:
                self._tail.next = None

    # --- Problem 3: Remove ---

    def remove(self, value: int):
        """Remove the first node that contains 'value'."""
        curr = self._head
        while curr is not None:
            if curr.data == value:
                if curr == self._head:
                    self.remove_head()
                elif curr == self._tail:
                    self.remove_tail()
                else:
                    # Node is in the middle
                    curr.prev.next = curr.next
                    curr.next.prev = curr.prev
                
                return # Found and removed the first instance

            curr = curr.next

    # --- Problem 4: Replace ---

    def replace(self, old_value: int, new_value: int):
        """Search for all instances of 'oldValue' and replace the value to 'newValue'."""
        curr = self._head
        while curr is not None:
            if curr.data == old_value:
                curr.data = new_value
            curr = curr.next

    # --- Problem 5: Reversed Iterator ---

    def reverse(self):
        """Iterate backward through the Linked List (Problem 5)."""
        curr = self._tail # Start at the back
        while curr is not None:
            yield curr.data # Provide (yield) each item to the user
            curr = curr.prev # Go backward in the linked list

    # --- Iterator & String Methods ---

    def __iter__(self):
        """Iterate forward through the LinkedList (forward iteration)."""
        curr = self._head
        while curr is not None:
            yield curr.data
            curr = curr.next

    def __str__(self):
        """Returns a strimg representation of the linked list."""
        return f"<LinkedList>{{{', '.join(map(str, self))}}}"

    # --- Test Methods (C# Translation) ---

    def head_and_tail_are_null(self) -> bool:
        """This is only for check if the head and the tail are null."""
        return self._head is None and self._tail is None

    def head_and_tail_are_not_null(self) -> bool:
        """This is only for check if the head and the tail are null."""
        return self._head is not None and self._tail is not None
