using System.ComponentModel.DataAnnotations;

namespace Tawh.NoTrace.Web.Models.Account
{
    public class ResetPasswordViewModel
    {
        /// <summary>
        /// Encrypted user id.
        /// </summary>
        [Required]
        public string UserId { get; set; }

        [Required]
        public string ResetCode { get; set; }
    }
}