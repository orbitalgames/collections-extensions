/*
The MIT License (MIT)

Copyright (c) 2015 Orbital Games, LLC.

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
using System.Collections;
using System.Collections.Generic;

namespace OrbitalGames.Collections
{
	/// <summary>
	/// Extension methods for System.IEnumerator
	/// </summary>
	public static class IEnumeratorExtensions
	{
		/// <summary>
		/// Enumerates an IEnumerator instance and stores the results in an IList.
		/// </summary>
		/// <param name="source">Enumerator instance</param>
		/// <param name="initialCapacity">Optional initial capacity to which the result is set</param>
		/// <exception cref="System.ArgumentNullException">Thrown when <paramref name="source" /> is null</exception>
		/// <returns>IList containing the enumerated values</returns>
		public static IList<TResult> ToList<TResult>(this IEnumerator<TResult> source, int initialCapacity = 0)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			var result = new List<TResult>(initialCapacity);
			while (source.MoveNext())
			{
				result.Add(source.Current);
			}
			return result;
		}

		/// <summary>
		/// Enumerates an IEnumerator instance and stores the results in an IList.
		/// </summary>
		/// <param name="source">Enumerator instance</param>
		/// <param name="initialCapacity">Optional initial capacity to which the result is set</param>
		/// <exception cref="System.ArgumentNullException">Thrown when <paramref name="source" /> is null</exception>
		/// <returns>IList containing the enumerated values</returns>
		public static IList ToList(this IEnumerator source, int initialCapacity = 0)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			var result = new List<object>(initialCapacity);
			while (source.MoveNext())
			{
				result.Add(source.Current);
			}
			return result;
		}
	}
}
