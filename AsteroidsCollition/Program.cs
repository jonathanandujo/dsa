// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");
//int[] asteroids = { 6, 2, 4, -3, 6, 1, 3, -5, -6, 4, -6 }; // -6 -6 6 6
//int[] asteroids = { 6, 8, -8, 10, -10, 5, -5, 8, -8, 6, -6, 7, -7, 9, -9, 4, -4 ,-11}; // -8 -10 -11
//int[] asteroids = { 6, 8, -8, 10, -10, 5, -5, 8, -8, 6, -6, 7, -7, 9, -9, 4, -4 }; // -8 -10 10 9 4
//int[] asteroids = {6, 2, 4, -3, 6, 1, 3, -5, -6, 4, -6}; // -6 -6 6 6
int[] asteroids = {-6,-6, 2, 4, -3, 6, 1, 3, -5, -6, 4, -6}; // -6 -6 6 6
//int [] asteroids = { -7, 7}; // -7 7
// -8,  -10, 10, 6, 7, -7, 9, -9, 4, -4 };
// int[] asteroids = { 6, 6, -6, -6 };
/*
6, 2, 4, -3, 6, 1, 3, -5, -6, 4, -6
-6 -6 -6 6



I ll type here for the questions to be asked ::

1. Approach: Priority queue, space O(k), time log(k)<--- tell this
2. Approach: Quick select

cool


*/
//int[] asteroids = { 6, 8, -8 };

var result = AsteroidCollision(asteroids); // 6, 6, 4, -6

result.ToList().ForEach(Console.WriteLine);

Console.WriteLine("Change Direction");

result = AsteroidCollisionChangeDirection(asteroids); // -6,-6,6,6 // 8, -8
result.ToList().ForEach(Console.WriteLine);


static int[] AsteroidCollision(int[] asteroids)
{
    List<int> survival = new List<int>();
    for(int i = 0; i<asteroids.Length; i++)
    {
        int asteroid = asteroids[i];
        while(survival.Count > 0 && survival[^1] > 0 && asteroid < 0)
        {
            int crash = survival[^1] + asteroid;
            if(crash > 0)
                asteroid = 0;
            else if(crash < 0 )
                survival.RemoveAt(survival.Count -1);
            else // if crash is 0
            {
                survival.RemoveAt(survival.Count -1);
                asteroid = 0;
            }
        }
        if(asteroid != 0)
            survival.Add(asteroid);
    }

    return survival.ToArray();
}

static int[] AsteroidCollisionChangeDirection(int[] asteroids)
{
    List<int> survival = new();
    for(int i = 0; i < asteroids.Length; i++)
    {
        int asteroid = asteroids[i];
        while(survival.Count > 0 && survival[^1] > 0 && asteroid < 0)
        {
            int crash = survival[^1] + asteroid;
            if(crash > 0)
            {
                asteroid = 0;
                break;
            }
            else if(crash < 0)
            {
                survival.RemoveAt(survival.Count -1);
            }
            else // if are same weight, must change direction
            {
                asteroid = -survival[^1];
                survival.RemoveAt(survival.Count -1);
                asteroids[i] = -asteroids[i--];
                asteroids[i--] = asteroid;
                asteroid = 0;
            }
        }
        if(asteroid != 0)
            survival.Add(asteroid);
    }
    return survival.ToArray();
}