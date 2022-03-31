package commands

import (
	"bookclubapi/internal/entities"
	repositories "bookclubapi/internal/repositories"
	services "bookclubapi/internal/services"
	"log"
	"strings"
)

type AddBookRequest struct {
	UserId string
	Url    string `json:"url"`
}

type addBookHandler struct {
	repository       repositories.BookRepository
	userRepository   repositories.UserRepository
	scrappingService services.ScrappingService
}

func NewAddBookHandler() *addBookHandler {
	return &addBookHandler{
		repository:       repositories.NewBookRepository(),
		userRepository:   repositories.NewUserRepository(),
		scrappingService: services.NewScrappingService(),
	}
}

func (h *addBookHandler) Handler(request AddBookRequest) (*entities.Book, error) {
	// endpoint := "https://www.goodreads.com/book/show/2767793-the-hero-of-ages"
	endpointSplitted := strings.Split(request.Url, "/")
	id := endpointSplitted[len(endpointSplitted)-1]

	user, err := h.getUser(request.UserId)

	if err != nil {
		log.Println(err)
	}

	book, err := h.scrappingService.Execute(request.Url)
	book.Id = id
	if err != nil {
		log.Println(err)
	}

	book.Username = user.Id

	addError := h.repository.Add(book)

	if addError != nil {
		log.Println(addError)
	}

	return book, nil
}

func (h *addBookHandler) getUser(id string) (*entities.User, error) {

	user, _ := h.userRepository.FetchById(id)

	if user == nil {
		log.Println("User not found")
	}
	return user, nil
}
