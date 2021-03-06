using SiteServer.CMS.Core;
using SiteServer.CMS.Model;

namespace SiteServer.CMS.StlParser.Cache
{
    public class Input
    {
        private static readonly object LockObject = new object();

        public static InputInfo GetInputInfo(int inputId, string guid)
        {
            var cacheKey = StlCacheUtils.GetCacheKeyByGuid(guid, nameof(Input), nameof(GetInputInfo),
                    inputId.ToString());
            var retval = StlCacheUtils.GetCache<InputInfo>(cacheKey);
            if (retval != null) return retval;

            lock (LockObject)
            {
                retval = StlCacheUtils.GetCache<InputInfo>(cacheKey);
                if (retval == null)
                {
                    retval = DataProvider.InputDao.GetInputInfo(inputId);
                    StlCacheUtils.SetCache(cacheKey, retval);
                }
            }

            return retval;
        }

        public static int GetInputIdAsPossible(string inputName, int publishmentSystemId, string guid)
        {
            var cacheKey = StlCacheUtils.GetCacheKeyByGuid(guid, nameof(Input), nameof(GetInputIdAsPossible),
                    inputName, publishmentSystemId.ToString());
            var retval = StlCacheUtils.GetIntCache(cacheKey);
            if (retval != -1) return retval;

            lock (LockObject)
            {
                retval = StlCacheUtils.GetIntCache(cacheKey);
                if (retval == -1)
                {
                    retval = DataProvider.InputDao.GetInputIdAsPossible(inputName, publishmentSystemId);
                    StlCacheUtils.SetCache(cacheKey, retval);
                }
            }

            return retval;
        }
    }
}
