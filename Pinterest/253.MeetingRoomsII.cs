public class Solution {
    public int MinMeetingRooms(int[][] intervals) {
        // Sort intervals by start time
        Array.Sort(intervals, (a, b) => a[0].CompareTo(b[0]));

        // Min-heap to track end times of meetings
        var pq = new PriorityQueue<int, int>();

        foreach (var interval in intervals) {
            // If the earliest ending meeting is done before the current starts, reuse the room
            if (pq.Count > 0 && pq.Peek() <= interval[0]) {
                pq.Dequeue();
            }

            // Add the current meeting's end time
            pq.Enqueue(interval[1], interval[1]);
        }

        return pq.Count;
    }
}
