using ExecutionFramework;

namespace Executors
{
	public class TestExecutor : ExecutorBase
	{

		private string _notUsed1 = "This is quite a long string that will need to be placed into the assembly if it is not stripped by the trimmer";
		private string _notUsed2 = "This is also quite a long string and this too will need to be placed into the assembly if it is not stripped by the trimmer";

		private void PerformSomeAction()
		{
			int test;
			string checks;

			// Some code that is to be executed within the controlled environment
			// Console.WriteLine($"I ran! Base says {TellMeASecret()}");
			checks = _notUsed1;
			checks = _notUsed2;
			test = RandomIntGenerator.Generate();
		}

	}
}
