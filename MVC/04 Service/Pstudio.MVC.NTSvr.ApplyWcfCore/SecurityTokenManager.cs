using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pstudio.MVC.NTSvr.ApplyWcfCore
{
    public class SecurityTokenManager
    {
        private SecurityTokenManager()
        {

        }

        private static SecurityTokenManager m_instance = null;

        public static SecurityTokenManager Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = new SecurityTokenManager();
                }

                return m_instance;
            }
        }

        public void AddOrUpdateToken(string token, long timeStamp)
        {
            if (!m_validTokens.ContainsKey(token))
                m_validTokens.TryAdd(token, timeStamp);
            else
            {
                long temp = 0;
                if (m_validTokens.TryGetValue(token, out temp))
                {
                    m_validTokens.TryUpdate(token, timeStamp, temp);
                }
            }
        }

        private System.Collections.Concurrent.ConcurrentDictionary<string, long>
            m_validTokens = new System.Collections.Concurrent.ConcurrentDictionary<string, long>();

        public bool ExistsToken(string securityToken)
        {
            if (m_validTokens.ContainsKey(securityToken))
                return true;
            return false;
        }

        public void RemoveToken(string securityToken)
        {
            if (m_validTokens.ContainsKey(securityToken))
            {
                long temp = 0;
                m_validTokens.TryRemove(securityToken, out temp);
            }
        }

        internal long GetTimeStamp(string securityToken)
        {
            throw new NotImplementedException();
        }
    }
}
