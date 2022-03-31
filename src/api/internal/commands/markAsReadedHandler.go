package commands

import (
	repositories "bookclubapi/internal/repositories"
	services "bookclubapi/internal/services"
	"log"
)

type MarkAsReadedRequest struct {
	UserId string
	Id     string
	Readed bool
}

type markAsReadedHandler struct {
	repository repositories.BookRepository
}

func NewMarkAsReadedHandler() *markAsReadedHandler {
	return &markAsReadedHandler{
		repository: repositories.NewBookRepository(),
	}
}

func (h *markAsReadedHandler) Handler(request MarkAsReadedRequest) error {

	book, err := h.repository.FetchById(request.Id)
	if err != nil {
		log.Println(err)
	}

	if request.Readed {
		if !services.StringInSlice(request.UserId, book.Readed) {
			book.Readed = append(book.Readed, request.UserId)
		}
	} else {
		book.Readed = services.RemoveValue(book.Readed, request.UserId)
	}

	updateResult, err := h.repository.Upsert(request.Id, book)

	if err != nil {
		log.Println(err)
	}

	if updateResult.ModifiedCount != 1 {
		log.Println("An error occurred updating book")
	}

	return err
}
