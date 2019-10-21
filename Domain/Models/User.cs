using Domain.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    [JsonObject("user")]
    public class User : IEntity
    {
        [JsonProperty("id")]
        [Key]
        public int Id { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

        [Required]
        [JsonProperty("email")]
        public string Email { get; set; }

        [Required]
        [JsonProperty("password")]
        public string Password { get; set; }

        [Required]
        [JsonProperty("phone")]
        public string Phone { get; set; }

        [Required]
        [JsonProperty("login")]
        public string Login { get; set; }

        [Required]
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("ava")]
        [ForeignKey("avaId")]
        public Image Ava { get; set; }

        [JsonProperty("contacts")]
        public ICollection<Contact> Contacts { get; set; }

        [JsonProperty("blackList")]
        public ICollection<BlockedUser> BlackList { get; set; }

        [JsonProperty("changes")]
        public ICollection<ChangedName> Changes { get; set; }
    }

    [JsonObject("contact")]
    public class Contact : IEntity
    { 
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public int UserContactId { get; set; }
    }

    public class BlockedUser : IEntity
    {
        public int Id { get; set; }
        public int BlockerId { get; set; }
        public int BlockedId { get; set; }
    }
}
