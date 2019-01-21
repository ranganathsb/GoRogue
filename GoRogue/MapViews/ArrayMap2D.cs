﻿using System;

namespace GoRogue.MapViews
{
	/// <summary>
	/// Default implementation of the ISettableMapView interface, exposing the interface using a
	/// 2D array to store data.
	/// </summary>
	/// <typeparam name="T">The type of value being stored.</typeparam>
	public class ArrayMap2D<T> : ISettableMapView<T>, ICloneable
	{
		private readonly T[,] _array;

		/// <summary>
		/// Constructor. Takes width and height of array.
		/// </summary>
		/// <param name="width">Width of array.</param>
		/// <param name="height">Height of array.</param>
		public ArrayMap2D(int width, int height)
			: this(new T[width, height])
		{ }


		public ArrayMap2D(T[,] existingArray)
		{
			_array = existingArray;
		}

		/// <summary>
		/// The height of the map.
		/// </summary>
		public int Height => _array.GetLength(1);

		/// <summary>
		/// The width of the map.
		/// </summary>
		public int Width => _array.GetLength(0);


		public T this[int index1D]
		{
			get => _array[Coord.ToXValue(index1D, Width), Coord.ToYValue(index1D, Width)];
			set => _array[Coord.ToXValue(index1D, Width), Coord.ToYValue(index1D, Width)] = value;
		}

		/// <summary>
		/// Given an X and Y value, returns/sets the "value" associated with that location.
		/// </summary>
		/// <param name="x">X-value of location.</param>
		/// <param name="y">Y-value of location.</param>
		/// <returns>The "value" associated with that location.</returns>
		public T this[int x, int y]
		{
			get => _array[x, y];
			set => _array[x, y] = value;
		}

		/// <summary>
		/// Given a Coord, returns/sets the "value" associated with that location.
		/// </summary>
		/// <param name="pos">Location to get/set the value for.</param>
		/// <returns>THe "value" associated with the provided location.</returns>
		public T this[Coord pos]
		{
			get => _array[pos.X, pos.Y];
			set => _array[pos.X, pos.Y] = value;
		}

		/// <summary>
		/// Performs deep copy of array map.
		/// </summary>
		/// <returns>The cloned ArrayMap2D.</returns>
		public object Clone()
		{
			var newObj = new ArrayMap2D<T>(Width, Height);

			for (int x = 0; x < Width; x++)
				for (int y = 0; y < Height; y++)
					newObj[x, y] = _array[x, y];

			return newObj;
		}

		public static implicit operator T[,] (ArrayMap2D<T> arrayMap) => arrayMap._array;


		/// <summary>
		/// Sets each element in the ArrayMap to the default for type T.
		/// </summary>
		public void SetToDefault() => Array.Clear(_array, 0, _array.Length);

		/// <summary>
		/// Returns a string representation of the 2D array.
		/// </summary>
		/// <returns>A string representation of the 2D array.</returns>
		public override string ToString() => this.ExtendToString();

		/// <summary>
		/// Returns a string representation of the 2D array, using the elementStringifier function
		/// given to determine what string represents which value.
		/// </summary>
		/// <remarks>
		/// This could be used, for example, on an ArrayMap2D of boolean values, to output '#' for
		/// false values, and '.' for true values.
		/// </remarks>
		/// <param name="elementStringifier">
		/// Function determining the string representation of each element.
		/// </param>
		/// <returns>A string representation of the 2D array.</returns>
		public string ToString(Func<T, string> elementStringifier) => this.ExtendToString(elementStringifier: elementStringifier);

		/// <summary>
		/// Prints the values in the ArrayMap2D, using the function specified to turn elements into
		/// strings, and using the "field length" specified. Each element of type T will have spaces
		/// added to cause it to take up exactly fieldSize characters, provided fieldSize is less
		/// than the length of the element's string represention. A positive-number right-aligns the
		/// text within the field, while a negative number left-aligns the text.
		/// </summary>
		/// <param name="fieldSize">The size of the field to give each value.</param>
		/// <param name="elementStringifier">
		/// Function to use to convert each element to a string. Null defaults to the ToString
		/// function of type T.
		/// </param>
		/// <returns>A string representation of the ArrayMap2D.</returns>
		public string ToString(int fieldSize, Func<T, string> elementStringifier = null) => this.ExtendToString(fieldSize, elementStringifier: elementStringifier);
	}
}