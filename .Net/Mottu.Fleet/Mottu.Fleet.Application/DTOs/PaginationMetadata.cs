using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mottu.Fleet.Application.DTOs;
public class PaginationMetadata
{
    public int FirstPage { get; set; } = 1;
    public int LastPage { get; set; }
    public int? PreviousPage { get; set; }
    public int? NextPage { get; set; }
    public long ProcessingTimeMs { get; set; }
    public Dictionary<string, object> AppliedFilters { get; set; } = new();
}