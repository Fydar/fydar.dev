using System;
using System.Collections.Generic;

namespace Portfolio.Site.Services.PageMetaProvider
{
	public class PageMetaCollection
	{
		private readonly Dictionary<Type, object> models;

		public PageMetaCollection()
		{
			models = new Dictionary<Type, object>();
		}

		public T? GetModel<T>()
			where T : class
		{
			if (models.TryGetValue(typeof(T), out object? value))
			{
				return (T)value;
			}
			return null;
		}

		public void SetModel(Type type, object value)
		{
			models[type] = value;
		}

		public void SetModel<T>(T value)
			where T : class
		{
			models[typeof(T)] = value;
		}
	}
}
