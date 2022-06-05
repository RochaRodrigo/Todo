namespace Todo.Commands
{
    public class CommandResult
    {
        public CommandResult(bool status, string title, object data)
        {
            Status = status;
            Title = title;
            Data = data;
        }

        public bool Status { get; set; }

        public string Title { get; set; }

        public object Data { get; set; }
    }
}