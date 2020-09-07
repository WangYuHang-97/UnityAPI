namespace QFramework
{
    /// <summary>
    /// Res工厂用来创建Res
    /// </summary>
    public class ResFactory
    {
        /// <summary>
        /// 创建Res
        /// </summary>
        /// <param name="assetName"></param>
        /// <param name="assetBundleName"></param>
        /// <returns></returns>
        public static Res Create(string assetName, string assetBundleName)
        {
            Res res = null;
            
            if (assetBundleName != null)
            {
                res = new AssetRes(assetName, assetBundleName);//输出AssetRes
            }
            else if (assetName.StartsWith("resources://"))//字段以resources://开始
            {
                res = new ResourcesRes(assetName);//输出ResourcesRes
            }
            else
            {
                res = new AssetBundleRes(assetName);//输出AssetBundleRes
            }

            return res;
        }
    }
}