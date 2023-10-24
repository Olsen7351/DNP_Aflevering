using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class SubComment
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string Text { get; set; }
        public string Author { get; set; } // Add this property
        public DateTime CreatedAt { get; set; }
    }
}
