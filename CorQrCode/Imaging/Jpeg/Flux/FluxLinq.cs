
namespace FluxJpeg
{

	using libQrCode;
	using System;
	using System.Collections.Generic;


	internal static class FluxLinq
    {


		internal delegate TResult Func<in T, out TResult>(T arg);
		internal delegate TResult Func<in T1, in T2, out TResult>(T1 arg1, T2 arg2);
		
	

		public static TSource[] ToArray<TSource>(this IEnumerable<TSource> source)
		{
			Check.Source(source);

			var collection = source as ICollection<TSource>;
			if (collection != null)
			{
				var array = new TSource[collection.Count];
				collection.CopyTo(array, 0);
				return array;
			}

			return new List<TSource>(source).ToArray();
		}


		public static IEnumerable<TResult> Select<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
		{
			Check.SourceAndSelector(source, selector);

			return CreateSelectIterator(source, selector);
		}

		static IEnumerable<TResult> CreateSelectIterator<TSource, TResult>(IEnumerable<TSource> source, Func<TSource, TResult> selector)
		{
			foreach (var element in source)
				yield return selector(element);
		}




		public static bool Any<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			Check.SourceAndPredicate(source, predicate);

			foreach (TSource element in source)
				if (predicate(element))
					return true;

			return false;
		}



		public static TAccumulate Aggregate<TSource, TAccumulate>(this IEnumerable<TSource> source,
			TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func)
		{
			Check.SourceAndFunc(source, func);

			TAccumulate folded = seed;
			foreach (TSource element in source)
				folded = func(folded, element);

			return folded;
		}




		public static int Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
		{
			Check.SourceAndSelector(source, selector);
			int total = 0;

			foreach (var element in source)
				total = checked(total + selector(element));

			return total;
		}



		// TODO: test nullable and non-nullable
		public static TSource Max<TSource>(this IEnumerable<TSource> source)
		{
			Check.Source(source);

			var comparer = Comparer<TSource>.Default;

			TSource max = default(TSource);

			if (default(TSource) == null)
			{
				foreach (var element in source)
				{
					if (element == null)
						continue;

					if (max == null || comparer.Compare(element, max) > 0)
						max = element;
				}
			}
			else
			{
				bool empty = true;
				foreach (var element in source)
				{
					if (empty)
					{
						max = element;
						empty = false;
						continue;
					}
					if (comparer.Compare(element, max) > 0)
						max = element;
				}
				if (empty)
					throw new InvalidOperationException();
			}
			return max;
		}


	}
}
