namespace CardStorageService.Models
{
    public interface IOperationResult
    {
        public int ErrorCode { get; }

        public string? ErrorMessage { get; }
    }
}
