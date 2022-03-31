package middlewares

import (
	"bookclubapi/internal/util"

	"github.com/gofiber/fiber/v2"
)

func IsAuthenticated(c *fiber.Ctx) error {
	auth := c.GetReqHeaders()["Authorization"]
	// cookie := c.Cookies("jwt")

	if _, err := util.ParseJwt(auth); err != nil {
		c.Status(fiber.StatusUnauthorized)
		return c.JSON(fiber.Map{"message": "unauthenticated"})
	}

	return c.Next()
}

func IsAuthenticatedNotNecesary(c *fiber.Ctx) error {
	return c.Next()
}
