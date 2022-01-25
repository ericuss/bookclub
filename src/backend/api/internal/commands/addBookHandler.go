package commands

import (
	"bookclubapi/internal/entities"
	repositories "bookclubapi/internal/repositories"
	services "bookclubapi/internal/services"
	"log"
	"strings"
)

type AddBookRequest struct {
	Url string `json:"url"`
}

type upsertCharactersHandler struct {
	repository       repositories.BookRepository
	scrappingService services.ScrappingService
}

func NewUpsertCharactersHandler() *upsertCharactersHandler {
	return &upsertCharactersHandler{
		repository:       repositories.NewBookRepository(),
		scrappingService: services.NewScrappingService(),
	}
}

func (h *upsertCharactersHandler) Handler(request AddBookRequest) (*entities.Book, error) {
	// endpoint := "https://www.goodreads.com/book/show/2767793-the-hero-of-ages"
	endpointSplitted := strings.Split(request.Url, "/")
	id := endpointSplitted[len(endpointSplitted)-1]
	user := "Random"

	book, err := h.scrappingService.Execute(request.Url, user)
	book.Id = id

	if err != nil {
		log.Fatalln(err)
	}

	_, addError := h.repository.Add(book)

	if addError != nil {
		log.Fatalln(addError)
	}

	return book, nil
}
