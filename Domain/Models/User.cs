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
        public Image Ava { get; set; }

        [JsonProperty("contacts")]
        public ICollection<Contact> Contacts { get; set; }

        [JsonProperty("blackList")]
        public ICollection<User> BlackList { get; set; }

        [JsonProperty("changes")]
        public ICollection<ChangedName> Changes { get; set; }

        [JsonProperty("messages")]
        public ICollection<Message> Messages { get; set; }
    }

    [JsonObject("contact")]
    public class Contact
    { 
        public User Owner { get; set; }
        public User UserContact { get; set; }
    }
}
