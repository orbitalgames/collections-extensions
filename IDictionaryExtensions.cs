// IDictionaryExtensions.cs
// Copyright (c) 2015 Orbital Games, LLC.

using System.Collections.Generic;

namespace OrbitalGames.Collections
{
	public static class IDictionaryExtensions
	{
		public static TValue GetValue<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key, TValue defaultValue)
		{
			TValue result;
			return source.TryGetValue(key, out result) ? result : defaultValue;
		}
	}
}
