using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTOs
{
    public class ImageDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("messageDTO")]
        public MessageDTO message { get; set; }
    }
}
