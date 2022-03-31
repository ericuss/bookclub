package server

import (
	repositories "bookclubapi/internal/repositories"
	"bookclubapi/internal/util"

	"github.com/gofiber/fiber/v2"
)

type Server interface {
}

func GetUserId(c *fiber.Ctx, repository repositories.UserRepository) (string, error) {
	// cookie := c.Cookies("jwt")
	auth := c.GetReqHeaders()["Authorization"]

	id, _ := util.ParseJwt(auth)

	user, _ := repository.FetchById(id)

	if user == nil {
		c.Status(404)
		return "", c.JSON(fiber.Map{"message": "User not found"})
	}

	return id, nil
}
