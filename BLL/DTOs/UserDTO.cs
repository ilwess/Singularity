using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.DTOs
{
    [JsonObject("user")]
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

        [JsonProperty("contacts")]
        public ICollection<ContactDTO> Contacts { get; set; }

        [JsonProperty("blackList")]
        public ICollection<BlockedUserDTO> BlackList { get; set; }

        [JsonProperty("changes")]
        public ICollection<ChangedNameDTO> Changes { get; set; }

        [JsonProperty("lastEnter")]
        public DateTime LastEnter { get; set; }
    }

    public class BlockedUserDTO
    {
        public int BlockerId { get; set; }
        public int BlockedId { get; set; }
    }

    public class ContactDTO
    {
        public int OwnerId { get; set; }
        public int UserContactId { get; set; }
    }
}
