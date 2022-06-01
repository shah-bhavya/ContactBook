namespace Contact.API.DTOs.Contact
{
    public class ContactResponseDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string PhotoPath { get; set; }
        public bool IsFavourite { get; set; }
    }
}
