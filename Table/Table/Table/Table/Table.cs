using System;
using System.Collections.Generic;
using System.Text;

namespace Table
{
	class Table
	{
		private int count;
		private int[] vec;
		int avarage;

		private int toIndex(int num)
		{
			if (num >= 0)
			{
				return num % count;
			}

			num = -num;
			num = num % count;
			return count - num;
		}

		private int getDistance(int a, int b)
		{
			int result = Math.Abs(b - a);

			if (count / 2 < result)
			{
				result = count - result;
			}

			return result;
		}

		private int getIndexMaxElement(int position)
		{
			int res = 0;
			int distance = getDistance(position, 0);
			for (int i = 1; i < count; ++i)
			{
				if (vec[i] > vec[res])
				{
					res = i;
					distance = getDistance(position, i);
				}
				if (vec[i] == vec[res])
				{
					int dist = getDistance(i, position);
					if (dist < distance)
					{
						res = i;
						distance = dist;
					}
				}
			}
			return res;
		}

		private int getIndexMinElement(int position)
		{
			int res = 0;
			int distance = getDistance(position, 0);
			for (int i = 1; i < count; ++i)
			{
				if (vec[i] < vec[res])
				{
					res = i;
					distance = getDistance(position, i);
				}
				if (vec[i] == vec[res])
				{
					int dist = getDistance(i, position);
					if (dist < distance)
					{
						res = i;
						distance = dist;
					}
				}
			}
			return res;
		}

		private void getForces(int index, ref int left, ref int right)
		{
			left = 0;
			right = 0;

			for (int i = 0; i <= 3; ++i)
			{
				right += (vec[toIndex(index + i)] - vec[index]);
				left += (vec[toIndex(index - i)] - vec[index]);
			}

		}

		private bool isBalance()
		{
			bool res = true;
			for (int i = 0; i < count; ++i)
			{
				if (vec[i] != avarage)
				{
					return false;
				}
			}
			return true;
		}

		private int sign(int value)
		{
			if (value > 0)
			{
				return 1;
			}
			return -1;
		}

		public Table() {; }

		public Table(int[] vec)
		{
			count = vec.Length;
			avarage = 0;
			this.vec = vec;
			for (int i = 0; i < vec.Length; ++i)
			{
				avarage += vec[i];
			}
			avarage /= count;
		}

		public int makeBalance()
		{
			int counter = 0;
			int left = 0, right = 0;
			int force = 0;
			int leftVal = 0;
			int rightVal = 0;
			int maxElIdx = 0;
			int minElIdx = 0;
			bool isMax = false;
			int position = 0;
			int distance = 0;

			while (!isBalance())
			{
				maxElIdx = getIndexMaxElement(position);
				minElIdx = getIndexMinElement(maxElIdx);
				maxElIdx = getIndexMaxElement(minElIdx);

				if (Math.Abs(vec[minElIdx] - avarage) > Math.Abs(vec[maxElIdx] - avarage))
				{
					position = minElIdx;
					isMax = false;
				}
				else
				{
					position = maxElIdx;
					isMax = true;
				}

				leftVal = vec[toIndex(position - 1)];
				rightVal = vec[toIndex(position + 1)];

				getForces(position, ref left, ref right);

				if (leftVal == rightVal)
				{
					int agIdx = 0;
					if (isMax)
					{
						agIdx = getIndexMinElement(position);
					}
					else
					{
						agIdx = getIndexMaxElement(position);
					}
					distance = Math.Abs(position - agIdx);
					force = sign(left);
					if (distance <= count / 2)
					{
						if (position > agIdx)
						{
							while (vec[position] == vec[toIndex(position - 1)])
							{
								position = toIndex(position - 1);
							}
							vec[position] += force;
							vec[toIndex(position - 1)] -= force;
						}
						else
						{
							while (vec[position] == vec[toIndex(position + 1)])
							{
								position = toIndex(position + 1);
							}
							vec[position] += force;
							vec[toIndex(position + 1)] -= force;
						}
					}
					else
					{
						if (position > agIdx)
						{
							while (vec[position] == vec[toIndex(position + 1)])
							{
								position = toIndex(position + 1);
							}
							vec[position] += force;
							vec[toIndex(position + 1)] -= force;
						}
						else
						{
							while (vec[position] == vec[toIndex(position - 1)])
							{
								position = toIndex(position - 1);
							}
							vec[position] += force;
							vec[toIndex(position - 1)] -= force;
						}
					}

				}
				else
				{

					if (Math.Abs(left) > Math.Abs(right))
					{

						force = sign(left);
						vec[toIndex(position - 1)] -= force;
						vec[position] += force;
					}
					else
					{
						force = sign(right);
						vec[toIndex(position + 1)] -= force;
						vec[position] += force;
					}
				}
				counter++;
			}
			return counter;
		}
	}
};
