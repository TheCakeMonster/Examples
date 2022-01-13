using Csla;
using Csla.Server;

namespace Interception.Library
{
	public class FakeDataPortal : IDataPortalServer
	{
		private readonly InterceptionManager _interceptionManager;

		public FakeDataPortal(InterceptionManager interceptionManager)
		{
			_interceptionManager = interceptionManager;
		}

		public async Task<DataPortalResult> Create(Type objectType, object criteria, DataPortalContext context, bool isSync)
		{
			DataPortalResult result;
			
			Initialize(new InterceptArgs { ObjectType = objectType, Parameter = criteria, Operation = DataPortalOperations.Create, IsSync = isSync });

			result = await DoCreate();

			Complete(new InterceptArgs { ObjectType = objectType, Parameter = criteria, Result = result, Operation = DataPortalOperations.Create, IsSync = isSync });

			return result;
		}

		public async Task<DataPortalResult> Fetch(Type objectType, object criteria, DataPortalContext context, bool isSync)
		{
			DataPortalResult result;
			
			Initialize(new InterceptArgs { ObjectType = objectType, Parameter = criteria, Operation = DataPortalOperations.Create, IsSync = isSync });

			result = await DoFetch();

			Complete(new InterceptArgs { ObjectType = objectType, Parameter = criteria, Result = result, Operation = DataPortalOperations.Create, IsSync = isSync });

			return result;
		}

		public async Task<DataPortalResult> Update(object obj, DataPortalContext context, bool isSync)
		{
			Type objectType;
			DataPortalResult result;

			objectType = obj.GetType();

			Initialize(new InterceptArgs { ObjectType = objectType, Parameter = obj, Operation = DataPortalOperations.Create, IsSync = isSync });

			result = await DoUpdate();

			Complete(new InterceptArgs { ObjectType = objectType, Parameter = obj, Result = result, Operation = DataPortalOperations.Create, IsSync = isSync });

			return result;
		}

		public async Task<DataPortalResult> Delete(Type objectType, object criteria, DataPortalContext context, bool isSync)
		{
			DataPortalResult result;

			Initialize(new InterceptArgs { ObjectType = objectType, Parameter = criteria, Operation = DataPortalOperations.Create, IsSync = isSync });

			result = await DoDelete();

			Complete(new InterceptArgs { ObjectType = objectType, Parameter = criteria, Result = result, Operation = DataPortalOperations.Create, IsSync = isSync });

			return result;
		}

		private void Initialize(InterceptArgs interceptArgs)
		{
			_interceptionManager.Initialize(interceptArgs);
		}

		private void Complete(InterceptArgs interceptArgs)
		{
			_interceptionManager.Complete(interceptArgs);
		}

		#region Remaining DataPortal behaviour (faked)

		private Task<DataPortalResult> DoCreate()
		{
			return Task.FromResult(new DataPortalResult());
		}

		private Task<DataPortalResult> DoFetch()
		{
			return Task.FromResult(new DataPortalResult());
		}

		private Task<DataPortalResult> DoUpdate()
		{
			return Task.FromResult(new DataPortalResult());
		}

		private Task<DataPortalResult> DoDelete()
		{
			return Task.FromResult(new DataPortalResult());
		}

		#endregion

	}
}