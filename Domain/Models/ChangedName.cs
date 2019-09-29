using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class ChangedName : IEntity
    {
        public string NewName { get; set; }
        public User Changable { get; set; }
        public int Id { get; set; }
    }
}
