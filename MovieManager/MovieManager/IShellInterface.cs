namespace MovieManager
{
	public interface IShellInterface
	{
		void Show();

		bool ShowActivated { get; set; }

		void SetContent(object content, string region);

		void ClearContent(string region);
	}
}