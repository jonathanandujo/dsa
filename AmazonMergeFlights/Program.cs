/*
1) You are given a list of airline tickets where tickets[i] = [fromi, toi] represent the departure and the arrival airports of one flight.
Reconstruct the itinerary in order and return it.

All of the tickets belong to a man who departs from "JFK", thus, the itinerary must begin with "JFK".
If there are multiple valid itineraries, you should return the itinerary that has the smallest lexical order when read as a single string.

For example, the itinerary ["JFK", "LGA"] has a smaller lexical order than ["JFK", "LGB"].
You may assume all tickets form at least one valid itinerary. You must use all the tickets once and only once.


Example 1:
Input: tickets = [["MUC","LHR"],["JFK","MUC"],["SFO","SJC"],["LHR","SFO"]]
Output: ["JFK","MUC","LHR","SFO","SJC"]

Example 2:
Input: tickets = [["JFK","SFO"],["JFK","ATL"],["SFO","ATL"],["ATL","JFK"],["ATL","SFO"]]
Output: ["JFK","ATL","JFK","SFO","ATL","SFO"]
Explanation: Another possible reconstruction is ["JFK","SFO","ATL","JFK","ATL","SFO"] but it is larger in lexical order.

//Input: tickets = [["JFK","SFO"],["JFK","ATL"],["SFO","ATL"],["ATL","JFK"],["ATL","SFO"]]


counter used tickets - 1
selecting jfk - arrivals -
list<list<strings>>
// jfk - muc - lhr - sfo - sjc
//[["JFK","SFO"],["JFK","ATL"],["SFO","ATL"],["ATL","JFK"],["ATL","SFO"]]

Input: tickets = [["MUC","LHR"],["JFK","MUC"],["SFO","SJC"],["LHR","SFO"]] hashset<(string,string)> //dictionary<string departure,string[] arrivals>
Output: ["JFK","MUC","LHR","SFO","SJC"]
*/
Console.WriteLine("Hello, World!");
IList<IList<string>> tickets = new List<IList<string>>();
tickets.Add(new List<string>(){"JFK","SFO"});
tickets.Add(new List<string>(){"JFK","ATL"});
tickets.Add(new List<string>(){"SFO","ATL"});
tickets.Add(new List<string>(){"ATL","JFK"});
tickets.Add(new List<string>(){"ATL","SFO"});
var result = FindItinerary(tickets);
foreach(string r in result)
    Console.Write($"{r}-");
//Console.WriteLine
IList<string> FindItinerary(IList<IList<string>> tickets) {
    if(tickets == null || tickets.Count == 0) //Mistake 1:has length insted of count
        return new List<string>();

    List<string> result = new();
    int totalTickets = tickets.Count; // 5 //Mistake 2:has length insted of count
    Dictionary<string, SortedList<string,string>> data = new(); //Mistake 3:it needs 2 vaues, has string instead of string,string
    
    //Fill out dictionary with input
    foreach(List<string> pair in tickets)
    {
        if(!data.ContainsKey(pair[0]))
            data[pair[0]] = new(); //creating list
        data[pair[0]].Add(pair[1],pair[1]); //source . add -> destination //
    }
    /*
    data =
    jfk = 
    sfo = 
    atl = 
    */
    
    result.Add("JFK");
    for(int i = 0; i < totalTickets; i++) //4
    {
        var ticket = data[result[^1]]; //list<string>
        result.Add(ticket.First().Key); // add atl //Mistake 4:it has ticket[0] and should be first().Key
        ticket.RemoveAt(0);
    }
    return result; // jfk - atl - jfk - sfo - atl - sfo
}
//jfk = sfo
/*
Input: tickets = [["JFK","SFO"],["JFK","ATL"],["SFO","ATL"],["ATL","JFK"],["ATL","SFO"]]
Output: ["JFK","ATL","JFK","SFO","ATL","SFO"]
return result; // jfk - atl - jfk - sfo - atl - sfo
*/