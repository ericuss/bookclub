package repositories

import (
	"context"
	"fmt"
	"log"
	"os"

	mongo "go.mongodb.org/mongo-driver/mongo"
	"go.mongodb.org/mongo-driver/mongo/options"
)

type RepositoryBase interface {
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
