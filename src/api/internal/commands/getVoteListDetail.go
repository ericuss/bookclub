package commands

import (
	"bookclubapi/internal/entities"
	repositories "bookclubapi/internal/repositories"
	"errors"
)

type GetVoteListDetailRequest struct {
	Id string
}

type GetVoteListDetailResponse struct {
	Id            string
	UserId        string
	Title         string
	NumberOfVotes int16
	Books         []BookDto
}

type BookDto struct {
	Id       string
	VotedBy  []string
	Title    string
	Series   string
	Authors  string
	Rating   string
	Sinopsis string

	ImageUrl string
	Url      string
	Pages    string
	Username string

	Readed []string
}

type getVoteListDetail struct {
	repository     repositories.VoteListRepository
	bookRepository repositories.BookRepository
}

func NewGetVoteListDetail() *getVoteListDetail {
	return &getVoteListDetail{
		repository:     repositories.NewVoteListRepository(),
		bookRepository: repositories.NewBookRepository(),
	}
}

func (h *getVoteListDetail) Handler(request GetVoteListDetailRequest) (*GetVoteListDetailResponse, error) {
	voteList, err := h.repository.FetchById(request.Id)

	if err != nil {
		return nil, err
	}

	var bookIds []string

	if voteList.Books == nil || len(voteList.Books) == 0 {
		return nil, errors.New("List of books is empty")

	}

	for k := range voteList.Books {
		bookIds = append(bookIds, k)
	}

	books, err := h.bookRepository.FetchByIds(bookIds)

	if err != nil {
		return nil, err
	}

	response := GetVoteListDetailResponse{
		Id:            voteList.Id,
		Title:         voteList.Title,
		NumberOfVotes: voteList.NumberOfVotes,
		UserId:        voteList.UserId,
	}

	for k, v := range voteList.Books {
		book := getBook(k, books)
		if book != nil {
			response.Books = append(response.Books, BookDto{
				Id:       book.Id,
				Title:    book.Title,
				VotedBy:  v,
				Series:   book.Series,
				Authors:  book.Authors,
				Rating:   book.Rating,
				Sinopsis: book.Sinopsis,
				ImageUrl: book.ImageUrl,
				Url:      book.Url,
				Pages:    book.Pages,
				Username: book.Username,
				Readed:   book.Readed,
			})
		}
	}

	return &response, nil
}

func getBook(id string, books []*entities.Book) *entities.Book {
	for _, v := range books {
		if v.Id == id {
			return v
		}
	}

	return nil
}
