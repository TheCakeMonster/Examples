namespace AsyncByDesign;

public class Program
{ 
	static async Task Main(string[] args)
	{
		IList<string> names;

		// Get the data to display
		await Initialise();
		names = await GetNames();

		// Use the data
		foreach (string name in names)
		{
			Console.WriteLine(name);
		}
	}

	private static Task Initialise()
	{
		// TODO: Do intial setup work here
		return Task.CompletedTask;
	}

	private static Task<IList<string>> GetNames()
	{
		IList<string> names;

		names = new List<string>()
		{
			"Sue,",
			"Joe",
			"Alice"
		};

		return Task.FromResult(names);
	}
}