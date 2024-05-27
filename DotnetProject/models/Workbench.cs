using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AasApi.Models
{
    public class Workbench
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public List<SubmodelData>? SubmodelData { get; set; }
    }
}