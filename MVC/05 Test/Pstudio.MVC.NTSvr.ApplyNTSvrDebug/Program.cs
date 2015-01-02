using Pstudio.MVC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pstudio.MVC.NTSvr.ApplyNTSvrDebug
{
    class Program
    {
        static void Main(string[] args)
        {
            using (EntityDbContext context =
                new EntityDbContext("name=DefaultConnection"))
            {
                var list = context.SysConfigs.ToList();

            }
        }
    }
}
