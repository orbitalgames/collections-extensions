﻿/*
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
using System.Security.Cryptography;
using System;

namespace OrbitalGames.Collections
{
	/// <summary>
	/// Extension methods for System.IList.
	/// </summary>
	public static class IListExtensions
	{
		/// <summary>
		/// Adds a range of elements to the given IList.
		/// </summary>
		/// <param name="source">Source list</param>
		/// <param name="collection">Range of elements</param>
		/// <exception cref="System.ArgumentNullException">Thrown when <paramref name="source" /> is null</exception>
		/// <exception cref="System.ArgumentNullException">Thrown when <paramref name="collection" /> is null</exception>
		public static void AddRange<TSource>(this IList<TSource> source, IEnumerable<TSource> collection)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			foreach (var item in collection)
			{
				source.Add(item);
			}
		}

		/// <summary>
		/// Returns a randomized copy of the given IList.
		/// </summary>
		/// <param name="source">Collection to randomize</param>
		/// <param name="rng">Random number generator, or a new default if unspecified</param>
		/// <exception cref="System.ArgumentNullException">Thrown when <paramref name="source" /> is null</exception>
		/// <returns>Randomized copy of the given <paramref name="source" /></returns>
		public static IList<TSource> Randomize<TSource>(this IList<TSource> source, Random rng = null)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			var result = new List<TSource>(source);
			return result.RandomizeInPlace(rng);
		}

		/// <summary>
		/// Randomizes an IList in-place.
		/// </summary>
		/// <param name="source">Collection to randomize</param>
		/// <param name="rng">Random number generator, or a new default if unspecified</param>
		/// <exception cref="System.ArgumentNullException">Thrown when <paramref name="source" /> is null</exception>
		/// <returns>Randomized <paramref name="source" /> for use with method chaining</returns>
		public static IList<TSource> RandomizeInPlace<TSource>(this IList<TSource> source, Random rng = null)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (rng == null)
			{
				rng = new Random();
			}
			for (int i = 0; i < source.Count - 1; ++i)
			{
				int r = rng.Next(i, source.Count);
				object tmp = source[i];
				source[i] = source[r];
				source[r] = (TSource)tmp;
			}
			return source;
		}

		/// <summary>
		/// Returns a securely randomized copy of the given IList.
		/// </summary>
		/// <param name="source">Collection to randomize</param>
		/// <param name="rng">Random number generator, or a new default if unspecified</param>
		/// <exception cref="System.ArgumentNullException">Thrown when <paramref name="source" /> is null</exception>
		/// <returns>Securely randomized copy of the given <paramref name="source" /></returns>
		public static IList<TSource> RandomizeSecure<TSource>(IList<TSource> source, RandomNumberGenerator rng = null)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			var result = new List<TSource>(source);
			return result.RandomizeInPlaceSecure(rng);
		}

		/// <summary>
		/// Randomizes an IList in-place securely.
		/// </summary>
		/// <param name="source">Collection to randomize</param>
		/// <param name="rng">Random number generator, or a new default if unspecified</param>
		/// <exception cref="System.ArgumentNullException">Thrown when <paramref name="source" /> is null</exception>
		/// <returns>Securely randomized <paramref name="source" /> for use with method chaining</returns>
		public static IList<TSource> RandomizeInPlaceSecure<TSource>(this IList<TSource> source, RandomNumberGenerator rng = null)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (rng == null)
			{
				rng = new RNGCryptoServiceProvider();
			}
			for (int i = 0; i < source.Count - 1; ++i)
			{
				int r = GetRandomSecureInt(i, source.Count, rng);
				object tmp = source[i];
				source[i] = source[r];
				source[r] = (TSource)tmp;
			}
			return source;
		}

		/// <summary>
		/// Gets the next integer from the given RandomNumberGenerator.
		/// </summary>
		/// <param name="min">Minimum value (inclusive)</param>
		/// <param name="max">Maximum value (exclusive)</param>
		/// <param name="rng">Random number generator</param>
		/// <returns>Next integer generated by <paramref name="rng" /></returns>
		private static int GetRandomSecureInt(int min, int max, RandomNumberGenerator rng)
		{
			var randomBytes = new byte[sizeof(int)];
			rng.GetBytes(randomBytes);
			int randomInt = Math.Abs(BitConverter.ToInt32(randomBytes, 0));
			return (randomInt % (max - min)) + min;
		}
	}
}
