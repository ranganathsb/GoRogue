﻿using Troschuetz.Random;
using Troschuetz.Random.Generators;

namespace GoRogue.Random
{
	/// <summary>
	/// Static class that contains 1 variable, which is a an RNG instance that is used as the default
	/// rng by other features (dice, etc) wherever needed, and can also be used if you need a random
	/// number generator for your own code.
	/// </summary>
	public static class SingletonRandom
	{
		/// <summary>
		/// Settable field that specifies what <see cref="IGenerator"/> instance should be considered the default
		/// RNG. Defaults to an <see cref="XorShift128Generator"/> with a time-dependent value used as a seed.
		/// </summary>
		public static IGenerator DefaultRNG = GetDefaultGenerator();

		private static IGenerator GetDefaultGenerator()
		{
			var gen = new XorShift128Generator();
			for (int i = 0; i < 3; i++) // Becuase of bug in the RNG library, make sure we are completely pseudo-random
				gen.Next();

			return gen;
		}
	}
}