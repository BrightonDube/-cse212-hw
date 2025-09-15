using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: 
    // Expected Result: 
    // Defect(s) Found: 
    public void TestPriorityQueue_1()
    {
    // Scenario: Enqueue several items with different priorities then dequeue them
    // Expected Result: Items are returned in order of descending priority. If priorities tie, the earlier enqueued item is returned first.
    var priorityQueue = new PriorityQueue();
    priorityQueue.Enqueue("low", 1);
    priorityQueue.Enqueue("med", 5);
    priorityQueue.Enqueue("high", 10);

    Assert.AreEqual("high", priorityQueue.Dequeue());
    Assert.AreEqual("med", priorityQueue.Dequeue());
    Assert.AreEqual("low", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: 
    // Expected Result: 
    // Defect(s) Found: 
    public void TestPriorityQueue_2()
    {
        // Scenario: Enqueue items with duplicate highest priority and ensure FIFO tie-break. Also verify empty queue exception.
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("firstHigh", 10);
        priorityQueue.Enqueue("secondHigh", 10);
        priorityQueue.Enqueue("low", 1);

        // firstHigh should be dequeued before secondHigh because it was enqueued earlier
        Assert.AreEqual("firstHigh", priorityQueue.Dequeue());
        Assert.AreEqual("secondHigh", priorityQueue.Dequeue());
        Assert.AreEqual("low", priorityQueue.Dequeue());

        // Empty queue should throw the required InvalidOperationException
        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
    }

    // Add more test cases as needed below.
}