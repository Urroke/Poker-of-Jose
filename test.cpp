#include <iostream>
#include <Vector>
#include <algorithm>

#define OUT
#define IN

class Table
{
private:
    size_t count;
    std::vector<int> vec;
    int avarage;

    int toIndex(int num)
    {
        if(num >= 0)
        {
            return num % count;
        }

        num = -num;
        num = num % count;
        return count - num;
    }

    int getIndexMaxElement()
    {
        int res = 0;
        for(int i = 1;i < count; ++i)
        {
            if(vec[i] > vec[res])
            {
                res = i;
            }
        }
        return res;
    }

    int getIndexMinElement()
    {
        int res = 0;
        for(int i = 1;i < count; ++i)
        {
            if(vec[i] < vec[res])
            {
                res = i;
            }
        }
        return res;
    }

    void getForces(IN int index, OUT int& left, OUT int& right)
    {
        left = 0;
        right = 0;

        right += vec[toIndex(index + 1)] - vec[index];
        right += vec[toIndex(index + 2)] - vec[index];
        right += vec[toIndex(index + 3)] - vec[index];
        right += vec[toIndex(index + 4)] - vec[index];
        left += vec[toIndex(index - 1)] - vec[index];
        left += vec[toIndex(index - 2)] - vec[index];
        left += vec[toIndex(index - 3)] - vec[index];
        left += vec[toIndex(index - 4)] - vec[index];
    }

    bool isBalance()
    {
        bool res= true;
        for(int i = 0;i < count; ++i)
        {
            if(vec[i] != avarage)
            {
                return false;
            }
        }
        return true;
    }

    int sign(int value)
    {
        if(value > 0)
        {
            return 1;
        }
        return -1;
    }

public:
    Table() = delete;

    Table(std::vector<int> vec) : count(vec.size()), vec(vec), avarage(0)
    {
        for(int i = 0;i < vec.size(); ++i)
        {
            avarage += vec[i];
        }
        avarage /= count;
    }

    int makeBalance()
    {
        int counter = 0;
        int left = 0, right = 0;
        int force = 0;
        int shift = 0;
        int leftVal = 0;
        int rightVal = 0;
        int maxElIdx = 0;
        int minElIdx = 0;
        bool isMax = false;
        int position = 0;
        int distance = 0;
        
        while(!isBalance())
        {
            maxElIdx = getIndexMaxElement();
            minElIdx = getIndexMinElement();
            if(abs(vec[minElIdx] - avarage) > abs(vec[maxElIdx] - avarage))
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

            getForces(position, left, right);

            if(left == right)
            {
                int agIdx = 0;
                if(isMax)
                {
                    agIdx = getIndexMinElement();
                }
                else
                {
                    agIdx = getIndexMaxElement();
                }
                distance = abs(position - agIdx);
                force = sign(left);
                if(distance <= count / 2)
                {
                    if(position > agIdx)
                    {
                        vec[position] += force;
                        vec[toIndex(position - 1)] -= force;
                    }
                    else
                    {
                        vec[position] += force;
                        vec[toIndex(position + 1)] -= force;
                    }
                }
                else
                {
                    if(position > agIdx)
                    {
                        vec[position] += force;
                        vec[toIndex(position + 1)] -= force;
                    }
                    else
                    {
                        vec[position] += force;
                        vec[toIndex(position - 1)] -= force;
                    }
                }
                
            }

            if(abs(left) > abs(right))
            {
                if(abs(leftVal - vec[position]) < 1)
                {
                    continue;
                }
                force = sign(left);
                vec[toIndex(position - 1)] -= force;
                vec[position] += force;
            }
            if(abs(left) < abs(right))
            {
                if(abs(rightVal - vec[position]) < 1)
                {
                    continue;
                }
                force = sign(right);
                vec[toIndex(position + 1)] -= force;
                vec[position] += force;
            }
            counter++;
        }
        return counter;
    }
};

int main()
{
    size_t count = 0;
    std::cin>>count;
    std::vector<int> vec;
    int k;
    for(int i = 0; i < count; ++i)
    {
        std::cin>>k;
        vec.push_back(k);
    }
    Table t1(vec);

    std::cout<<t1.makeBalance();
}
