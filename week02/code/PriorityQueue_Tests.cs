using System.Collections.Generic;

public class PriorityQueue
{
    private Queue<string> highPriority = new Queue<string>();
    private Queue<string> lowPriority = new Queue<string>();

    public void Add(string item, bool high)
    {
        if (high)
        {
            highPriority.Enqueue(item);
        }
        else
        {
            lowPriority.Enqueue(item);
        }
    }

    public string Remove()
    {
        if (highPriority.Count > 0)
        {
            return highPriority.Dequeue();
        }
        else if (lowPriority.Count > 0)
        {
            return lowPriority.Dequeue();
        }
        else
        {
            return null;
        }
    }

    public int Length()
    {
        return highPriority.Count + lowPriority.Count;
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    public void TestPriorityQueue_1()
    {
        var pq = new PriorityQueue();
        pq.Add("Low1", false);
        pq.Add("High1", true);
        pq.Add("High2", true);
        pq.Add("Low2", false);

        string first = pq.Remove(); // High1
        string second = pq.Remove(); // High2
        string third = pq.Remove(); // Low1
        string fourth = pq.Remove(); // Low2

        Assert.AreEqual("High1", first);
        Assert.AreEqual("High2", second);
        Assert.AreEqual("Low1", third);
        Assert.AreEqual("Low2", fourth);
    }

    [TestMethod]
    public void TestPriorityQueue_2()
    {
        var pq = new PriorityQueue();
        string result = pq.Remove();
        Assert.IsNull(result);
    }
}

using System.Collections.Generic;

public class PersonQueue
{
    private Queue<string> queue = new Queue<string>();

    public void AddPerson(string name)
    {
        queue.Enqueue(name);
    }

    public string RemovePerson()
    {
        if (queue.Count == 0)
        {
            return null;
        }
        return queue.Dequeue();
    }

    public int Length()
    {
        return queue.Count;
    }
}

using System.Collections.Generic;

public class TakingTurnsQueue
{
    private Queue<PersonQueue> queues = new Queue<PersonQueue>();

    public void AddQueue(PersonQueue q)
    {
        queues.Enqueue(q);
    }

    public string Remove()
    {
        if (queues.Count == 0)
            return null;

        PersonQueue currentQueue = queues.Dequeue();
        string person = currentQueue.RemovePerson();

        if (currentQueue.Length() > 0)
        {
            queues.Enqueue(currentQueue);
        }

        return person;
    }

    public int Length()
    {
        int total = 0;
        foreach (var q in queues)
        {
            total += q.Length();
        }
        return total;
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class TakingTurnsQueueTests
{
    [TestMethod]
    public void TestTakingTurnsQueue_1()
    {
        var queue1 = new PersonQueue();
        var queue2 = new PersonQueue();

        queue1.AddPerson("Alice");
        queue1.AddPerson("Bob");
        queue2.AddPerson("Charlie");
        queue2.AddPerson("Diana");

        var takingTurns = new TakingTurnsQueue();
        takingTurns.AddQueue(queue1);
        takingTurns.AddQueue(queue2);

        Assert.AreEqual("Alice", takingTurns.Remove());
        Assert.AreEqual("Charlie", takingTurns.Remove());
        Assert.AreEqual("Bob", takingTurns.Remove());
        Assert.AreEqual("Diana", takingTurns.Remove());
        Assert.AreEqual(0, takingTurns.Length());
    }

    [TestMethod]
    public void TestTakingTurnsQueue_2()
    {
        var queue1 = new PersonQueue();
        queue1.AddPerson("Eve");
        queue1.AddPerson("Frank");

        var takingTurns = new TakingTurnsQueue();
        takingTurns.AddQueue(queue1);

        Assert.AreEqual("Eve", takingTurns.Remove());
        Assert.AreEqual("Frank", takingTurns.Remove());
        Assert.AreEqual(0, takingTurns.Length());
    }
}
