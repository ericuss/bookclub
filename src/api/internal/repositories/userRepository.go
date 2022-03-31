package repositories

import (
	"context"
	"log"

	entities "bookclubapi/internal/entities"

	"go.mongodb.org/mongo-driver/bson"
	mongo "go.mongodb.org/mongo-driver/mongo"
	"go.mongodb.org/mongo-driver/mongo/options"
)

type UserRepository interface {
	Add(entity interface{}) error
	Upsert(id string, entity interface{}) (updateResult *mongo.UpdateResult, err error)
	Delete(id string) (err error)
	Fetch(skip int64, limit int64) ([]*entities.User, error)
	FetchById(id string) (*entities.User, error)
	FetchByEmail(email string) (*entities.User, error)
}

type userRepository struct {
	repositoryBase
}

func NewUserRepository() *userRepository {
	return &userRepository{
		repositoryBase: *NewRepositoryBase("Users"),
	}
}

func (r *userRepository) Fetch(skip int64, limit int64) ([]*entities.User, error) {
	var results []*entities.User
	findOptions := options.Find()
	findOptions.SetSkip(skip)
	findOptions.SetLimit(limit)
	cur, err := r.repositoryBase.collection.Find(context.TODO(), bson.D{{}}, findOptions)
	if err != nil {
		log.Println(err)
	}
	for cur.Next(context.TODO()) {

		// create a value into which the single document can be decoded
		var s entities.User
		err := cur.Decode(&s)
		if err != nil {
			log.Println(err)
		}

		results = append(results, &s)
	}

	if err := cur.Err(); err != nil {
		log.Println(err)
	}

	return results, nil
}

func (r *userRepository) FetchById(id string) (*entities.User, error) {
	cur := r.repositoryBase.collection.FindOne(context.TODO(), bson.M{"Id": id})

	// create a value into which the single document can be decoded
	var s entities.User
	err := cur.Decode(&s)
	if err != nil {
		log.Println(err)
	}

	return &s, nil
}

func (r *userRepository) FetchByEmail(email string) (*entities.User, error) {
	cur := r.repositoryBase.collection.FindOne(context.TODO(), bson.M{"email": email})

	// create a value into which the single document can be decoded
	var s entities.User
	err := cur.Decode(&s)
	if err != nil {
		log.Print(err)
	}

	return &s, nil
}
