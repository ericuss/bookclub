package commands

import (
	repositories "bookclubapi/internal/repositories"
	"log"
)

type MarkAsReadedRequest struct {
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

	err := h.repository.UpdateReaded(request.Id, request.Readed)

	if err != nil {
		log.Fatalln(err)
	}

	return err
}
