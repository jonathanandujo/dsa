docker pull confluentinc/cp-kafka:7.4.0

docker-compose up -d


docker exec -it <container_id> kafka-console-consumer --bootstrap-server localhost:9092 --topic test-topic --from-beginning


348cda7d04f1

docker exec -it <kafka-container-id> kafka-topics \
  --create --topic test-topic \
  --bootstrap-server localhost:9092 \
  --partitions 1 --replication-factor 1


docker exec -it 348cda7d04f1 kafka-topics --create --topic test-topic --bootstrap-server  localhost:9092 --partitions 1 --replication-factor 1

docker exec -it 348cda7d04f1 kafka-topics --list --bootstrap-server localhost:9092

docker exec -it <kafka-container-id> kafka-topics --list --bootstrap-server localhost:9092
