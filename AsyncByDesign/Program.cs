namespace AsyncByDesign;

public class Program
{ 
	static void Main(string[] args)
	{
		IList<string> names;

		// Get the data to display
		Initialise();
		names = GetNames();

		// Use the data
		foreach (string name in names)
		{
			Console.WriteLine(name);
		}
	}

	private static void Initialise()
	{
		// TODO: Do intial setup work here
	}

	private static IList<string> GetNames()
	{
		IList<string> names;

		names = new List<string>()
		{
			"Sue,",
			"Joe",
			"Alice"
		};

		return names;
	}
}