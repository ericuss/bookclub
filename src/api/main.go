// package main

// import (
// 	repositories "bookclubapi/internal/repositories"
// 	server "bookclubapi/internal/server"
// 	"fmt"
// 	"log"
// 	"net/http"
// 	"os"

// 	"github.com/gorilla/mux"
// 	"github.com/joho/godotenv"
// 	"github.com/rs/cors"
// )

// func init() {
// 	log.Println("init")
// 	err := godotenv.Load()
// 	if err != nil {
// 		log.Println("Error loading .env file")
// 	}
// }

// func main() {
// 	log.Println("Starting app...")
// 	router := mux.NewRouter()
// 	bookRepository := repositories.NewBookRepository()

// 	server.NewBooksController(bookRepository, router)
// 	router.HandleFunc("/health", healthHandler)

// 	fmt.Println("The bookclub server is on tap now: http://localhost:8080")

// 	httpHanler := configureCors(router)
// 	log.Println(http.ListenAndServe(":8080", httpHanler))

// 	log.Println("Finished")
// }

// func healthHandler(w http.ResponseWriter, _ *http.Request) {
// 	w.WriteHeader(http.StatusOK)
// 	w.Write([]byte("everything is ok!"))
// }

// func configureCors(router *mux.Router) http.Handler {
// 	return cors.New(cors.Options{
// 		// AllowCredentials
// 		// handlers.AllowedHeaders([]string{"X-Requested-With"}),
// 		// Where ORIGIN_ALLOWED is like `scheme://dns[:port]`, or `*` (insecure)
// 		AllowedOrigins: []string{os.Getenv("ORIGIN_ALLOWED")},
// 		AllowedMethods: []string{"GET", "HEAD", "POST", "PUT", "OPTIONS"},
// 	}).Handler(router)
// }

package main

import (
	"fmt"
	"log"

	"github.com/gofiber/fiber/v2"
	"github.com/gofiber/fiber/v2/middleware/cors"
	"github.com/joho/godotenv"

	"bookclubapi/internal/routes"
	"os"
	"os/signal"
)

func main() {
	log.Println("Starting app...")
	err := godotenv.Load(".env")

	if err != nil {
		log.Println("Error loading .env file")
	}

	app := fiber.New()

	app.Use(cors.New(cors.Config{
		AllowCredentials: true,
	}))
	routes.Setup(app)

	c := make(chan os.Signal, 1)
	signal.Notify(c, os.Interrupt)
	go func() {
		_ = <-c
		fmt.Println("Gracefully shutting down...")
		_ = app.Shutdown()
	}()

	// ...
	httpPort := os.Getenv("HTTP_PORT")

	fmt.Println("The bookclub server is on tap now: http://localhost:" + httpPort)
	if err := app.Listen(":" + httpPort); err != nil {
		log.Panic(err)
	}

	fmt.Println("Running cleanup tasks...")
	// Your cleanup tasks go here
}
