package repositories

import (
	"context"
	"fmt"
	"log"
	"os"
	"sync"

	mongo "go.mongodb.org/mongo-driver/mongo"
	"go.mongodb.org/mongo-driver/mongo/options"
)

var lock = &sync.Mutex{}

type single struct {
}

var client *mongo.Client

func getInstance() *mongo.Client {
	if client == nil {
		lock.Lock()
		defer lock.Unlock()
		if client == nil {
			connectionString := os.Getenv("connectionString")
			// connectionString := "mongodb://localhost:27017"
			fmt.Println("connectionString")
			fmt.Println(connectionString)
			clientOpts := options.Client().ApplyURI(connectionString)
			var err error
			client, err = mongo.Connect(context.TODO(), clientOpts)
			if err != nil {
				log.Println(err)
			}
		}
	}

	return client
}
