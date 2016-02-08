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

using System.Collections.Generic;
using System.Linq;
using System;

namespace OrbitalGames.Collections
{
	/// <summary>
	/// Extension methods for System.IEnumerable
	/// </summary>
	public static class IEnumerableExtensions
	{
		/// <summary>
		/// Joins the string representations of each element of the given IEnumerable using a given separator string.
		/// </summary>
		/// <param name="source">IEnumerable to join</param>
		/// <param name="separator">String to use as a separator</param>
		/// <exception cref="System.ArgumentNullException">Thrown when <paramref name="source" /> is null</exception>
		/// <returns>Joined string</returns>
		public static string StringJoin<TSource>(this IEnumerable<TSource> source, string separator)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return string.Join(separator, source.Select<TSource, string>(x => x.ToString()).ToArray());
		}
	}
}
