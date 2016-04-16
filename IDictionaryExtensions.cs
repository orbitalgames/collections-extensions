/*
The MIT License (MIT)

Copyright (c) 2016 Orbital Games, LLC.

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System;
using System.Collections.Generic;

namespace OrbitalGames.Collections
{
	/// <summary>
	/// Extension methods for System.IDictionary
	/// </summary>
	public static class IDictionaryExtensions
	{
		/// <summary>
		/// Retrieves a value by key or a default value if the key is not present in the dictionary.
		/// </summary>
		/// <param name="source">Source dictionary</param>
		/// <param name="key">Key to use to lookup the desired value</param>
		/// <param name="defaultValue">Value to return if <paramref name="key" /> is not present in <paramref name="source" />.</param>
		/// <exception cref="System.ArgumentNullException">Thrown when <paramref name="source" /> is null</exception>
		/// <returns>Value associated with the <paramref name="key" />, or <paramref name="defaultValue" /> otherwise.</returns>
		public static TValue GetValue<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key, TValue defaultValue)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			TValue result;
			return source.TryGetValue(key, out result) ? result : defaultValue;
		}

		/// <summary>
		/// Adds a range of key/value pairs to a dictionary.  Existing elements are updated if <paramref name="collection" /> contains keys already present in the dictionary.
		/// </summary>
		/// <param name="source">Source dictionary</param>
		/// <param name="collection">Range of key/value pairs</param>
		/// <exception cref="System.ArgumentNullException">Thrown when <paramref name="source" /> is null</exception>
		/// <exception cref="System.ArgumentNullException">Thrown when <paramref name="collection" /> is null</exception>
		public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> source, IEnumerable<KeyValuePair<TKey, TValue>> collection)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			foreach (var element in collection)
			{
				source[element.Key] = element.Value;
			}
		}
	}
}
