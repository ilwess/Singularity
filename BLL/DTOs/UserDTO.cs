using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.DTOs
{
    public class UserDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("ava")]
        public ImageDTO Ava { get; set; }

        [JsonProperty("contacats")]
        public ICollection<UserDTO> Contacts { get; set; }

        [JsonProperty("blackList")]
        public ICollection<UserDTO> BlackList { get; set; }

        [JsonProperty("changes")]
        public ICollection<ChangedNameDTO> Changes { get; set; }
    }
}
