﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EnumsNET.Test
{
	public static class TestHelper
	{
		public static void ExpectException<TException>(Action action)
			where TException : Exception
		{
			try
			{
				action();
				Assert.Fail($"Expected exception of type {typeof(TException).Name}");
			}
			catch (TException)
			{
			}
		}

		public static void ArraysAreEqual<T>(T[] array1, T[] array2)
		{
			if (array1 == null || array2 == null)
			{
				Assert.AreEqual(array1, array2);
			}
			Assert.AreEqual(array1.Length, array2.Length);

			for (var i = 0; i < array1.Length; ++i)
			{
				Assert.AreEqual(array1[i], array2[i]);
			}
		}

		public static void ArrayOfArraysAreEqual<T>(T[][] array1, T[][] array2)
		{
			if (array1 == null || array2 == null)
			{
				Assert.AreEqual(array1, array2);
			}
			Assert.AreEqual(array1.Length, array2.Length);

			for (var i = 0; i < array1.Length; ++i)
			{
				ArraysAreEqual(array1[i], array2[i]);
			}
		}

		public static void EnumerablesAreEqual<T>(IEnumerable<T> enumerable1, IEnumerable<T> enumerable2)
		{
			if (enumerable1 == null || enumerable2 == null)
			{
				Assert.AreEqual(enumerable1, enumerable2);
			}
			using (var enumerator1 = enumerable1.GetEnumerator())
			{
				using (var enumerator2 = enumerable2.GetEnumerator())
				{
					while (enumerator1.MoveNext())
					{
						Assert.IsTrue(enumerator2.MoveNext());
						Assert.AreEqual(enumerator1.Current, enumerator2.Current);
					}
					Assert.IsFalse(enumerator2.MoveNext());
				}
			}
		}

		public static void EnumerableOfEnumerablesAreEqual<T>(IEnumerable<IEnumerable<T>> enumerable1, IEnumerable<IEnumerable<T>> enumerable2)
		{
			if (enumerable1 == null || enumerable2 == null)
			{
				Assert.AreEqual(enumerable1, enumerable2);
			}
			using (var enumerator1 = enumerable1.GetEnumerator())
			{
				using (var enumerator2 = enumerable2.GetEnumerator())
				{
					while (enumerator1.MoveNext())
					{
						Assert.IsTrue(enumerator2.MoveNext());
						EnumerablesAreEqual(enumerator1.Current, enumerator2.Current);
					}
					Assert.IsFalse(enumerator2.MoveNext());
				}
			}
		}
	}
}