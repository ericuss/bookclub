package server

import (
	entities "bookclubapi/internal/entities"
	repositories "bookclubapi/internal/repositories"
	"log"

	"github.com/gofiber/fiber/v2"

	"bookclubapi/internal/util"

	"github.com/google/uuid"
)

type AuthController interface {
	Fetch() ([]*entities.User, error)
}

type authController struct {
	repository repositories.UserRepository
}

func NewAuthController() *authController {
	return &authController{
		repository: repositories.NewUserRepository(),
	}
}

// var repository *UserRepository

func (a *authController) Register(c *fiber.Ctx) error {
	var data map[string]string

	if err := c.BodyParser((&data)); err != nil {
		return err
	}

	if data["password"] != data["password_confirm"] {
		c.Status(400)
		return c.JSON(fiber.Map{"message": "Password does not match"})
	}

	existingUser, err := a.repository.FetchByEmail(data["email"])

	if existingUser.Id != "" {
		c.Status(400)
		return c.JSON(fiber.Map{"message": "user exist"})
	}

	id, _ := uuid.NewRandom()
	user := entities.User{
		Email: data["email"],
		Id:    id.String(),
	}

	user.SetPassword(data["password"])

	result, err := a.repository.Upsert(id.String(), user)
	if err != nil || result.UpsertedCount != 1 {
		log.Println(err)
	}

	return c.JSON(user)
}

func (a *authController) Login(c *fiber.Ctx) error {
	var data map[string]string

	if err := c.BodyParser((&data)); err != nil {
		return err
	}

	user, err := a.repository.FetchByEmail(data["email"])

	if err != nil {
		c.Status(400)
		return c.JSON(fiber.Map{"message": err})
	}

	if user.Id == "" {
		c.Status(404)
		return c.JSON(fiber.Map{"message": "User not found"})
	}

	if err := user.ComparePassword(data["password"]); err != nil {
		c.Status(400)
		return c.JSON(fiber.Map{"message": "Incorrect password"}) // anonymize
	}

	token, err := util.GenerateJwt(user.Id)
	if err != nil {
		return c.SendStatus(fiber.StatusInternalServerError)
	}

	// cookie := fiber.Cookie{
	// 	Name:     "jwt",
	// 	Value:    token,
	// 	Expires:  time.Now().Add(time.Hour * 24),
	// 	HTTPOnly: true,
	// }
	// c.Cookie(&cookie)

	return c.JSON(token)
}

func (a *authController) User(c *fiber.Ctx) error {
	cookie := c.Cookies("jwt")

	id, _ := util.ParseJwt(cookie)

	user, _ := a.repository.FetchById(id)

	if user == nil {
		c.Status(404)
		return c.JSON(fiber.Map{"message": "User not found"})
	}

	return c.JSON(user)
}

func (a *authController) Logout(c *fiber.Ctx) error {
	// cookie := fiber.Cookie{
	// 	Name:     "jwt",
	// 	Value:    "",
	// 	Expires:  time.Now().Add(-time.Hour),
	// 	HTTPOnly: true,
	// }
	// c.Cookie(&cookie)

	return c.JSON(fiber.Map{"message": "Loged out"})
}
