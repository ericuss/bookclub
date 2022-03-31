package server

import (
	"net/http"

	commands "bookclubapi/internal/commands"
	repositories "bookclubapi/internal/repositories"

	"github.com/gofiber/fiber/v2"
)

type VoteListsController interface {
	Fetch(w http.ResponseWriter, r *http.Request) error
	FetchById(c *fiber.Ctx) error
	Add(w http.ResponseWriter, r *http.Request) error
}

type voteListsController struct {
	repository     repositories.VoteListRepository
	userRepository repositories.UserRepository
}

func NewVoteListsController() *voteListsController {
	return &voteListsController{
		repository:     repositories.NewVoteListRepository(),
		userRepository: repositories.NewUserRepository(),
	}
}

func (a *voteListsController) Fetch(c *fiber.Ctx) error {
	response, _ := a.repository.Fetch()

	return c.JSON(response)
}

func (a *voteListsController) FetchById(c *fiber.Ctx) error {
	id := c.Params("id")

	voteList, _ := commands.NewGetVoteListDetail().Handler(commands.GetVoteListDetailRequest{Id: id})
	return c.JSON(voteList)
}

func (a *voteListsController) Add(c *fiber.Ctx) error {
	var command commands.AddVoteListRequest

	if err := c.BodyParser(&command); err != nil {
		return err
	}

	userId, err := GetUserId(c, a.userRepository)
	if err != nil {
		return err
	}

	command.UserId = userId

	book, _ := commands.NewAddVoteListHandler().Handler(command)

	return c.JSON(book)
}
func (a *voteListsController) Vote(c *fiber.Ctx) error {
	var command commands.VoteVoteListRequest

	if err := c.BodyParser(&command); err != nil {
		return err
	}

	userId, err := GetUserId(c, a.userRepository)
	if err != nil {
		return err
	}

	command.UserId = userId

	err = commands.NewVoteVoteListHandler().Handler(command)
	if err != nil {
		c.Response().SetStatusCode(400)
		return c.JSON(fiber.Map{"error": err})
	}

	return nil
}
