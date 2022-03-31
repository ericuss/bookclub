package server

import (
	"net/http"

	commands "bookclubapi/internal/commands"
	repositories "bookclubapi/internal/repositories"

	"github.com/gofiber/fiber/v2"
)

type BooksController interface {
	Fetch(w http.ResponseWriter, r *http.Request)
	Add(w http.ResponseWriter, r *http.Request)
	MarkAsReaded(w http.ResponseWriter, r *http.Request)
	FetchUnreaded(w http.ResponseWriter, r *http.Request)
	MarkAsUnreaded(w http.ResponseWriter, r *http.Request)
}

type booksController struct {
	repository     repositories.BookRepository
	userRepository repositories.UserRepository
}

func NewBooksController() *booksController {
	return &booksController{
		repository:     repositories.NewBookRepository(),
		userRepository: repositories.NewUserRepository(),
	}
}

func (a *booksController) Fetch(c *fiber.Ctx) error {
	books, _ := a.repository.Fetch()

	return c.JSON(books)
}

func (a *booksController) FetchById(c *fiber.Ctx) error {
	id := c.Params("id")
	book, _ := a.repository.FetchById(id)

	return c.JSON(book)
}

func (a *booksController) Add(c *fiber.Ctx) error {
	var command commands.AddBookRequest

	if err := c.BodyParser(&command); err != nil {
		return err
	}

	userId, err := GetUserId(c, a.userRepository)
	if err != nil {
		return err
	}

	command.UserId = userId

	book, _ := commands.NewAddBookHandler().Handler(command)

	return c.JSON(book)
}

func (a *booksController) MarkAsReaded(c *fiber.Ctx) error {
	return a.updateRead(c, true)
}

func (a *booksController) MarkAsUnreaded(c *fiber.Ctx) error {
	return a.updateRead(c, false)
}

func (a *booksController) FetchUnreaded(c *fiber.Ctx) error {
	books, _ := a.repository.FetchUnread()

	if books == nil || len(books) == 0 {
		return nil
	}

	return c.JSON(books)
}

func (a *booksController) updateRead(c *fiber.Ctx, readed bool) error {
	id := c.Params("id")
	userId, err := GetUserId(c, a.userRepository)
	if err != nil {
		return err
	}

	commandRequest := commands.MarkAsReadedRequest{Readed: readed, Id: id, UserId: userId}
	commands.NewMarkAsReadedHandler().Handler(commandRequest)
	return nil
}
