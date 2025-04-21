(ns wordcounter)

(defn counter [input]
  (->> (clojure.string/split input #" ")
       (frequencies)))

(defn -main []
  (let [x (counter "this is a text to take any text to count every word in the text counting")
        total (reduce + (vals x))]
    (doseq [[word count] x]
      (println (str word "-" count)))
    (println (str "Total words: " total))))