package main

import (
	repositories "bookclubapi/internal/repositories"
	"log"
)

func main() {
	log.Println("Starting app...")

	characterRepository := repositories.NewCharacterRepository()

	characters, err := characterRepository.FetchCharacters()

	if err != nil {
		log.Fatal(err)
	}

	for i := 0; i < len(characters); i++ {
		log.Println(characters[i].Name)
	}
	log.Println("Finished")
}
