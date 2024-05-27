namespace Models1
{
    public class RegistrationResponseDTO
    {
        public bool IsRegistrationSuccessful { get; set; }

        public IEnumerable<string> Errors { get; set; }
    }
}
