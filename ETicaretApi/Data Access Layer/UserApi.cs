using System.ComponentModel.DataAnnotations;

namespace ETicaretApi.Data_Access_Layer
{
    public class UserApi
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
