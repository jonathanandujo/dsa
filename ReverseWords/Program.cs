using System;
using System.Collections.Generic;
using System.Text;

namespace ReverseWords
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine(Reverse("  the  sky  is blue  "));
        }
        static string Reverse(string s){
            //stack of words
        var st = new Stack<string>();
        //result
        var sb = new StringBuilder();
        //every word
        var tmp = "";
        for(int i=0; i<s.Length;i++){
            if(s[i]==' '){
                if(!string.IsNullOrEmpty(tmp)){
                    st.Push(tmp);
                    tmp="";
                }
            }
            else{
                tmp+=s[i];
            }
        }
        //last word if not spaces
        sb.Append(tmp);
        foreach(string str in st){
            if(sb.Length>0)
                sb.Append(" ");
            sb.Append($"{str}");
        }
        return sb.ToString();
        }
    }
}
