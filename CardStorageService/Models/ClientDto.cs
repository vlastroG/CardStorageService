namespace CardStorageService.Models
{
    public class ClientDto
    {
        public string? Surname { get; set; }

        public string? FirstName { get; set; }

        public string? Patronymic { get; set; }

        public ICollection<CardDto> Cards { get; set; }
    }
}
