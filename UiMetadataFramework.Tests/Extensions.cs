namespace UiMetadataFramework.Tests
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using global::MediatR;

	public static class Extensions
	{
		public static Dictionary<string, object?> ToDictionary(this IBaseRequest? request)
		{
			if (request == null)
			{
				return new Dictionary<string, object?>();
			}

			return request.GetType().GetProperties()
				.Where(t => t is { CanRead: true, MemberType: MemberTypes.Property })
				.Where(t => t.GetMethod!.IsPublic)
				.ToDictionary(t => t.Name, t => t.GetValue(request));
		}
	}
}