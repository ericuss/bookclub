package commands

import (
	repositories "bookclubapi/internal/repositories"
	services "bookclubapi/internal/services"
	"log"
)

type VoteVoteListRequest struct {
	Id     string
	UserId string
	Books  []string
}

type voteVoteListHandler struct {
	repository repositories.VoteListRepository
}

func NewVoteVoteListHandler() *voteVoteListHandler {
	return &voteVoteListHandler{
		repository: repositories.NewVoteListRepository(),
	}
}

func (h *voteVoteListHandler) Handler(request VoteVoteListRequest) error {
	voteList, err := h.repository.FetchById(request.Id)

	if err != nil {
		return err
	}
	booksUpdated := make(map[string][]string)
	for k, v := range voteList.Books {
		if services.StringInSlice(k, request.Books) {
			v = append(v, request.UserId)
		}
		booksUpdated[k] = v
	}

	voteList.Books = booksUpdated

	result, err := h.repository.Upsert(voteList.Id, voteList)

	if err != nil && result.ModifiedCount != 1 {
		log.Println(err)
	}

	return nil
}
