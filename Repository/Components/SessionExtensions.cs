using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Newtonsoft.Json;

namespace Project_PhoneStore.Repository.Components
{
	public static class SessionExtensions
	{
		public static void SetJson(this ISession session, string key, object vlaue)
		{
			session.SetString(key, JsonConvert.SerializeObject(vlaue));

		}
		 public static T GetJson<T> (this ISession session, string key)
		{
			var sessionData = session.GetString(key);
			return sessionData == null ? default(T) : JsonConvert.DeserializeObject<T>(sessionData);
		}
	}
}
