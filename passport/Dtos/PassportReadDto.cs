namespace passport.Dtos
{
    public class PassportReadDto
    {        
        public int Id { get; set; }

        public int PassportNumber { get; set; }

        public string AuthorityThatIssued { get; set; }

        public string Status { get; set; }


        public string AccountData { get; set; }
    }
}