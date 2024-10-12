using System;

namespace codefacebook
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var myarray =sorted(new int[9]{2,2,1,0,2,0,1,1,2});
            //var myarray =sorted(new int[9]{0,0,1,2,2,1,1,2,2});
            //var myarray =sorted(new int[7]{2,0,0,2,0,2,2});
            for(int i=0; i < myarray.Length; i++){
                Console.Write(myarray[i]);
            }
        }

        static int[] sorted (int[] unsorted){
        int cursorZero = 0;
        int cursorOne = 0;
        int cursorTwo = unsorted.Length - 1;
        while(cursorOne <= cursorTwo){
            if (unsorted[cursorOne]==0) {
                    int temp = unsorted[cursorZero];
                    unsorted[cursorZero] = unsorted[cursorOne];
                    unsorted[cursorOne] = temp;
                    cursorZero++;
                    cursorOne++;
                    }
                else if(unsorted[cursorOne]==1) {
                    cursorOne++;
                    }
                else if(unsorted[cursorOne]==2) {
                    int tmp = unsorted[cursorOne];
                    unsorted[cursorOne] = unsorted[cursorTwo];
                    unsorted[cursorTwo] = tmp;
                    cursorTwo--;
                }
        }
        return unsorted;
            /*
            int cursorZero = 0;
            int cursorTwo=unsorted.Length-1;
            
            for(int i=0;i<=cursorTwo;i++){
                if(unsorted[i]==0){
                    int tmp = unsorted[cursorZero];
                    unsorted[cursorZero]=unsorted[i];
                    unsorted[i]=tmp;
                    cursorZero++;
                }
                while(unsorted[i]==2 && i<=cursorTwo){
                    int tmp = unsorted[cursorTwo];
                    unsorted[cursorTwo]=unsorted[i];
                    unsorted[i]=tmp;
                    cursorTwo--;
                }
            }
            return unsorted;
            */
        }
    }
}
