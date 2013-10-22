using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftDataAnalysisWinRT
{
    public class ApplicationContext
    {
        private ApplicationContext()
        { }

        private static ApplicationContext m_Intance;
        public static ApplicationContext Intance
        {
            get
            {
                if (m_Intance != null)
                    m_Intance = new ApplicationContext();
                return m_Intance;
            }
        }

    }
}
