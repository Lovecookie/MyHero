using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Featrues.Requests;


/// <summary>
/// 
/// </summary>
/// <param name="PageSize"></param>
/// <param name="PageIndex"></param>
/// <description> ?pageIndex=0&pageSize=5 </description>
public record PaginationRequest(int PageSize = 10, int PageIndex = 0);