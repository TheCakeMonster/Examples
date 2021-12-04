namespace AsyncByDesign;

public class Program
{ 
	static async Task Main(string[] args)
	{
		IList<string> names;

		// Get the data to display
		await InitialiseAsync();
		names = await GetNamesAsync();

		// Use the data
		foreach (string name in names)
		{
			Console.WriteLine(name);
		}
	}

	private static async Task InitialiseAsync()
	{
		// TODO: Do intial setup work here
		await Task.Delay(200);
	}

	private static async Task<IList<string>> GetNamesAsync()
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