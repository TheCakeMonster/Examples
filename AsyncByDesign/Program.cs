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

	private static async Task Initialise()
	{
		// TODO: Do intial setup work here
		await Task.Delay(200);
	}

	private static async Task<IList<string>> GetNames()
	{
		IList<string> names;

		await Task.Delay(200);
		names = new List<string>()
		{
			"Sue,",
			"Joe",
			"Alice"
		};

		return names;
	}
}