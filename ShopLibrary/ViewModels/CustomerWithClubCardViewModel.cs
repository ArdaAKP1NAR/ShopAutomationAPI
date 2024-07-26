using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ShopLibrary.ViewModels
{

    public class CustomerWithClubCardViewModel
    {
       /* [Required] //[Required]: Name özelliğinin boş geçilemeyeceğini belirtir. Kullanıcı, bu alanı doldurmak zorundadır.
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "The name should only contain letters.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", Name özelliğinin yalnızca harflerden ve boşluklardan oluşmasını zorunlu kılar. 
        */
        public string Name { get; set; }
        public long? ClubId { get; set; }
    }
}
