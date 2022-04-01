package repositories

import (
	"context"
	"fmt"
	"log"

	"go.mongodb.org/mongo-driver/bson"
	mongo "go.mongodb.org/mongo-driver/mongo"
	"go.mongodb.org/mongo-driver/mongo/options"
)

type RepositoryBase interface {
	Add(entity interface{}) error
	Upsert(id string, entity interface{}) (updateResult *mongo.UpdateResult, err error)
	Delete(id string) (err error)
}

type repositoryBase struct {
	client     *mongo.Client
	collection *mongo.Collection
}

func NewRepositoryBase(collectionName string) *repositoryBase {
	// connectionString := os.Getenv("connectionString")
	// // connectionString := "mongodb://localhost:27017"
	// fmt.Println("connectionString")
	// fmt.Println(connectionString)
	// clientOpts := options.Client().ApplyURI(connectionString)
	// client, err := mongo.Connect(context.TODO(), clientOpts)
	// if err != nil {
	// 	log.Println(err)
	// }

	client := getInstance()
	// Check the connections
	err := client.Ping(context.TODO(), nil)
	if err != nil {
		log.Println(err)
	}

	fmt.Println("Congratulations, you're already connected to MongoDB!")
	collection := client.Database("BookClub").Collection(collectionName)
	return &repositoryBase{
		client:     client,
		collection: collection,
	}
}

func (r *repositoryBase) Add(entity interface{}) error {
	_, err := r.collection.InsertOne(context.TODO(), entity)

	if err != nil {
		log.Println(err)
	}

	return nil
}

func (r *repositoryBase) Upsert(id string, entity interface{}) (updateResult *mongo.UpdateResult, err error) {
	result, err := r.collection.UpdateOne(
		context.Background(),
		bson.M{"Id": id},
		bson.M{"$set": entity},
		options.Update().SetUpsert(true),
	)

	if err != nil {
		log.Println(err)
	}

	return result, nil
}

func (r *repositoryBase) Delete(id string) (err error) {
	result, err := r.collection.DeleteOne(
		context.Background(),
		bson.M{"Id": id},
	)

	if err != nil || result.DeletedCount != 1 {
		log.Println(err)
		return err
	}

	return nil
}
