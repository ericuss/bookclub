package repositories

import (
	"context"
	"log"

	entities "bookclubapi/internal/entities"

	"go.mongodb.org/mongo-driver/bson"
	"go.mongodb.org/mongo-driver/mongo/options"
)

type BookRepository interface {
	RepositoryBase
	Fetch() ([]*entities.Book, error)
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
