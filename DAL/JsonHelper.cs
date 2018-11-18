using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace DAL
{
    public class JsonHelper
    {
        private static JavaScriptSerializer serializer = new JavaScriptSerializer();
        #region [反序列化]
        /// <summary>
        /// 反序列化微博操作返回信息
        /// </summary>
        /// <param name="jsonStr"></param>
        /// <returns></returns>
        public static Model.BackJson GetBackJson(string jsonStr)
        {
            Model.BackJson backJson = new Model.BackJson();

            try
            {
                backJson = serializer.Deserialize<Model.BackJson>(jsonStr);
            }
            catch { }

            return backJson;
        }

        //反序列化web api 信息
        public static Model.WebMessage WebMessage(string jsonStr)
        {
            Model.WebMessage webMessage = new Model.WebMessage();
            try
            {
                webMessage = serializer.Deserialize<Model.WebMessage>(jsonStr);
            }
            catch { }
            return webMessage;
        }
        #endregion
    }
}
