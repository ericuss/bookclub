package server

import (
	"bookclubapi/internal/entities"
	repositories "bookclubapi/internal/repositories"
	"log"
	"math"
	"strconv"

	"github.com/gofiber/fiber/v2"
)

type UserController interface {
	Fetch() ([]*entities.User, error)
}

type userController struct {
	repository repositories.UserRepository
}

func NewUserController() *userController {
	return &userController{
		repository: repositories.NewUserRepository(),
	}
}

func (a *userController) AllUsers(c *fiber.Ctx) error {
	page, _ := strconv.Atoi(c.Query("page", "1"))
	limit, _ := strconv.Atoi(c.Query("limit", "5"))
	offset := (page - 1) * limit
	var total int64
	users, _ := a.repository.Fetch(int64(offset), int64(limit))

	// database.DB.Preload("Role").Offset(offset).Limit(limit).Find(&users)
	// database.DB.Model(&entities.User{}).Count(&total)

	return c.JSON(fiber.Map{
		"data": users,
		"meta": fiber.Map{
			"limit":     limit,
			"page":      page,
			"total":     total,
			"last_page": math.Ceil(float64(int(total) / limit)),
		},
	})
}

func (a *userController) CreateUser(c *fiber.Ctx) error {
	var user entities.User

	if err := c.BodyParser(&user); err != nil {
		return err
	}

	user.SetPassword("123456Aa!")
	a.repository.Add(&user)

	return c.JSON(user)
}

func (a *userController) GetUserById(c *fiber.Ctx) error {
	id := c.Params("id")

	user, _ := a.repository.FetchById(id)

	return c.JSON(user)
}

func (a *userController) UpdateUser(c *fiber.Ctx) error {
	id := c.Params("id")
	user := entities.User{
		Id: id,
	}

	if err := c.BodyParser(&user); err != nil {
		return err
	}

	_, err := a.repository.Upsert(id, user)
	if err != nil {
		log.Println(err)
	}

	return c.JSON(user)
}

func (a *userController) DeleteUser(c *fiber.Ctx) error {
	id := c.Params("id")
	user := entities.User{
		Id: id,
	}

	err := a.repository.Delete(id)
	if err != nil {
		log.Println(err)
	}

	return c.JSON(user)
}
