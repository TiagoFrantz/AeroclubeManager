using AeroclubeManager.Core.Entities.User;

namespace AeroclubeManager.Web.Models.Home
{
    public class HomeIndexModelView
    {
        public bool UserAuthenticated { get; set; } = false;
        public ApplicationUser User { get; set; }
    }
}
