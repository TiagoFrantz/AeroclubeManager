namespace AeroclubeManager.Web.Models.Account
{

    //por enquanto sem uso 
    public class EditAccountDto
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Document {  get; set; }
        public DateTime DateOfBirth { get; set; }

        public bool ImageSelected { get; set; } = false;
        public bool ImageUpdated { get; set; } = false;

        public IFormFile Image {  get; set; }  
    }
}