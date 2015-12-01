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
using System;

namespace OrbitalGames.Collections
{
	/// <summary>
	/// Extension methods for System.LinkedList.
	/// </summary>
	public static class LinkedListExtensions
	{
		/// <summary>
		/// Enumerate a LinkedList allowing for removal of nodes.
		/// </summary>
		/// <param name="source">LinkedList to enumerate</param>
		/// <exception cref="System.ArgumentException">Thrown when <paramref name="source" /> is null</exception>
		/// <returns>Enumerable sequence of items contained in <paramref name="source" />.</returns>
		/// <remarks>
		/// This method is implemented using deferred execution. The immediate return value is an object that stores all the information that is required to perform the action. The query represented by this method is not executed until the object is enumerated either by calling its GetEnumerator method directly or by using foreach.
		/// This method works by storing a reference to the next element prior to yielding. This means that the caller can remove the yielded node or otherwise modify the list. Note however that removing both the current and the next node will end the enumeration.
		/// </remarks>
		public static IEnumerable<TResult> ModifiableEnumerate<TResult>(this LinkedList<TResult> source)
		{
			if (source == null)
			{
				throw new ArgumentException("source is null", "source");
			}
			LinkedListNode<TResult> current = source.First;
			while (current != null)
			{
				LinkedListNode<TResult> next = current.Next;
				yield return current.Value;
				current = current.Next ?? next;
			}
			yield break;
		}

		/// <summary>
		/// Enumerate a LinkedList allowing for removal of nodes.
		/// </summary>
		/// <param name="source">LinkedList to enumerate</param>
		/// <exception cref="System.ArgumentException">Thrown when <paramref name="source" /> is null</exception>
		/// <returns>Enumerable sequence of item nodes contained in <paramref name="source" />.</returns>
		/// <remarks>
		/// This method is implemented using deferred execution. The immediate return value is an object that stores all the information that is required to perform the action. The query represented by this method is not executed until the object is enumerated either by calling its GetEnumerator method directly or by using foreach.
		/// This method works by storing a reference to the next node prior to yielding. This means that the caller can remove the yielded node or otherwise modify the list. Note however that removing both the current and the next node will end the enumeration.
		/// </remarks>
		public static IEnumerable<LinkedListNode<TResult>> ModifiableEnumerateNodes<TResult>(this LinkedList<TResult> source)
		{
			if (source == null)
			{
				throw new ArgumentException("source is null", "source");
			}
			LinkedListNode<TResult> current = source.First;
			while (current != null)
			{
				LinkedListNode<TResult> next = current.Next;
				yield return current;
				current = current.Next ?? next;
			}
			yield break;
		}

		/// <summary>
		/// Adds a range of items to the end of a LinkedList.
		/// </summary>
		/// <param name="source">LinkedList to which the new items will be added</param>
		/// <param name="items">Items to add</param>
		/// <exception cref="System.ArgumentException">Thrown when <paramref name="source" /> or <paramref name="items" /> is null</exception>
		public static void AddRangeLast<TResult>(this LinkedList<TResult> source, IEnumerable<TResult> items)
		{
			if (source == null)
			{
				throw new ArgumentException("source is null", "source");
			}
			if (items == null)
			{
				throw new ArgumentException("items is null", "items");
			}
			foreach (var item in items)
			{
				source.AddLast(item);
			}
		}

		/// <summary>
		/// Adds a range of items to the start of a LinkedList.
		/// </summary>
		/// <param name="source">LinkedList to which the new items will be added</param>
		/// <param name="items">Items to add</param>
		/// <exception cref="System.ArgumentException">Thrown when <paramref name="source" /> or <paramref name="items" /> is null</exception>
		public static void AddRangeFirst<TResult>(this LinkedList<TResult> source, IEnumerable<TResult> items)
		{
			if (source == null)
			{
				throw new ArgumentException("source is null", "source");
			}
			foreach (var item in items)
			{
				source.AddFirst(item);
			}
		}

		/// <summary>
		/// Returns the first node of the sequence whose element satisfies a condition or a default value if no such node is found.
		/// </summary>
		/// <param name="source">A LinkedList from which a node is returned</param>
		/// <param name="predicate">A function to test each element for a condition</param>
		/// <exception cref="System.ArgumentException">Thrown when <paramref name="source" /> or <paramref name="predicate" /> is null</exception>
		/// <returns>null if source is empty or if no element passes the test specified by <paramref name="predicate" />; otherwise, the first element in <paramref name="source" /> that passes the test specified by <paramref name="predicate" /></returns>
		public static LinkedListNode<TResult> FirstOrDefaultNode<TResult>(this LinkedList<TResult> source, Func<TResult, bool> predicate)
		{
			if (source == null)
			{
				throw new ArgumentException("source is null", "source");
			}
			if (predicate == null)
			{
				throw new ArgumentException("predicate is null", "predicate");
			}
			for (var current = source.First; current != null; current = current.Next)
			{
				if (predicate(current.Value))
				{
					return current;
				}
			}
			return null;
		}

		/// <summary>
		/// Sorts the elements of a LinkedList in ascending order according to a key.
		/// </summary>
		/// <param name="source">The LinkedList to sort</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <exception cref="System.ArgumentException">Thrown when <paramref name="source" /> or <paramref name="keySelector" /> is null</exception>
		/// <returns>The source LinkedList to support method chaining</returns>
		public static LinkedList<TSource> Sort<TSource, TKey>(this LinkedList<TSource> source, Func<TSource, TKey> keySelector) where TKey : IComparable
		{
			if (source == null)
			{
				throw new ArgumentException("source is null", "source");
			}
			if (keySelector == null)
			{
				throw new ArgumentException("keySelector is null", "keySelector");
			}
			if (source.Count <= 1)
			{
				return source;
			}
			var current = source.First.Next;
			while (current != null)
			{
				var next = current.Next;
				for (var comparison = source.First; comparison != current; comparison = comparison.Next)
				{
					if (keySelector(current.Value).CompareTo(keySelector(comparison.Value)) < 0)
					{
						source.Remove(current);
						source.AddBefore(comparison, current);
						break;
					}
				}
				current = next;
			}
			return source;
		}
	}
}
