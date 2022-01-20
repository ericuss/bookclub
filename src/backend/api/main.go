package main

import (
	repositories "bookclubapi/internal/repositories"
	server "bookclubapi/internal/server"
	"fmt"
	"log"
	"net/http"

	"github.com/gorilla/handlers"
	"github.com/gorilla/mux"
)

func init() {
	log.Println("init")
}

func main() {
	log.Println("Starting app...")

	router := mux.NewRouter()
	bookRepository := repositories.NewBookRepository()

	server.NewBooksController(bookRepository, router)
	router.HandleFunc("/health", healthHandler)

	fmt.Println("The bookclub server is on tap now: http://localhost:8080")

	httpHanler := configureCors(router)
	log.Fatal(http.ListenAndServe(":8080", httpHanler))

	log.Println("Finished")
}

func healthHandler(w http.ResponseWriter, _ *http.Request) {
	w.WriteHeader(http.StatusOK)
	w.Write([]byte("everything is ok!"))
}

func configureCors(router *mux.Router) http.Handler {
	return handlers.CORS(
		handlers.AllowedHeaders([]string{"X-Requested-With"}),
		// Where ORIGIN_ALLOWED is like `scheme://dns[:port]`, or `*` (insecure)
		// handlers.AllowedOrigins([]string{os.Getenv("ORIGIN_ALLOWED")}),
		handlers.AllowedOrigins([]string{"*"}),
		handlers.AllowedMethods([]string{"GET", "HEAD", "POST", "PUT", "OPTIONS"}),
	)(router)
}
