package server

import (
	"encoding/json"
	"io/ioutil"
	"net/http"

	commands "bookclubapi/internal/commands"
	repositories "bookclubapi/internal/repositories"

	"github.com/gorilla/mux"
)

type booksController struct {
	router     *mux.Router
	repository repositories.BookRepository
}

func NewBooksController(repository repositories.BookRepository, r *mux.Router) Server {
	a := &booksController{repository: repository}
	a.router = r
	a.routes()
	return a
}

func (a *booksController) routes() {
	a.router.HandleFunc("/api/books", a.fetch).Methods(http.MethodGet)
	a.router.HandleFunc("/api/books", a.add).Methods(http.MethodPost)
}
func (a *booksController) Router() mux.Router {
	return *a.router
}

func (a *booksController) fetch(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json")
	books, _ := a.repository.Fetch()

	json.NewEncoder(w).Encode(books)
}

func (a *booksController) add(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json")

	reqBody, _ := ioutil.ReadAll(r.Body)
	var commandRequest commands.AddBookRequest
	json.Unmarshal(reqBody, &commandRequest)

	book, _ := commands.NewUpsertCharactersHandler().Handler(commandRequest)

	json.NewEncoder(w).Encode(book)
}
