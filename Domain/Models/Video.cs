using Domain.Abstracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    [JsonObject("video")]
    public class Video : Content
    {
    }
}
