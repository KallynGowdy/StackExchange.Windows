namespace StackExchange.Windows.Api.Models
{
    public class Comment : Content
    {
        public override string FormattedDate => $"commented {CreationDate:MMM dd \"'\"yy} at {CreationDate:HH:mm}";
    }
}