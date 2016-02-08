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
	/// Extension methods for System.ICollection
	/// </summary>
	public static class ICollectionExtensions
	{
		/// <summary>
		/// Randomly select a weighted element from the given collection.
		/// </summary>
		/// <param name="source">Collection of items from which the weights and elements can be derived</param>
		/// <param name="weightSelector">A function to extract the weight from an element</param>
		/// <param name="rng">Random number generator, or a default new instance if null</param>
		/// <exception cref="System.ArgumentNullException">Thrown when <paramref name="source" />, <paramref name="weightSelector" />, or <paramref name="elementSelector" /> is null</exception>
		/// <returns>Randomly selected weighted element</returns>
		public static TSource WeightedRandom<TSource>(this ICollection<TSource> source, Func<TSource, int> weightSelector, Random rng = null)
		{
			return source.WeightedRandom(weightSelector, x => x, rng);
		}

		/// <summary>
		/// Randomly select a weighted element from the given collection.
		/// </summary>
		/// <param name="source">Collection of items from which the weights and elements can be derived</param>
		/// <param name="weightSelector">A function to extract the weight from an element</param>
		/// <param name="elementSelector">A function to extract the resulting value from an element</param>
		/// <param name="rng">Random number generator, or a default new instance if null</param>
		/// <exception cref="System.ArgumentNullException">Thrown when <paramref name="source" />, <paramref name="weightSelector" />, or <paramref name="elementSelector" /> is null</exception>
		/// <returns>Randomly selected weighted element</returns>
		public static TResult WeightedRandom<TSource, TResult>(this ICollection<TSource> source, Func<TSource, int> weightSelector, Func<TSource, TResult> elementSelector, Random rng = null)
		{
			return source.WeightedRandom(weightSelector, (x, y) => elementSelector(x), rng);
		}

		/// <summary>
		/// Randomly select a weighted element from the given collection. The element's index is used in the logic of the element selector function.
		/// </summary>
		/// <param name="source">Collection of items from which the weights and elements can be derived</param>
		/// <param name="weightSelector">A function to extract the weight from an element</param>
		/// <param name="elementSelector">A function to extract the resulting value from an element; the second parameter of the function represents the index of the source element</param>
		/// <param name="rng">Random number generator, or a default new instance if null</param>
		/// <exception cref="System.ArgumentNullException">Thrown when <paramref name="source" />, <paramref name="weightSelector" />, or <paramref name="elementSelector" /> is null</exception>
		/// <returns>Randomly selected weighted element</returns>
		public static TResult WeightedRandom<TSource, TResult>(this ICollection<TSource> source, Func<TSource, int> weightSelector, Func<TSource, int, TResult> elementSelector, Random rng = null)
		{
			if (rng == null)
			{
				rng = new Random();
			}
			return ElementByAggregate(source, 0, rng.Next() % source.Sum(weightSelector), weightSelector, (x, y) => x + y, elementSelector);
		}

		/// <summary>
		/// Randomly select a weighted element from the given collection.
		/// </summary>
		/// <param name="source">Collection of items from which the weights and elements can be derived</param>
		/// <param name="weightSelector">A function to extract the weight from an element</param>
		/// <param name="rng">Random number generator, or a default new instance if null</param>
		/// <exception cref="System.ArgumentNullException">Thrown when <paramref name="source" />, <paramref name="weightSelector" />, or <paramref name="elementSelector" /> is null</exception>
		/// <returns>Randomly selected weighted element</returns>
		public static TSource WeightedRandom<TSource>(this ICollection<TSource> source, Func<TSource, double> weightSelector, Random rng = null)
		{
			return source.WeightedRandom(weightSelector, x => x, rng);
		}

		/// <summary>
		/// Randomly select a weighted element from the given collection.
		/// </summary>
		/// <param name="source">Collection of items from which the weights and elements can be derived</param>
		/// <param name="weightSelector">A function to extract the weight from an element</param>
		/// <param name="elementSelector">A function to extract the resulting value from an element</param>
		/// <param name="rng">Random number generator, or a default new instance if null</param>
		/// <exception cref="System.ArgumentNullException">Thrown when <paramref name="source" />, <paramref name="weightSelector" />, or <paramref name="elementSelector" /> is null</exception>
		/// <returns>Randomly selected weighted element</returns>
		public static TResult WeightedRandom<TSource, TResult>(this ICollection<TSource> source, Func<TSource, double> weightSelector, Func<TSource, TResult> elementSelector, Random rng = null)
		{
			return source.WeightedRandom(weightSelector, (x, y) => elementSelector(x), rng);
		}

		/// <summary>
		/// Randomly select a weighted element from the given collection. The element's index is used in the logic of the element selector function.
		/// </summary>
		/// <param name="source">Collection of items from which the weights and elements can be derived</param>
		/// <param name="weightSelector">A function to extract the weight from an element</param>
		/// <param name="elementSelector">A function to extract the resulting value from an element; the second parameter of the function represents the index of the source element</param>
		/// <param name="rng">Random number generator, or a default new instance if null</param>
		/// <exception cref="System.ArgumentNullException">Thrown when <paramref name="source" />, <paramref name="weightSelector" />, or <paramref name="elementSelector" /> is null</exception>
		/// <returns>Randomly selected weighted element</returns>
		public static TResult WeightedRandom<TSource, TResult>(this ICollection<TSource> source, Func<TSource, double> weightSelector, Func<TSource, int, TResult> elementSelector, Random rng = null)
		{
			if (rng == null)
			{
				rng = new Random();
			}
			return ElementByAggregate(source, 0, rng.NextDouble() * source.Sum(weightSelector), weightSelector, (x, y) => x + y, elementSelector);
		}

		private static TResult ElementByAggregate<TSource, TWeight, TResult>(ICollection<TSource> source,
				TWeight seed,
				TWeight max,
				Func<TSource, TWeight> weightSelector,
				Func<TWeight, TWeight, TWeight> weightAccumulator,
				Func<TSource, int, TResult> elementSelector) where TWeight : IComparable
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (weightSelector == null)
			{
				throw new ArgumentNullException("weightSelector");
			}
			if (weightAccumulator == null)
			{
				throw new ArgumentNullException("weightAccumulator");
			}
			if (elementSelector == null)
			{
				throw new ArgumentNullException("elementSelector");
			}
			if (source.Count == 0)
			{
				throw new InvalidOperationException("source contains no elements");
			}
			TWeight currentWeight = seed;
			var item = source.GetEnumerator();
			int i = 0;
			for (; item.MoveNext(); ++i)
			{
				currentWeight = weightAccumulator(currentWeight, weightSelector(item.Current));
				if (currentWeight.CompareTo(max) >= 0)
				{
					return elementSelector(item.Current, i);
				}
			}
			return default(TResult);
		}
	}
}
