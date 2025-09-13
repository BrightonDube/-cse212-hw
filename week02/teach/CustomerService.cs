/// <summary>
/// Maintain a Customer Service Queue.  Allows new customers to be 
/// added and allows customers to be serviced.
/// </summary>
public class CustomerService
{
    public static void Run()
    {
        // Automated tests for CustomerService

        // Test 1: Add several customers directly and verify ToString
        Console.WriteLine("Test 1: Add customers and inspect queue");
        var cs = new CustomerService(5);
        cs._queue.Add(new Customer("Alice", "A1", "Password reset"));
        cs._queue.Add(new Customer("Bob", "B2", "Billing issue"));
        cs._queue.Add(new Customer("Carol", "C3", "Connectivity"));

        Console.WriteLine("Expected: size=3 and three customers in order (Alice, Bob, Carol)");
        Console.WriteLine("Actual:   " + cs);
        Console.WriteLine("Defect(s) Found: \n");
        Console.WriteLine("=================");

        // Test 2: Serve a customer from a queue with multiple entries
        Console.WriteLine("Test 2: ServeCustomer with multiple customers");
        var cs2 = new CustomerService(5);
        cs2._queue.Add(new Customer("Dave", "D4", "Password change"));
        cs2._queue.Add(new Customer("Ellen", "E5", "Account locked"));

        Console.WriteLine("Before Serve: " + cs2);
        try
        {
            cs2.ServeCustomer();
            Console.WriteLine("After Serve:  " + cs2);
            Console.WriteLine("Note: ServeCustomer should print and remove the FIRST customer (Dave).");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Defect(s) Found: Exception during ServeCustomer -> " + ex.GetType().Name + ": " + ex.Message);
        }
        Console.WriteLine("=================");

        // Test 3: ServeCustomer on a single-customer queue (should handle gracefully)
        Console.WriteLine("Test 3: ServeCustomer with a single customer (edge case)");
        var cs3 = new CustomerService(5);
        cs3._queue.Add(new Customer("Frank", "F6", "Installation"));

        Console.WriteLine("Before Serve: " + cs3);
        try
        {
            cs3.ServeCustomer();
            Console.WriteLine("After Serve:  " + cs3);
            Console.WriteLine("Note: Serving the only customer should not throw; queue should be empty afterward.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Defect(s) Found: Exception during ServeCustomer -> " + ex.GetType().Name + ": " + ex.Message);
        }
        Console.WriteLine("=================");

        // Test 4: Max-size enforcement using AddNewCustomer (supply Console input)
        Console.WriteLine("Test 4: Max-size enforcement for AddNewCustomer");
        var csMax = new CustomerService(2);
        csMax._queue.Add(new Customer("Gina", "G7", "Password"));
        csMax._queue.Add(new Customer("Hank", "H8", "Billing"));

        Console.WriteLine("Before AddNewCustomer (max_size=2): " + csMax);
        var originalIn = Console.In;
        try
        {
            // supply input for a third customer
            Console.SetIn(new System.IO.StringReader("Ivy\nI9\nPrinter won't print\n"));
            csMax.AddNewCustomer();
        }
        finally
        {
            Console.SetIn(originalIn);
        }

        Console.WriteLine("After AddNewCustomer:  " + csMax);
        Console.WriteLine("Expected: AddNewCustomer should NOT add a third customer when max_size=2.");
        Console.WriteLine("Defect(s) Found: \n");
        Console.WriteLine("=================");

        // Add more Test Cases As Needed Below
    }

    private readonly List<Customer> _queue = new();
    private readonly int _maxSize;

    public CustomerService(int maxSize)
    {
        if (maxSize <= 0)
            _maxSize = 10;
        else
            _maxSize = maxSize;
    }

    /// <summary>
    /// Defines a Customer record for the service queue.
    /// This is an inner class.  Its real name is CustomerService.Customer
    /// </summary>
    private class Customer
    {
        public Customer(string name, string accountId, string problem)
        {
            Name = name;
            AccountId = accountId;
            Problem = problem;
        }

        private string Name { get; }
        private string AccountId { get; }
        private string Problem { get; }

        public override string ToString()
        {
            return $"{Name} ({AccountId})  : {Problem}";
        }
    }

    /// <summary>
    /// Prompt the user for the customer and problem information.  Put the 
    /// new record into the queue.
    /// </summary>
    private void AddNewCustomer()
    {
        // Verify there is room in the service queue
        if (_queue.Count > _maxSize)
        {
            Console.WriteLine("Maximum Number of Customers in Queue.");
            return;
        }

        Console.Write("Customer Name: ");
        var name = Console.ReadLine()!.Trim();
        Console.Write("Account Id: ");
        var accountId = Console.ReadLine()!.Trim();
        Console.Write("Problem: ");
        var problem = Console.ReadLine()!.Trim();

        // Create the customer object and add it to the queue
        var customer = new Customer(name, accountId, problem);
        _queue.Add(customer);
    }

    /// <summary>
    /// Dequeue the next customer and display the information.
    /// </summary>
    private void ServeCustomer()
    {
        _queue.RemoveAt(0);
        var customer = _queue[0];
        Console.WriteLine(customer);
    }

    /// <summary>
    /// Support the WriteLine function to provide a string representation of the
    /// customer service queue object. This is useful for debugging. If you have a 
    /// CustomerService object called cs, then you run Console.WriteLine(cs) to
    /// see the contents.
    /// </summary>
    /// <returns>A string representation of the queue</returns>
    public override string ToString()
    {
        return $"[size={_queue.Count} max_size={_maxSize} => " + string.Join(", ", _queue) + "]";
    }
}