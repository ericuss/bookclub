package routes

import (
	"bookclubapi/internal/middlewares"
	controllers "bookclubapi/internal/server"

	"github.com/gofiber/fiber/v2"
)

func Setup(app *fiber.App) {
	authController := *controllers.NewAuthController()
	userController := *controllers.NewUserController()
	bookController := *controllers.NewBooksController()
	voteListsController := *controllers.NewVoteListsController()

	app.Static("/", "public")
	app.Static("/public*", "public")
	app.Static("/static", "public/static")

	app.Get("/health", func(c *fiber.Ctx) error {
		return c.SendString("healthy")
	})

	app.Post("/api/register", authController.Register)
	app.Post("/api/login", authController.Login)
	api := app.Group("/api")
	api.Use(middlewares.IsAuthenticated)
	api.Get("/user", authController.User)
	api.Post("/logout", authController.Logout)

	api.Get("/users", userController.AllUsers)
	api.Post("/users", userController.CreateUser)
	api.Get("/users/:id", userController.GetUserById)
	api.Put("/users/:id", userController.UpdateUser)
	api.Delete("/users/:id", userController.DeleteUser)

	api.Get("/books", bookController.Fetch)
	api.Post("/books", bookController.Add)
	api.Get("/books/:id", bookController.FetchById)
	api.Put("/books/:id/readed", bookController.MarkAsReaded)
	api.Get("/books/unreaded", bookController.FetchUnreaded)
	api.Put("/books/:id/unreaded", bookController.MarkAsUnreaded)

	api.Get("/vote-lists", voteListsController.Fetch)
	api.Get("/vote-lists/:id", voteListsController.FetchById)
	api.Post("/vote-lists", voteListsController.Add)
	// app.Static("/*", "./public")

}
