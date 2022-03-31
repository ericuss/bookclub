package repositories

import (
	"context"
	"log"

	entities "bookclubapi/internal/entities"

	"go.mongodb.org/mongo-driver/bson"
	"go.mongodb.org/mongo-driver/mongo/options"
)

type VoteListRepository interface {
	RepositoryBase
	Fetch() ([]*entities.VoteList, error)
	FetchById(id string) (*entities.VoteList, error)
}

type voteListRepository struct {
	repositoryBase
}

func NewVoteListRepository() *voteListRepository {
	return &voteListRepository{
		repositoryBase: *NewRepositoryBase("VoteList"),
	}
}

func (r *voteListRepository) Fetch() ([]*entities.VoteList, error) {
	var results []*entities.VoteList
	findOptions := options.Find()
	cur, err := r.repositoryBase.collection.Find(context.TODO(), bson.D{{}}, findOptions)
	if err != nil {
		log.Println(err)
	}
	for cur.Next(context.TODO()) {

		// create a value into which the single document can be decoded
		var s entities.VoteList
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

func (r *voteListRepository) FetchById(id string) (*entities.VoteList, error) {
	cur := r.repositoryBase.collection.FindOne(context.TODO(), bson.M{"Id": id})

	// create a value into which the single document can be decoded
	var s entities.VoteList
	err := cur.Decode(&s)
	if err != nil {
		log.Println(err)
	}

	return &s, nil
}
