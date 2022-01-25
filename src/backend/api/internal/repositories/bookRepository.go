package repositories

import (
	"context"
	"fmt"
	"log"

	entities "bookclubapi/internal/entities"

	"go.mongodb.org/mongo-driver/bson"
	"go.mongodb.org/mongo-driver/mongo/options"
)

type BookRepository interface {
	RepositoryBase
	Fetch() ([]*entities.Book, error)
	UpdateReaded(id string, readed bool) error
}

type bookRepository struct {
	repositoryBase
}

func NewBookRepository() *bookRepository {
	return &bookRepository{
		repositoryBase: *NewRepositoryBase("Books"),
	}
}

func (r *bookRepository) Fetch() ([]*entities.Book, error) {
	var results []*entities.Book
	findOptions := options.Find()
	cur, err := r.repositoryBase.collection.Find(context.TODO(), bson.D{{}}, findOptions)
	if err != nil {
		log.Fatal(err)
	}
	for cur.Next(context.TODO()) {

		// create a value into which the single document can be decoded
		var s entities.Book
		err := cur.Decode(&s)
		if err != nil {
			log.Fatal(err)
		}

		results = append(results, &s)
	}

	if err := cur.Err(); err != nil {
		log.Fatal(err)
	}

	return results, nil
}

func (r *bookRepository) UpdateReaded(id string, readed bool) error {
	result, err := r.repositoryBase.collection.UpdateOne(
		context.TODO(),
		bson.M{"Id": id},
		bson.D{
			{"$set", bson.D{{"Readed", readed}}},
		})
	if err != nil {
		log.Fatal(err)
	}
	fmt.Printf("Updated %v Documents!\n", result.ModifiedCount)
	return err
}
