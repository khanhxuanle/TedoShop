using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeduShop.Web.Models
{
    public class FeedbackViewModel
    {

        public int ID { get; set; }

        [MaxLength(250, ErrorMessage = "Tên không được vượt quá 250 kí tự")]
        [Required(ErrorMessage = "Phải nhập tên")]
        public string Name { get; set; }

        [MaxLength(250, ErrorMessage = "Mail không được vượt quá 250 kí tự ")]
        public string Email { get; set; }

        [MaxLength(500, ErrorMessage = "Tin nhắn không được vượt quá 500 kí tự ")]
        public string Message { get; set; }

        public DateTime CreatDate { get; set; }

        [Required(ErrorMessage = "Phải chọn status")]
        public bool Status { get; set; }

        public ContactDetailViewModel ContactDetail { get; set; }
    }
}