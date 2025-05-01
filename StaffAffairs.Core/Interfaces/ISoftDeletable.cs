using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffAffairs.Core.Interfaces
{
    public interface ISoftDeletable
    {
        bool IsDeleted { get; }   
    }
}
