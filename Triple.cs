using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDFPhilosophyApp
{
    public record Triple
    {
        public required string Subject { get; set; }
        public required string Predicate { get; set; }
        public required string Object { get; set; }
    }
}
