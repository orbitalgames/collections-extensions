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

		/// <summary>
		/// Returns the cartesian product of a sequence of sequences.
		/// </summary>
		/// <param name="source">Sequence of sequences on which to operate</param>
		/// <exception cref="System.ArgumentNullException">Thrown when <paramref name="source" /> is null</exception>
		/// <returns>Cartesian product of source</returns>
		/// <remarks>
		/// Inspired by Eric Lippert: http://ericlippert.com/2010/06/28/computing-a-cartesian-product-with-linq/
		/// </remarks>
		public static IEnumerable<IEnumerable<T>> CartesianProduct<T>(this IEnumerable<IEnumerable<T>> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			IEnumerable<IEnumerable<T>> emptyProduct = new[] { Enumerable.Empty<T>() };
			return source.Aggregate(emptyProduct, (accumulator, sequence) => accumulator.SelectMany(accseq => sequence.Select(item => accseq.Concat(new[] { item }))));
		}

		/// <summary>
		/// Concatenates a single key/value pair to a sequence of <see cref="KeyValuePair{TKey, TValue}" /> elements.
		/// </summary>
		/// <param name="source">Sequence to which the new key/value pair will be concatenated</param>
		/// <param name="key">Key</param>
		/// <param name="value">Value</param>
		/// <returns>Concatenation of the original sequence and the given key/value pair</returns>
		public static IEnumerable<KeyValuePair<TKey, TValue>> ConcatKVP<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, TKey key, TValue value)
		{
			return source.Concat(new[] { new KeyValuePair<TKey, TValue>(key, value) });
		}
	}
}
