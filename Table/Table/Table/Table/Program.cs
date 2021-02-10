using System;

namespace Table
{
	class Program
	{
		static void Main(string[] args)
		{
			int count = Convert.ToInt32(Console.ReadLine());

			if (count > 0)
			{

				int[] vec = new int[count];
				int k = 0;
				for (int i = 0; i < count; ++i)
				{
					vec[i] = Convert.ToInt32(Console.ReadLine()); ;
				}

				Table t1 = new Table(vec);
				Console.WriteLine(t1.makeBalance());
			}
			else
			{
				Console.WriteLine(0);
			}
		}
	}
}
