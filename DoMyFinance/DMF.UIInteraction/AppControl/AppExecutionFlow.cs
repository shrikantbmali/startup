namespace DMF.UIInteraction.AppControl
{
	public abstract class AppExecutionFlow
	{
		private readonly AppExecutionFlow _successor;

		protected AppExecutionFlow(AppExecutionFlow successor)
		{
			_successor = successor;
		}

		protected abstract void Execute();

		public void Start()
		{
			throw new System.NotImplementedException();
		}
	}
}