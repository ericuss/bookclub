package repositories

import (
	"context"
	"fmt"
	"log"
	"os"

	"go.mongodb.org/mongo-driver/bson"
	"go.mongodb.org/mongo-driver/bson/primitive"
	mongo "go.mongodb.org/mongo-driver/mongo"
	"go.mongodb.org/mongo-driver/mongo/options"
)

type RepositoryBase interface {
	Add(entity interface{}) (id primitive.ObjectID, err error)
	Upsert(id int, entity interface{}) (updateResult *mongo.UpdateResult, err error)
}

type repositoryBase struct {
	client     *mongo.Client
	collection *mongo.Collection
}

func NewRepositoryBase(collectionName string) *repositoryBase {
	connectionString := os.Getenv("connectionString")

	fmt.Println("connectionString")
	fmt.Println(connectionString)
	clientOpts := options.Client().ApplyURI(connectionString)
	client, err := mongo.Connect(context.TODO(), clientOpts)
	if err != nil {
		log.Fatal(err)
	}

	// Check the connections
	err = client.Ping(context.TODO(), nil)
	if err != nil {
		log.Fatal(err)
	}

	fmt.Println("Congratulations, you're already connected to MongoDB!")
	collection := client.Database("BookClub").Collection(collectionName)
	return &repositoryBase{
		client:     client,
		collection: collection,
	}
}

func (r *repositoryBase) Add(entity interface{}) (id primitive.ObjectID, err error) {
	result, err := r.collection.InsertOne(context.TODO(), entity)

	if err != nil {
		log.Fatal(err)
	}

	return result.InsertedID.(primitive.ObjectID), nil
}

func (r *repositoryBase) Upsert(id int, entity interface{}) (updateResult *mongo.UpdateResult, err error) {
	result, err := r.collection.UpdateOne(
		context.Background(),
		bson.M{"Id": id},
		bson.M{"$set": entity},
		options.Update().SetUpsert(true),
	)

	if err != nil {
		log.Fatal(err)
	}

	return result, nil
}
