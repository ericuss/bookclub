package server

import (
	"encoding/json"
	"net/http"

	repositories "bookclubapi/internal/repositories"

	"github.com/gorilla/mux"
)

type booksController struct {
	router     *mux.Router
	repository repositories.BookRepository
}

func NewBooksController(repository repositories.BookRepository, r *mux.Router) Server {
	a := &booksController{repository: repository}

	r.HandleFunc("/api/books", a.fetch).Methods(http.MethodGet)

	a.router = r
	return a
}

func (a *booksController) routes() {
	a.router.HandleFunc("/api/books", a.fetch).Methods(http.MethodGet)
}
func (a *booksController) Router() mux.Router {
	return *a.router
}

func (a *booksController) fetch(w http.ResponseWriter, r *http.Request) {
	books, _ := a.repository.Fetch()

	w.Header().Set("Content-Type", "application/json")
	json.NewEncoder(w).Encode(books)
}
