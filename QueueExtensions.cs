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

using System;
using System.Collections.Generic;

namespace OrbitalGames.Collections
{
	/// <summary>
	/// Extension methods for System.Queue.
	/// </summary>
	public static class QueueExtensions
	{
		/// <summary>
		/// Enqueue a range of items.
		/// </summary>
		/// <param name="source">Destination queue</param>
		/// <param name="items">Items to add</param>
		/// <exception cref="System.ArgumentNullException">Thrown when <paramref name="source" /> or <paramref name="items" /> is null</exception>
		public static void EnqueueRange<TResult>(this Queue<TResult> source, IEnumerable<TResult> items)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			foreach (var item in items)
			{
				source.Enqueue(item);
			}
		}
	}
}
