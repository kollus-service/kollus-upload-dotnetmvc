using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using System.Text;

namespace KollusSampleMVC.Util
{
    public class KollusPlayer
    {
        private static readonly string secretKey = "";  // YOUR SECRET KEY
        private static readonly string customKey = "";  // YOUR COSTOM KEY
        private static readonly int expiredTime = 1440;
        private static readonly String cuid = "CLIENT_USER_ID";

        private static String encodeBase64Safe(String src)
        {
            if (string.IsNullOrEmpty(src))
            { return src; }
            byte[] temp = Encoding.UTF8.GetBytes(src.Trim());
            return Convert.ToBase64String(temp).TrimEnd('=').Replace('+', '-').Replace('/', '_');
        }
        private static String encodeBase64Safe(byte[] src)
        {
            return Convert.ToBase64String(src).TrimEnd('=').Replace('+', '-').Replace('/', '_');
        }
        private static String createToken(String payload)
        {
            var header = JObject.FromObject(new { typ = "JWT", alg = "HS256" });

            String h = encodeBase64Safe(header.ToString());
            String p = encodeBase64Safe(payload);
            String c = String.Format("{0}.{1}", h, p);
            HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey));
            byte[] signature = hmac.ComputeHash(Encoding.UTF8.GetBytes(c));
            String sig = encodeBase64Safe(signature);
            hmac.Dispose();
            return String.Format("{0}.{1}", c, sig);
        }
        public static String getUrlWithJWT(String mck)
        {
            Int64 exp = (Int64)DateTime.Now.AddMinutes(expiredTime).Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            var payload = JObject.FromObject(new { cuid = cuid, expt = exp });

            List<String> mcKeys = new List<string>();
            mcKeys.Add(mck);
            JArray mcs = new JArray();
            foreach (String key in mcKeys)
            {
                JObject mc = JObject.FromObject(new { mckey = key });
                mcs.Add(mc);
            }
            payload.Add("mc", mcs);

            String token = createToken(payload.ToString().Trim());
            String url = String.Format("https://v.kr.kollus.com/s?jwt={0}&custom_key={1}", token, customKey);

            return url;
        }

        public static String getSrUrlWithJWT(String mck, String cdnType)
        {
            Int64 exp = (Int64)DateTime.Now.AddMinutes(expiredTime).Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            var payload = JObject.FromObject(new { cuid = cuid, expt = exp });

            List<String> mcKeys = new List<string>();
            mcKeys.Add(mck);
            JArray mcs = new JArray();
            foreach (String key in mcKeys)
            {
                JObject mc = JObject.FromObject(new { mckey = key });
                mcs.Add(mc);
            }
            payload.Add("mc", mcs);

            String token = createToken(payload.ToString().Trim());
            String url = String.Format("https://v.kr.kollus.com/sr?cdn={0}&jwt={1}&custom_key={2}", cdnType, token, customKey);

            return url;
        }
    }
}
